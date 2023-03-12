using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public HealthScript HealthBar;

    public Animator Animator;

    public float MaxHealth = 100;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        HealthBar.SetHealth(_currentHealth);
        Animator.SetBool("IsJumping", false);
        Animator.SetTrigger("Damaged");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Animator.SetBool("IsDead", true);

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        this.enabled = false;
    }
}
