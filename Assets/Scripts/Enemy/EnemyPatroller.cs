using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    const float Min_Distance_To_Patrol_Point = 0.2f;
    const float Horizontal_Offset = 0.5f;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float moveSpeed;
    [SerializeField] float waitAtPoint;
    [SerializeField] float jumpForce;
    [SerializeField] Animator anim;
    int currentPoint;
    float waitCounter;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waitCounter = waitAtPoint;

        foreach (var patrolPoint in patrolPoints)
        {
            patrolPoint.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        anim.SetFloat(AnimationNames.Move_Speed_Animation, Mathf.Abs(rb.velocity.x));
    }

    private void Move()
    {
        if (MoveTowardsCurrentPatrolPoint())
        {
            int direction = GetDirection();
            SetToFaceMovingDirection(direction);
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
            if (transform.position.y < patrolPoints[currentPoint].position.y - Horizontal_Offset && IsNotJumping())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else
        {
            WaitAtPatrolPoint();
        }
    }

    private bool IsNotJumping()
    {
        return rb.velocity.y < 0.1f;
    }

    private void WaitAtPatrolPoint()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        waitCounter -= Time.deltaTime;
        if (waitCounter <= 0)
        {
            waitCounter = waitAtPoint;
            SetNewPatrolPoint();
        }
    }

    private void SetNewPatrolPoint()
    {
        currentPoint++;
        if (currentPoint >= patrolPoints.Length)
        {
            currentPoint = 0;
        }
    }

    private void SetToFaceMovingDirection(int direction)
    {
        transform.localScale = new Vector3(-direction, 1f, 1f);
    }

    private bool MoveTowardsCurrentPatrolPoint()
    {
        return Mathf.Abs(transform.position.x - patrolPoints[currentPoint].transform.position.x) > Min_Distance_To_Patrol_Point;
    }

    private int GetDirection()
    {
        int direction = 1;
        if (transform.position.x > patrolPoints[currentPoint].transform.position.x)
        {
            direction = -1;
        }

        return direction;
    }
}
