using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    private const float Min_Distance_To_Patrol_Point = 0.2f;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float moveSpeed;
    [SerializeField] float waitAtPoint;
    [SerializeField] float jumpForce;

    int currentPoint;
    float waitCounter;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waitCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (MoveTowardsCurrentPatrolPoint())
        {
            int direction = GetDirection();
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        }
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
