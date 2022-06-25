using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCharacterHealth : MonoBehaviour
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
    public void mainCharacterTakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.Play("mainCharacterGotHit");
        if (currentHealth <= 0)
        {

            Die();
            return;
        }
    }
    void Die()
    {
        anim.Play("mainCharacterDie");
        Debug.Log("main character Die");
        this.enabled = false;
        gameObject.SetActive(false);

        return;
    }
    public int getCurrentHealthOfCharacter()
    {
        return currentHealth;
    }
}
