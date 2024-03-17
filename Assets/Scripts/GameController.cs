using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameFinishPanel;
    
    
    private Transform playerTransform;
    private int nextBeatIndex = 0;
    public bool isFinish = false;
    public bool isOver = false;

    WaitToStart waitToStart;
    BeatTimes beatTimes;
    SpawnPoints spawnPoints;
    Timer timer;
    CameraZoomByTime _zoomCamera;
    void Start()
    {
        waitToStart = FindObjectOfType<WaitToStart>();
        beatTimes = FindObjectOfType<BeatTimes>();
        spawnPoints = FindObjectOfType<SpawnPoints>();
        timer = FindObjectOfType<Timer>();
        _zoomCamera = FindObjectOfType<CameraZoomByTime>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
       

    }
    private void Update()
    {
        if (isFinish)
        {
            gameFinishPanel.SetActive(true);
        }
        if(waitToStart.canPlay)
        {
            audioSource.Play();
            StartCoroutine(SpawnBullets(beatTimes.resultBeatTimes));
            waitToStart.canPlay = false;
        }
    }

    IEnumerator SpawnBullets(float[] times)
    {
        while (nextBeatIndex < times.Length && !isOver)
        {
            while (audioSource.isPlaying && audioSource.time < times[nextBeatIndex])
            {
                yield return null;
            }


            if (audioSource.time >= times[nextBeatIndex])
            {
                SpawnBullet();
                
                if (timer.num <= 14)
                {
                    SpawnBullet();
                }

                if (nextBeatIndex == 2|| nextBeatIndex == 5||nextBeatIndex==8 || nextBeatIndex==12 || nextBeatIndex == 75)
                {
                    _zoomCamera.beatIt(4.8f, 5f, 0.6f);
                    
                    for (int i = 0; i < 3; i++) 
                    {
                        SpawnBullet();
                    }
                }
                nextBeatIndex++;
            }
        }

    }
    private void SpawnBullet()
    {
        Transform blspawnPoint = spawnPoints.spawnPoints[UnityEngine.Random.Range(0, 34)];
        Transform spawnPoint = blspawnPoint;
        Vector3 direction = playerTransform.position - spawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject bl = Instantiate(bulletPrefab, spawnPoint.position, rotation);
        bl.transform.parent = spawnPoint.transform;
    }
    public void GameOver()
    {
        isOver = true;
        audioSource.Stop();
        gameOverPanel.SetActive(true);
    }
   
   
    public void Replay()
    {
        SceneManager.LoadScene("Level1");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
