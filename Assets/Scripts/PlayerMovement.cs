using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    GameController controller;

    private Rigidbody2D rb;
    private Vector2 moveDirector;
    private Animator anim;
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!controller.isOver) ProcessInputs();
        
        Vector2 fixPosition = transform.localPosition;
        if (MathF.Abs(fixPosition.y) > 4.8)
        {
            fixPosition.y = 4.8f * ((fixPosition.y < 0) ? -1 : 1);
        }
        if (MathF.Abs(fixPosition.x) > 9)
        {
            fixPosition.x = 8.7f * ((fixPosition.x < 0) ? -1 : 1);
        }
        transform.localPosition = fixPosition;
    }
    void FixedUpdate()
    {
        Move();
        Animate();
    }
    public void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        
        moveDirector = new Vector2(moveX, moveY).normalized;
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirector.x * moveSpeed, moveDirector.y * moveSpeed);

    }
    private void Animate()
    {
        anim.SetFloat("MovementX",moveDirector.x);
        anim.SetFloat("MovementY", moveDirector.y);
    }
}
