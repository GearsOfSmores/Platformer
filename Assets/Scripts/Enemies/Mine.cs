using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float damage;

    Collider2D firecol;

    private void Start()
    {
        firecol = this.GetComponent<Collider2D>();
    }

    public void FireON()
    {
        firecol.enabled = true;
        
    }
    public void FireOFF()
    {
        firecol.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}
