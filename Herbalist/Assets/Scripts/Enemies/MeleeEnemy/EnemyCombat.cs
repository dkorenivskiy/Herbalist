using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Animator Animator;

    public Collider2D Vision;

    public Transform Player;
    private Rigidbody2D _rb;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public float AttackDamage = 40f;
    private bool _attack = false;

    public float AttackRate = 0.5f;
    private float nextAttackTime = 0f;

    public LayerMask EnemyLayer;

    public float Speed = 1f;
    private float _resetSpeed;

    private bool MovingRight = true;

    public Transform GroundDetection;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        _resetSpeed = Speed;
    }

    private void FixedUpdate()
    {
        if (Time.time > nextAttackTime)
        {
            _attack = false;
        }

        if (Vision.IsTouching(Player.GetComponent<Collider2D>()))
        {
            FlipToPlayer();

            Vector2 target = new Vector2(Player.position.x, _rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(_rb.position, target, Speed * Time.fixedDeltaTime);
            _rb.MovePosition(newPos);

            if (Vector2.Distance(Player.position, _rb.position) <= 2 * AttackRange)
            {
                if (Time.time > nextAttackTime)
                {
                    Speed = 0f;
                    _attack = true;
                    Animator.SetTrigger("Attack");
                    nextAttackTime = Time.time + AttackRate;
                }
            }
        }
        else
        {
            Patrolling();
        }
    }

    public void AttackEvent()
    {
        try
        {
            Collider2D hitEnemy = Physics2D.OverlapCircle(AttackPoint.position, AttackRange, EnemyLayer);
            hitEnemy.GetComponent<PlayerHP>().TakeDamage(AttackDamage);
        }
        catch
        {
            //TODO: UI animation text miss
            Debug.Log("Miss!!!");
        }
    }

    public void StartRunEvent()
    {
        Speed = _resetSpeed;
    }

    public void StopCombatEvent()
    {
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

    private void Patrolling()
    {
        transform.Translate(Vector2.right * Speed * Time.fixedDeltaTime);

        RaycastHit2D Ground = Physics2D.Raycast(GroundDetection.position, Vector2.down, 0.2f);

        if (Ground.collider == false)
        {
            if (MovingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                MovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                MovingRight = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
