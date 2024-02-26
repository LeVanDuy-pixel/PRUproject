using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public GameObject ghostCd, shieldCd, player, shield;
    Vector2 oriScale, oriSize, oriPosition;
    float ghostTimeTotal = 10, shieldTimeTotal = 5, currentGhostCd, currentShieldCd;
    float shieldTime = 2, ghostTime = 4;
    bool isShieldOn = false, isSpeedBoost = false;
    void Start()
    {
        currentGhostCd = 0;
        currentShieldCd = 0;

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
        skillCountDown();
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
    void skillCountDown()
    {
        if (currentGhostCd >= 0)
        {
            scaleTopDown(ref currentGhostCd, ghostTimeTotal,ref ghostCd);
        }
        if (currentShieldCd >= 0)
        {
            scaleTopDown(ref currentShieldCd, shieldTimeTotal, ref shieldCd);
        }
    }
    void scaleTopDown(ref float currentTime, float totalTime, ref GameObject cd)
    {
        cd.transform.localScale = new Vector2(oriScale.x, oriScale.y * currentTime / totalTime);
        cd.transform.localPosition -= new Vector3(0, oriSize.y * Time.deltaTime / totalTime / 2);
        currentTime -= Time.deltaTime;
    }
}
