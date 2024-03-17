using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem effect;

    GameController gameController;
    Skills skills;
    private float destroyTime;
    private Vector3 direction;
    public float speed;
    void Start()
    {
        skills = FindObjectOfType<Skills>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = FindObjectOfType<GameController>();
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
        rb.AddForce(direction * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !skills.isShieldOn)
        {
            gameController.GameOver();
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "Player" && skills.isShieldOn)
        {
            Destroy(gameObject);
        }
    }
}
