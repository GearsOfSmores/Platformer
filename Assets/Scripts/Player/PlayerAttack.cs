using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAttack : MonoBehaviour
{
    [Header("Fireball")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("SlickKick")]


   
    private Animator anim;
     private PlayerMovement playerMovement;
    

    //checking when the last shot was fired
    //mathf.ifnity is show that the cooldowntimer doesnt start the game at zero and not allowing the player to fire
    private float cooldownTimer = Mathf.Infinity;


    private void Awake()
    {
        //Getting the animator and playermovement from the player object
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        //check if cooldowntimer is bigger than attackcooldown
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        //incrimenting it by each frame
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");

        //when we attack the cooldowntimer is reset to zero
        cooldownTimer = 0;

        //Everytime the player attacks we reset it's position to the firePoint position
        fireballs[FindFireball()].transform.position = firePoint.position;
        //Get the fireball's Projectile component and set its direciton the player is facing calculated by using the sign of the players local scale on the x axis
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
       }
        return 0;
    }


    
}


