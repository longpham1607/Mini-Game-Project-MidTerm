using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHeath = 100;
    int currentHealth;
    public Animator anim;
    public Transform player;
    public bool isFlipped = false;
    public HealthBar healthBar;
    void Start()
    {
        currentHealth = maxHeath;
        anim = GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHeath);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.Play("get_hurt");
        healthBar.SetHealth(currentHealth);
        if (currentHealth<=0)
        {
            Die();
            Application.LoadLevel("Level2");
        }
    }
    void Die()
    {
        anim.Play("die");
        Debug.Log("enemy Die");
        this.enabled = false;
        gameObject.SetActive(false);
        
    }


    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
