using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressStartMovenment : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;





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
        //body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

     

       // if (Input.GetKey(KeyCode.Space) && isGrounded())
            //Jump();

        moveCharacter(direction.x);
        modifyPhysics();

        
        //anim.SetBool("run", horizontalInput != 0);
        //anim.SetBool("grounded", isGrounded());
    }
    void moveCharacter(float horizontal)
    {
        body.AddForce(Vector2.right * horizontal * moveSpeed);

        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }
        if (Mathf.Abs(body.velocity.x) > maxSpeed)
        {
            body.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }
    private void Jump()
    {


        //body.velocity = new Vector2(body.velocity.x, jumpHeight);
        //grounded = false;
       // anim.SetTrigger("jump");




    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Ground")
        // grounded = true;

    }

   // private bool isGrounded()
   // {
        //using boxcollidor to specify the origin with our boxcast, need the (origin, and size)
        //Bounds.center is OnCollisionEnter2D point
        //RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        // will check if there is something underneath player, will return true if something is, if not, will return false
       // return raycastHit.collider != null;
   // }

    //public bool canAttack()
    //{

       //return horizontalInput == 0 && isGrounded();
   // }
}

