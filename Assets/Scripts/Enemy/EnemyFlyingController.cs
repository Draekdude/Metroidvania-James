using System.Runtime.ExceptionServices;
using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingController : MonoBehaviour
{
    const string Is_Attacking = "isAttacking";
    [SerializeField] Animator animator;
    [SerializeField] float attackDistance;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealthController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        bool isAttacking = IsAttacking();
        animator.SetBool(Is_Attacking, isAttacking);
        if (isAttacking)
        {
            SetAttackRotation();
            SetMovement();
        }
    }

    private void SetMovement()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        transform.position += -transform.right * moveSpeed * Time.deltaTime;
    }

    private void SetAttackRotation()
    {
        Vector3 direction = transform.position - target.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private bool IsAttacking()
    {
        if (!target.gameObject.activeSelf) return false;
        if (Vector3.Distance(transform.position, target.position) > attackDistance) return false;
        return true;
    }
}
