using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] bulletSpawnPoints;

    public float[] beatTimes = new float[] { 0.2f, 0.41f, 0.65f, 0.9f, 1.1f, 1.37f }; // Mảng beatTimes

    private Transform playerTransform;
    private int nextBeatIndex = 0;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnBullets(beatTimes));
        Debug.Log("Giá trị của beatTimes:");
        foreach (float beatTime in beatTimes)
        {
            Debug.Log(beatTime);
        }
    }


    private IEnumerator SpawnBullets(float[] times)
    {
        while (nextBeatIndex < times.Length)
        {
            while (audioSource.isPlaying && audioSource.time < times[nextBeatIndex])
            {
                yield return null;
            }

            if (audioSource.time >= times[nextBeatIndex])
            {
                Transform spawnPoint = bulletSpawnPoints[nextBeatIndex];
                Vector3 direction = playerTransform.position - spawnPoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, rotation);
                bullet.transform.parent = spawnPoint.transform;
                nextBeatIndex++;
            }
        }
    }
}
