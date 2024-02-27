using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public GameObject ghostCd, shieldCd, lightningCd, player, shield, speedBoost, lightArrow;
    Vector2 oriScale, oriSize, oriPosition;
    float ghostTimeTotal = 8, shieldTimeTotal = 5, lightningTimeTotal = 5, currentGhostCd, currentShieldCd, currentLightningCd;
    float shieldTime = 2, ghostTime = 4, timeCount = 0;
    bool isShieldOn = false, isSpeedBoost = false;
    void Start()
    {
        currentGhostCd = 0;
        currentShieldCd = 0;
        currentLightningCd = 0;

        oriScale = ghostCd.transform.localScale;
        oriPosition = ghostCd.transform.localPosition;
        oriSize.y = ghostCd.GetComponent<SpriteRenderer>().size.y * oriScale.y;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && currentGhostCd <= 0)
        {
            currentGhostCd = ghostTimeTotal;
            ghostCd.transform.localPosition = oriPosition;
            ghostOn();
        }
        if (Input.GetKeyDown(KeyCode.X) && currentShieldCd <= 0)
        {
            currentShieldCd = shieldTimeTotal;
            shieldCd.transform.localPosition = new Vector3(shieldCd.transform.localPosition.x, oriPosition.y);
            shieldOn();
        }
        if (Input.GetKeyDown(KeyCode.C) && currentLightningCd <= 0)
        {
            currentLightningCd = lightningTimeTotal;
            lightningCd.transform.localPosition = new Vector3(lightningCd.transform.localPosition.x, oriPosition.y);
            bulletAim();
        }

        skillCountDown();

        if (isSpeedBoost && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            timeCount += Time.deltaTime;
            if (timeCount > 0.05)
            {
                timeCount = 0;
                GameObject cloud = Instantiate(speedBoost, player.transform.localPosition - new Vector3(0, 0.5f), Quaternion.identity);
                cloud.SetActive(true);
                Destroy(cloud, 9 / 24f);
            }
        }
    }
    private void ghostOn()
    {
        isSpeedBoost = true;
        player.GetComponent<PlayerMovement>().moveSpeed *= 2;
        Invoke("ghostOff", ghostTime);
    }
    private void ghostOff()
    {
        isSpeedBoost = false;
        player.GetComponent<PlayerMovement>().moveSpeed /= 2;
    }
    private void shieldOn()
    {
        isShieldOn = true;
        shield.SetActive(true);
        Invoke("shieldOff", shieldTime);
    }
    private void shieldOff()
    {
        isShieldOn = false;
        shield.SetActive(false);
    }
    void bulletAim()
    {
        List<GameObject> bullets = GameObject.FindGameObjectsWithTag("Bullet").ToList();
        if (bullets.Count == 0)
        {
            currentLightningCd = 0;
        }
        else
        {
            GameObject[] bulletArray = bullets.OrderBy(x => Vector2.SqrMagnitude(x.transform.localPosition - player.transform.localPosition)).ToArray();
            for (int i = 0; i < bulletArray.Length; i++)
            {
                if (i < 3)
                {
                    Instantiate(lightArrow, player.transform.localPosition, Quaternion.identity).GetComponent<LightArrow>().aim(bulletArray[i]);
                }
            }
        }

    }
    void skillCountDown()
    {
        if (currentGhostCd >= 0)
        {
            scaleTopDown(ref currentGhostCd, ghostTimeTotal, ref ghostCd);
        }
        if (currentShieldCd >= 0)
        {
            scaleTopDown(ref currentShieldCd, shieldTimeTotal, ref shieldCd);
        }
        if (currentLightningCd >= 0)
        {
            scaleTopDown(ref currentLightningCd, lightningTimeTotal, ref lightningCd);
        }
    }
    void scaleTopDown(ref float currentTime, float totalTime, ref GameObject cd)
    {
        cd.transform.localScale = new Vector2(oriScale.x, oriScale.y * currentTime / totalTime);
        cd.transform.localPosition -= new Vector3(0, oriSize.y * Time.deltaTime / totalTime / 2);
        currentTime -= Time.deltaTime;
    }
}
