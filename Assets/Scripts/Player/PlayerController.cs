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
    [SerializeField] public Animator standAnimator;
    [SerializeField] public Animator ballAnimator;
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
    public bool canMove = true;

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
    const string Is_On_Ground_Animation = "isOnGround";
    const string Move_Speed_Animation = "speed";
    const string Shooting_Animation = "shotFired";
    const string Double_Jump_Animation = "doubleJump";
    const float Ground_Radius = 0.2f;
    PlayerAbilityTracker abilities;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        abilities = GetComponent<PlayerAbilityTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            CheckIfOnGround();
            ableToDash = canDash();
            if (dashCounter > 0)
            {
                print("can dash: " + abilities.GetCanDash());
                Dash();
            }
            else
            {
                Move();
                Jump();
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        SetMoveAnimation();
        if(abilities.GetCanBecomeBall()) SetBallStatus();
    }

    public bool canDash() 
    {
        if(dashRechargeCounter > 0){
            dashRechargeCounter -= Time.deltaTime;
            return false;
        }
        if(!standing.activeSelf || !abilities.GetCanDash())
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
        if(context.performed && (isOnGround || canDoubleJump && abilities.GetCanDoubleJump())) isJump = true;
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

    private void Shoot()
    {
        if(standing.activeSelf){
            Instantiate(bulletController, shotPoint.position, shotPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
            standAnimator.SetTrigger(Shooting_Animation);
        } 
        else if (ball.activeSelf && abilities.GetCanDropBomb()) 
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
            standAnimator.SetBool(Is_On_Ground_Animation, isOnGround);
            standAnimator.SetFloat(Move_Speed_Animation, Mathf.Abs(rb.velocity.x));
        } 
        else if (ball.activeSelf)
        {
            ballAnimator.SetFloat(Move_Speed_Animation, Mathf.Abs(rb.velocity.x));
        }
    }

    private bool CheckIfOnGround()
    {
        isOnGround = Physics2D.OverlapCircle(groundPoint.position, Ground_Radius, whatIsGround);
        return isOnGround;
    }

    private void Move()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        FacePlayerTowardsMovement();
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
            canDoubleJump = CanDoubleJump();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        isJump = false;
    }

    private bool CanDoubleJump()
    {
        if (isOnGround) return true;
        standAnimator.SetTrigger(Double_Jump_Animation);
        return false;
    }
}
