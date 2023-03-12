using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator Animator;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public float AttackDamage = 20f;
    private float _nextAttackTime = 0f;
    private float _attackRate = 0.5f;

    public GameObject Bomb;
    private float _nextThrowTime = 0f;
    private float _throwCooldown = 3f;

    public LayerMask EnemyLayers;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > _nextAttackTime)
        {
            Attack();
            _nextAttackTime = Time.time + _attackRate;
        }

        if (Input.GetButtonDown("Fire2") && Time.time > _nextThrowTime)
        {
            Throw();
            _nextThrowTime = Time.time + _throwCooldown;
        }
    }

    private void Attack()
    {
        Animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.name);

            enemy.GetComponent<EnemyHP>().TakeDamage(AttackDamage);
        }
    }

    private void Throw()
    {
        Instantiate(Bomb, AttackPoint.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
