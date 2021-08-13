
using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        

    }

    private void Update()
    {

        //direction is added to allow us to determine which direction the fireball will fly
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;

        //using the transform.translate to move object on the x axis
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checking to see if fireball hit any object
        hit = true;
        boxCollider.enabled = false;

        //setting the explode animation by setting the explode trigger
        anim.SetTrigger("explode");
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;

        //so name is not equal to local variable
        direction = _direction;


        // to reset the state of the fireball
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
       
        float localScaleX = transform.localScale.x;

        //checking if the sign of localScalex is not equal to direction
        if (Mathf.Sign(localScaleX) != _direction)

            //flipping the direction
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
