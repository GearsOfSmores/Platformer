using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;




    [Header("Iframes")]
    [SerializeField] private float IframesDuration;
    [SerializeField] private int numberOfFlashes;
     public SpriteRenderer spriteRend;
    

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();



    }

    private void Update()
    {
        if (transform.position.y < -15)
        {

            SceneManager.LoadScene("LoseScreen");
        }
    }
    public void TakeDamage(float _damage)
    {
        //make sure healh does not go bellow 0
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                SceneManager.LoadScene("LoseScreen");
            }


        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    //adds invulnerability when hurt for a duration of time
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        //invulnerability duration

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IframesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(IframesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }


}  
