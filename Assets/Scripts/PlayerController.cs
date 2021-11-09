using System.Runtime.InteropServices.WindowsRuntime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] Transform groundPoint;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Animator animator;
    [SerializeField] BulletController bulletController;
    [SerializeField] Transform shotPoint;

    Vector2 movement;
    bool isJump = false;
    bool isOnGround;
    bool canDoubleJump;

    const string isOnGroundAnimation = "isOnGround";
    const string moveSpeedAnimation = "speed";
    const string shootingAnimation = "shotFired";

    const string doubleJumpAnimation = "doubleJump";
    const float groundRadius = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfOnGround();
        Move();
        SetAnimation();
    }

    private void SetAnimation()
    {
        animator.SetBool(isOnGroundAnimation, isOnGround);
        animator.SetFloat(moveSpeedAnimation, Mathf.Abs(rb.velocity.x));
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && (isOnGround || canDoubleJump)) isJump = true;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed) Shoot();
    }

    private void Shoot()
    {
        Instantiate(bulletController, shotPoint.position, shotPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
        animator.SetTrigger(shootingAnimation);
    }

    private bool CheckIfOnGround()
    {
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsGround);
        return isOnGround;
    }


    private void Move()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        FacePlayerTowardsMovement();
        Jump();
    }

    private void FacePlayerTowardsMovement()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void Jump()
    {
        if(isJump)
        {
            canDoubleJump = SetDoubleJump();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        isJump = false;
    }

    private bool SetDoubleJump()
    {
        if (isOnGround) return true;
        animator.SetTrigger(doubleJumpAnimation);
        return false;
    }
}
