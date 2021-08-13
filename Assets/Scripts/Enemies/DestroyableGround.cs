using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableGround : MonoBehaviour
{
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private SpriteRenderer spriteRend;
    

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }


    private bool triggered;
    private bool active;
      
      
    
   private void OnCollisionEnter2D(Collision2D col)
    {


        if (col.gameObject.tag == "Player")
        {
            if (!triggered)
                StartCoroutine(DestroyGround());
            if (active)
                Destroy(gameObject);
        }
        
        
   }

    private IEnumerator DestroyGround()
    {

        triggered = true;
        spriteRend.color = Color.red;

        //Wait for delay, activate trap, turn on animation
        yield return new WaitForSeconds(activationDelay);
        active = true;
        spriteRend.color = Color.white;
        Destroy(gameObject);
        yield return new WaitForSeconds(activeTime);
        
        triggered = false;

    }
    

}
