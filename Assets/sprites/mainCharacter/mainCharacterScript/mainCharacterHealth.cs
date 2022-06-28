using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCharacterHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHeath = 100;
    int currentHealth;
    public Animator anim;
    // Thanh máu
    public HealthBar healthBar;
    void Start()
    {
        currentHealth = maxHeath;
        anim = GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHeath);
    }

    // Update is called once per frame
    public void mainCharacterTakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.Play("mainCharacterGotHit");
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {

            Die();
            Application.LoadLevel("menu");
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
