using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float invulerableTime;
    Collider2D firecol;
    private float collideTime;


    private void Start()
    {
        firecol = this.GetComponent<Collider2D>();
    }
    private void Update()
    {
        
    }

    public void FireON()
    {
        firecol.enabled = true;
        
    }
    public void FireOFF()
    {
        firecol.enabled = false;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            collideTime = collideTime * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}
