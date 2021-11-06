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

    Vector2 movement;
    bool isJump = false;
    bool isOnGround = true;

    const float groundRadius = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(CheckIfOnGround()) isJump = true;
    }

    private bool CheckIfOnGround()
    {
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, groundRadius, whatIsGround);
        return isOnGround;
    }


    private void Move()
    {
        rb.velocity= new Vector2(movement.x * moveSpeed, rb.velocity.y);
        Jump();
    }

    private void Jump()
    {
        if(isJump){
            rb.velocity= new Vector2(rb.velocity.x, jumpForce);
        }
        isJump = false;
    }


}
