using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private GameObject _player;

    private Rigidbody2D _rb;

    public float Damage = 10f;
    public float Force = 4f;

    public float FlyTime = 2f;
    private float _time2Disable;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();

        _time2Disable = Time.time + FlyTime;

        Vector3 direction = _player.transform.position - transform.position;
        _rb.velocity = new Vector2(direction.x, direction.y).normalized * Force;

        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void FixedUpdate()
    {
        if(Time.time >= _time2Disable)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitOther)
    {
        PlayerHP player = hitOther.GetComponent<PlayerHP>();
        if(player != null)
        {
            player.TakeDamage(Damage);
        }

        if(hitOther.tag == "Player" || hitOther.tag == "Gameplay Ground")
            Destroy(gameObject);
    }
}
