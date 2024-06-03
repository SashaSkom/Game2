using System.Collections;
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
    private CharacterEvents characterEvents;
    private bool canMove = true;
    public Transform groudCheck;
    public Transform sideCheck;

    public bool isGrounded = false;
    public bool isClimbing = false;
    public bool isBreaking = false;
    public bool isFalling = false;
    public bool canHit = true;
    public int breakingCount = 0;
    public bool CanInteract { get; private set; }

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
        characterEvents = GetComponent<CharacterEvents>();
        characterEvents.enableMovements.AddListener(EnableMovementsHandler);
        characterEvents.disableMovements.AddListener(DisableMovementsHandler);
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        UpdateCanInteract();
        CheckFalling();
        CheckBreakable();
        CheckClimbing();
        CheckGround();
    }

    void Update()
    {
        if(isFalling)
        {
            Fall();
        }
        if(Input.GetButton("Fire1") && isBreaking && canHit)
        {
            Break();
        }
        if(Input.GetButtonDown("Fire2") && CanInteract) //interaction
        {
            Interact();
        }
        if(Input.GetButton("Vertical") && isClimbing && canMove)
        {
            Climb();
        }
        if (Input.GetButton("Horizontal") && canMove)
        {
            Run();
        }
        if (Input.GetButtonDown("Jump") && isGrounded && canMove)
        {
            Jump();
        }

        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
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
        animator.Play("Character_Jump");
    }

    private void Climb()
    {
        var vertical = Input.GetAxis("Vertical");

        var direction = vertical > 0.0F ? 1 : -1;

        rb.velocity = new Vector2(rb.velocity.x, speed * direction);

        animator.Play("Character_Climb");
    }

    private void Fall()
    {
        rb.velocity = new Vector2(rb.velocity.x, -5 * speed);
        animator.Play("Character_Fall");
    }

    private void Break()
    {
      animator.Play("Character_Break");
        if(breakingCount == 1)
        {
            breakingCount = 0;
            var colliders = Physics2D.OverlapCircleAll(sideCheck.position, 0.3F);
            var toDestroy = colliders.First(c => c.CompareTag("Breakable"));
            Destroy(toDestroy.gameObject);
           // toDestroy.GetComponent<SpriteRenderer>().enabled = false;
            return;
        }
        breakingCount++;
        StartCoroutine(damageTimer());
    }

    private void Interact()
    {
        Debug.Log("Interact");
        var c = Physics2D.OverlapCircleAll(sideCheck.position, 1F).First(c => c.CompareTag("Interactable"));
        c.GetComponentInParent<IInteractable>().Interact();
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groudCheck.position, 0.25F);

        isGrounded = colliders
            .Where(collider => collider.CompareTag("Ground"))
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

    private void CheckBreakable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sideCheck.position, 0.3F);

        isBreaking = colliders
            .Where(collider => collider.tag == "Breakable")
            .ToArray()
            .Length > 0; 
    }

    private void CheckFalling()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sideCheck.position, 0.3F);

        isFalling = colliders
            .Where(collider => collider.tag == "Falling")
            .ToArray()
            .Length > 0;
    }

    private IEnumerator damageTimer()
    {
        canHit = false;
        yield return new WaitForSeconds(1f);
        canHit = true;
    }

    private void EnableMovementsHandler()
    {
        canMove = true;
    }

    private void DisableMovementsHandler()
    {
        canMove = false;
    }

    private void UpdateCanInteract()
    {
        var c = Physics2D.OverlapCircleAll(sideCheck.position, 1F).Where(c => c.CompareTag("Interactable"));
        if (!c.Any())
        {
            CanInteract = false;
            return;
        }

        CanInteract =  c.First().GetComponentInParent<IInteractable>().CanInteract();
    }
}
