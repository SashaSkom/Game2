using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : Creature
{
    [SerializeField]
    private float speed = 4.0F;

    [SerializeField]
    private float jumpForce = 7.0F;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    public Transform groudCheck;
    public Transform sideCheck;

    public bool isGrounded = false;
    public bool isClimbing = false;

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
        CheckClimbing();
        CheckGround();
    }

    void Update()
    {
        if(Input.GetButton("Vertical") && isClimbing)
        {
            Climb();
        }
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
        var horizontal = Input.GetAxis("Horizontal");

        sprite.flipX = horizontal > 0.0F;

        var direction = sprite.flipX ? 1 : -1;

        rb.velocity = new Vector2(speed * direction, rb.velocity.y);

        //if (isGrounded)
        //{
        //    State = CharState.Run;
        //}
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Climb()
    {
        var vertical = Input.GetAxis("Vertical");

        var direction = vertical > 0.0F ? 1 : -1;

        rb.velocity = new Vector2(rb.velocity.x, speed * direction);
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

    private void CheckClimbing()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sideCheck.position, 0.3F);

        isClimbing = colliders
            .Where(collider => collider.tag == "Climbing")
            .ToArray()
            .Length > 0;
    }
}
