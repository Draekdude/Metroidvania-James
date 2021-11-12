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
    [SerializeField] Animator standAnimator;
    [SerializeField] Animator ballanimator;
    [SerializeField] BulletController bulletController;
    [SerializeField] Transform shotPoint;
    [SerializeField] float dashSpeed, dashTime;
    [SerializeField] SpriteRenderer mainSpriteRend;
    [SerializeField] SpriteRenderer afterSpriteRend;
    [SerializeField] float afterImageLifetime;
    [SerializeField] float timeBetweenAfterImages;
    [SerializeField] Color afterImageColor;
    [SerializeField] float waitAfterDashing;
    [SerializeField] GameObject standing;
    [SerializeField] GameObject ball;
    [SerializeField] float waitToBall;
    [SerializeField] Transform bombPoint;
    [SerializeField] GameObject bomb;


    bool isBallActivating;
    bool isBallDeactivating;
    float ballCounter;
    float dashRechargeCounter;
    float afterImageCounter;
    bool ableToDash;
    Vector2 movement;
    bool isJump = false;
    bool isOnGround;
    bool canDoubleJump;
    float dashCounter;
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
        ableToDash = canDash();
        if (dashCounter > 0)
        {
            Dash();
        }
        else
        {
            Move();
            Jump();
        }
        SetBallStatus();
    }

    private void SetBallStatus()
    {
        if (isBallActivating || isBallDeactivating)
        {
            ballCounter -= Time.deltaTime;
            if (ballCounter <= 0)
            {
                if (isBallActivating)
                {
                    ball.SetActive(true);
                    standing.SetActive(false);
                }
                else if (isBallDeactivating)
                {
                    ball.SetActive(false);
                    standing.SetActive(true);
                }
            }
        }
    }

    public bool canDash() 
    {
        if(dashRechargeCounter > 0){
            dashRechargeCounter -= Time.deltaTime;
            return false;
        }
        if(!standing.activeSelf)
        {
            return false;
        }
        return true;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.performed && ableToDash) 
        {
            dashCounter = dashTime;
            ShowAfterImage();
        }
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

    public void OnBallActivate(InputAction.CallbackContext context)
    {
        if(context.started && !ball.activeSelf) {
            isBallActivating = true;
            ballCounter = waitToBall;
        }
        if(context.canceled) {
            isBallActivating = false;
        }
    }

    public void OnBallDeactivate(InputAction.CallbackContext context)
    {
        if(context.started && ball.activeSelf) {
            isBallDeactivating = true;
            ballCounter = waitToBall;
        }
        if(context.canceled) {
            isBallDeactivating = false;
        }
    }

    public void ShowAfterImage()
    {
        SpriteRenderer image = Instantiate(afterSpriteRend, transform.position, transform.rotation);
        image.sprite = mainSpriteRend.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;
        Destroy(image.gameObject, afterImageLifetime);
        afterImageCounter = timeBetweenAfterImages;
    }

    private void Shoot()
    {
        if(standing.activeSelf){
            Instantiate(bulletController, shotPoint.position, shotPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
            standAnimator.SetTrigger(shootingAnimation);
        } 
        else if (ball.activeSelf) 
        {
            Instantiate(bomb, bombPoint.transform.position, bombPoint.transform.rotation);
        }
    }

    private void Dash()
    {
        dashCounter -= Time.deltaTime;
        rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);
        DashEffect();
        dashRechargeCounter = waitAfterDashing;
    }

    private void DashEffect()
    {
        afterImageCounter -= Time.deltaTime;
        if(afterImageCounter <= 0) ShowAfterImage();
    }

    private void SetMoveAnimation()
    {
        if(standing.activeSelf){
            standAnimator.SetBool(isOnGroundAnimation, isOnGround);
            standAnimator.SetFloat(moveSpeedAnimation, Mathf.Abs(rb.velocity.x));
        } 
        else if (ball.activeSelf)
        {
            ballanimator.SetFloat(moveSpeedAnimation, Mathf.Abs(rb.velocity.x));
        }
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
        SetMoveAnimation();
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
        standAnimator.SetTrigger(doubleJumpAnimation);
        return false;
    }
}
