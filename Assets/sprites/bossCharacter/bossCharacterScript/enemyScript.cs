using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHeath = 100;
    int currentHealth;
    public Animator anim;
    void Start()
    {
        currentHealth = maxHeath;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.Play("get_hurt");
        if (currentHealth<=0)
        {
            Die();
        }
    }
    void Die()
    {
        anim.Play("die");
        Debug.Log("enemy Die");
        this.enabled = false;
    }
}
