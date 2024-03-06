using Assets.Scripts.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public GameObject ghostCd, shieldCd, lightningCd, teleCd, player, shield, speedBoost, lightArrow, teleEffect;
    public AudioSource audioSource;
    public AudioClip speedBoostClip, shieldOnClip, lightShootClip, teleportClip;

    float ghostTimeTotal = 5, shieldTimeTotal = 3, lightningTimeTotal = 3, teleTimeTotal = 2;
    float currentGhostCd, currentShieldCd, currentLightningCd, currentTeleCd;
    float shieldTime = 2, ghostTime = 4, timeCount = 0, teleDistance = 3;
    int lightningTurn = 5;
    public float t = 1;
    internal bool isShieldOn = false, isSpeedBoost = false;
    void Start()
    {

        currentGhostCd = 0;
        currentShieldCd = 0;
        currentLightningCd = 0;
        currentTeleCd = 0;

        ghostCd.GetComponent<Animator>().GetComponent<Animator>().speed = 1 / ghostTimeTotal;
        shieldCd.GetComponent<Animator>().GetComponent<Animator>().speed = 1 / shieldTimeTotal;
        lightningCd.GetComponent<Animator>().GetComponent<Animator>().speed = 1 / lightningTimeTotal;
        teleCd.GetComponent<Animator>().GetComponent<Animator>().speed = 1 / teleTimeTotal;
    }
    void Update()
    {
        Time.timeScale = t;
        if (Input.GetKeyDown(KeyCode.Z) && currentGhostCd <= 0)
        {
            audioSource.PlayOneShot(speedBoostClip);
            currentGhostCd = ghostTimeTotal;
            ghostCd.GetComponent<Animator>().GetComponent<Animator>().Play("skillCd", 0, 0);
            ghostOn();
        }
        if (Input.GetKeyDown(KeyCode.X) && currentShieldCd <= 0)
        {
            audioSource.PlayOneShot(shieldOnClip);
            currentShieldCd = shieldTimeTotal;
            shieldCd.GetComponent<Animator>().GetComponent<Animator>().Play("skillCd", 0, 0);
            shieldOn();
            GlobalVariables.main.GetComponent<CameraZoomByTime>().beatIt(4.8f, 5, 2f);
        }
        if (Input.GetKeyDown(KeyCode.C) && currentLightningCd <= 0)
        {
            lightningTurn = 5;
            bulletAim();
        }
        if (Input.GetKeyDown(KeyCode.S) && currentTeleCd <= 0)
        {
            audioSource.PlayOneShot(teleportClip);
            currentTeleCd = teleTimeTotal;
            teleCd.GetComponent<Animator>().GetComponent<Animator>().Play("skillCd", 0, 0);
            StartCoroutine("teleport");
        }

        skillCountDown();

        if (isSpeedBoost && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            timeCount += Time.deltaTime;
            if (timeCount > 0.1)
            {
                timeCount = 0;
                GameObject cloud = Instantiate(speedBoost, player.transform.position - new Vector3(0, 0.2f), Quaternion.identity);
                cloud.SetActive(true);
                Destroy(cloud, 9 / 24f);
            }
        }
    }

    private void ghostOn()
    {
        isSpeedBoost = true;
        player.GetComponent<PlayerMovement>().moveSpeed *= 1.5f;
        Invoke("ghostOff", ghostTime);
    }
    private void ghostOff()
    {
        isSpeedBoost = false;
        player.GetComponent<PlayerMovement>().moveSpeed /= 1.5f;
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
        if (bullets.Count > 0)
        {
            audioSource.PlayOneShot(lightShootClip);
            if (lightningTurn == 5)
            {
                currentLightningCd = lightningTimeTotal;
                lightningCd.GetComponent<Animator>().GetComponent<Animator>().Play("skillCd", 0, 0);
            }
            lightningTurn--;
            GameObject[] bulletArray = bullets.OrderBy(x => Vector2.SqrMagnitude(x.transform.position - player.transform.position)).ToArray();
            for (int i = 0; i < bulletArray.Length; i++)
            {
                if (i < 3)
                {
                    Instantiate(lightArrow, player.transform.position, Quaternion.identity).GetComponent<LightArrow>().aim(bulletArray[i]);
                }
            }
        }
        if(lightningTurn > 0)
        {
            Invoke("bulletAim", 0.5f);
        }
    }
    IEnumerator teleport()
    {
        Vector3 teleDir = player.GetComponent<Rigidbody2D>().velocity.normalized;
        if (teleDir == Vector3.zero)
        {
            teleDir = Vector2.down;
        }
        teleEffect.SetActive(true);
        teleEffect.transform.localPosition = player.transform.localPosition;
        player.transform.localPosition += teleDistance * teleDir;

        yield return new WaitForSeconds(9f / 24);
        teleEffect.SetActive(false);
    }
    void skillCountDown()
    {
        if (currentGhostCd > 0)
        {
            currentGhostCd -= Time.deltaTime;
        }
        if (currentShieldCd > 0)
        {
            currentShieldCd -= Time.deltaTime;
        }
        if (currentLightningCd > 0)
        {
            currentLightningCd -= Time.deltaTime;
        }
        if (currentTeleCd > 0)
        {
            currentTeleCd -= Time.deltaTime;
        }
    }
}
