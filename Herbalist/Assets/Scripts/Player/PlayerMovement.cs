using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D CharacterController;
    public Rigidbody2D Rb;
    public Animator Animator;

    public float runSpeed = 40f;

    public float DashingPower;
    public float DashingTime;
    public float DashingCooldown;
    private bool _canDash = true;
    private bool _isDash = false;

    private float horizontalMove;
    private bool jump = false;

    private void Update()
    {
        if (_isDash)
        {
            return;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            Animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Dash") && _canDash)
        {
            Animator.SetBool("IsJumping", false);
            Animator.SetTrigger("Dash");

            StartCoroutine(Dash());
        }

        Animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    private void FixedUpdate()
    {
        if (_isDash)
        {
            return;
        }

        CharacterController.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void OnLanding()
    {
        Animator.SetBool("IsJumping", false);
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDash = true;

        float originalGravity = Rb.gravityScale;
        Rb.gravityScale = 0f;
        Rb.velocity = new Vector2(transform.localScale.x * DashingPower, 0f);
        yield return new WaitForSeconds(DashingTime);

        _isDash = false;
        Rb.gravityScale = originalGravity;
        yield return new WaitForSeconds(DashingCooldown);

        _canDash = true;
    }
}
