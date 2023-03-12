using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    private Animator Animator;

    public float MaxHealth = 100;
    private float _currentHealth;

    void Start()
    {
        Animator = GetComponentInChildren<Animator>(); 
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log(_currentHealth);
        Animator.SetTrigger("Damaged");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Animator.SetBool("IsDead", true);

        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
