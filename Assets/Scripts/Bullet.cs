using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem effect;

    private float destroyTime;
    private Vector3 direction;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = (player.transform.position - this.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
        destroyTime += Time.deltaTime;
        if(destroyTime > 4)
        {
            Destroy(gameObject);
        }
        rb.AddForce(direction * 9);
    }
}
