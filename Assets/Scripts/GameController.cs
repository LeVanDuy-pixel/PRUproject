using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MiniJSON;
using System;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] bulletSpawnPoint;//
  
    
    private Transform playerTransform;
    private float[] beatTimes; 
    private int nextBeatIndex = 0;
    public string filePath;
    void Start()
    {
        
        beatTimes = new float[] {0.2f, 0.41f, 0.65f,0.9f,1.1f,1.37f};
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnBullets(beatTimes));

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
                Transform spawnPoint = bulletSpawnPoint[nextBeatIndex];
                Vector3 direction = playerTransform.position - spawnPoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                GameObject bl = Instantiate(bulletPrefab, spawnPoint.position, rotation);
                bl.transform.parent = spawnPoint.transform;
                nextBeatIndex++;
            }
        }

    }
   
    
}
