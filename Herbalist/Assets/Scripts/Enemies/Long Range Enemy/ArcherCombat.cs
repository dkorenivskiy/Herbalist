using UnityEngine;

public class ArcherCombat : MonoBehaviour
{
    private Animator Animator;

    public Collider2D VisionRange; 

    public Transform Player;
    private Rigidbody2D _rb;

    public GameObject Arrow;
    public Transform AttackPoint;
    public float AttackDamage = 40f;
    private bool _attack = false;

    public float AttackRate = 1f;
    private float nextAttackTime = 0f;

    public LayerMask EnemyLayer;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        if (Time.time > nextAttackTime)
        {
            _attack = false;
        }

        if (VisionRange.IsTouching(Player.GetComponent<Collider2D>()))
        {
            FlipToPlayer();

            if (Time.time > nextAttackTime)
            {
                _attack = true;
                Animator.SetTrigger("Attack");
                nextAttackTime = Time.time + AttackRate;
            }
        }
    }

    public void AttackEvent()
    {
        Instantiate(Arrow, AttackPoint.position, Quaternion.identity);
    }

    public void StopCombatEvent()
    {
        VisionRange.enabled = false;
        this.enabled = false;
    }

    private void FlipToPlayer()
    {
        if (_attack)
        {
            return;
        }

        if (transform.position.x > Player.position.x)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
