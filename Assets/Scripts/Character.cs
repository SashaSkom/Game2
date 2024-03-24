using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : Creature
{
    [SerializeField]
    private float speed = 3.0F;

    [SerializeField]
    private float jumpForce = 10.0F;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    public Transform groudCheck;

    private bool isGrounded = false;

    //private CharState State
    //{
    //    get { return (CharState)animator.GetInteger("State"); }
    //    set { animator.SetInteger("State", (int)value); }
    //}

    public enum CharState
    {
        Idle,
        Run,
        Jump
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x > 0.0F;

        //if (isGrounded)
        //{
        //    State = CharState.Run;
        //}
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groudCheck.position, 0.3F);

        isGrounded = colliders
            .Where(collider => collider.tag == "Ground")
            .ToArray()
            .Length > 0;

        //if (!isGrounded)
        //{
        //    State = CharState.Jump;
        //}
    }
}
