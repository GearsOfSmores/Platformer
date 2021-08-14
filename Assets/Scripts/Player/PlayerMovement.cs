using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float JumpHoldTime;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private float keyUp;
    private float keyDown;
    

    private void Awake()
    {
        //help get rigid2D and animator from game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when moving left
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Checking how long space key is press
        if (Input.GetKeyUp("space"))
        {
            keyUp = Time.time;
        }
        if (Input.GetKeyDown("space"))
        {
            keyDown = Time.time;
        }

        if (Input.GetKey(KeyCode.Space) 
            && Time.time - keyDown < JumpHoldTime
            && grounded)
            Jump();

        //set animtor parameters
        //HorizontalInput means "is 0 not eqaul to 0?" if there is no key press HI will be 0, and being false sets the run to off
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
       
        
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            grounded = false;
            anim.SetTrigger("jump");
           
       


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;

    }
  
    private bool isGrounded()
    {
        //using boxcollidor to specify the origin with our boxcast, need the (origin, and size)
        //Bounds.center is OnCollisionEnter2D point
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        // will check if there is something underneath player, will return true if something is, if not, will return false
            return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        
        return horizontalInput == 0 && isGrounded();
    }
}
