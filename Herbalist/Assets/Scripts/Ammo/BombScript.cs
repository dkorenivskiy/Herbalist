using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;

    public float ExplosionRange = 2f;
    public LayerMask EnemyLayers;

    public float Damage = 50f;
    public float Force = 4f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.transform.localScale.x == 1)
        {
            _rb.velocity = (Vector2.right + Vector2.up) * Force;
        }
        else
        {
            _rb.velocity = (Vector2.left + Vector2.up) * Force;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitOther)
    {
        EnemyHP enemy = hitOther.GetComponent<EnemyHP>();
        if (enemy != null)
        {
            _animator.SetTrigger("Explosion");
            _rb.bodyType = RigidbodyType2D.Static;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, ExplosionRange, EnemyLayers);
            foreach (Collider2D enemies in hitEnemies)
            {
                enemies.GetComponent<EnemyHP>().TakeDamage(Damage);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D hitOther)
    {
        if (hitOther.gameObject.tag == "Gameplay Ground")
        {
            _animator.SetTrigger("Explosion");
            _rb.bodyType = RigidbodyType2D.Static;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, ExplosionRange, EnemyLayers);
            foreach (Collider2D enemies in hitEnemies)
            {
                try
                {
                    enemies.GetComponent<EnemyHP>().TakeDamage(Damage);
                }
                catch
                {
                    enemies.GetComponent<PlayerHP>().TakeDamage(Damage);
                }
            }
        }
    }

    public void DestroyObjectEvent()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRange);
    }
}
