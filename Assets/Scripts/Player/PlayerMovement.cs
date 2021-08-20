using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;
    public GameObject characterHolder;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;
    public Vector3 colliderOffset;

    private SpriteRenderer spriteRend;
    // Update is called once per frame


    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        bool wasOnGround = onGround;
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (!wasOnGround && onGround)
        {
            StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }
    void FixedUpdate()
    {
        moveCharacter(direction.x);
        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }

        modifyPhysics();
    }
    void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("vertical", rb.velocity.y);
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
    }
    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            spriteRend.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            spriteRend.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }

    public bool canAttack()
    {

        return Input.GetAxis("Horizontal") == 0 && onGround;
    }






    // [SerializeField] private float speed;
    //[SerializeField] private float jumpHeight;
    //[SerializeField] private LayerMask groundLayer;
    //private Rigidbody2D body;
    // private Animator anim;
    // private bool grounded;
    // private BoxCollider2D boxCollider;
    // private float horizontalInput;


    // private void Awake()
    // {
    // //help get rigid2D and animator from game object
    //  body = GetComponent<Rigidbody2D>();
    //  anim = GetComponent<Animator>();
    //  boxCollider = GetComponent<BoxCollider2D>();
    // }

    // private void Update()
    // {
    //  horizontalInput = Input.GetAxis("Horizontal");
    //  body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

    //Flip player when moving left
    //  if (horizontalInput > 0.01f)
    //     transform.localScale = Vector3.one;
    //  else if (horizontalInput < -0.01f)
    //    transform.localScale = new Vector3(-1, 1, 1);

    //  if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
    // Jump();

    //set animtor parameters
    //HorizontalInput means "is 0 not eqaul to 0?" if there is no key press HI will be 0, and being false sets the run to off
    //  anim.SetBool("run", horizontalInput != 0);
    // anim.SetBool("grounded", isGrounded());
    //}

    // private void Jump()
    // {


    //   body.velocity = new Vector2(body.velocity.x, jumpHeight);
    //   grounded = false;
    //  anim.SetTrigger("jump");




    //}


    //private bool isGrounded()
    // {
    // //using boxcollidor to specify the origin with our boxcast, need the (origin, and size)
    // //Bounds.center is OnCollisionEnter2D point
    //  RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

    // will check if there is something underneath player, will return true if something is, if not, will return false
    //  return raycastHit.collider != null;
    // }

    //  public bool canAttack()
    // {

    // return horizontalInput == 0 && isGrounded();
    // }









}




