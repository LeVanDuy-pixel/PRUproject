using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using TMPro;


public class GameController : MonoBehaviour
{
    
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] bulletSpawnPoint;//
    
    
    private Transform playerTransform;
    private int nextBeatIndex = 0;
    public bool isFinish = false;

    WaitToStart waitToStart;
    BeatTimes beatTimes;
    SpawnPoints spawnPoints;
    Timer timer;
    void Start()
    {
        waitToStart = FindObjectOfType<WaitToStart>();
        beatTimes = FindObjectOfType<BeatTimes>();
        spawnPoints = FindObjectOfType<SpawnPoints>();
        timer = FindObjectOfType<Timer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
       

    }
    private void Update()
    {
        if(waitToStart.canPlay)
        {
            audioSource.Play();
            StartCoroutine(SpawnBullets(beatTimes.resultBeatTimes));
            waitToStart.canPlay = false;
        }
    }

    IEnumerator SpawnBullets(float[] times)
    {
        while (nextBeatIndex < times.Length)
        {
            while (audioSource.isPlaying && audioSource.time < times[nextBeatIndex])
            {
                yield return null;
            }


            if (audioSource.time >= times[nextBeatIndex])
            {
                
                Transform blspawnPoint = spawnPoints.spawnPoints[UnityEngine.Random.Range(0, 34)];
                Transform spawnPoint = blspawnPoint;
                Vector3 direction = playerTransform.position - spawnPoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                GameObject bl = Instantiate(bulletPrefab, spawnPoint.position, rotation);
                bl.transform.parent = spawnPoint.transform;
                if (timer.num <= 14)
                {
                    blspawnPoint = spawnPoints.spawnPoints[UnityEngine.Random.Range(0, 34)];
                    spawnPoint = blspawnPoint;
                    direction = playerTransform.position - spawnPoint.position;
                    angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    bl = Instantiate(bulletPrefab, spawnPoint.position, rotation);
                    bl.transform.parent = spawnPoint.transform;
                }

                if (nextBeatIndex == 2|| nextBeatIndex == 5||nextBeatIndex==8 || nextBeatIndex==12)
                {
                    for (int i = 0; i < 3; i++)
                    {
                         blspawnPoint = spawnPoints.spawnPoints[UnityEngine.Random.Range(0, 34)];
                         spawnPoint = blspawnPoint;
                         direction = playerTransform.position - spawnPoint.position;
                         angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                         rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                         bl = Instantiate(bulletPrefab, spawnPoint.position, rotation);
                        bl.transform.parent = spawnPoint.transform;
                    }
                }
                nextBeatIndex++;
            }
        }

    }
   
    
}
