using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemystate
{
    Idle,
    Chasing,
    Attacking,
    Konckback
}

public class Enemy_Movement : MonoBehaviour
{
    public float attackRange = 2;
    public float speed = 3;
    public int test;
    public float attackCD = 3;
    public float derectionDistance = 5;
    public Transform derectionPoint;
    public LayerMask playerLayer;

    private Animator anim;
    private Enemystate enemyState;
    private int facingDirection = 1;
    private Transform player;
    private Rigidbody2D rb;
    private float attackCDTimer;

    // 组件获取移到OnEnable，保证每次激活都能拿到
    private void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
        ChangeState(Enemystate.Idle);
    }

    void Start()
    {
        // 保留Start做初始化兜底
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();
        ChangeState(Enemystate.Idle);
    }

    void Update()
    {
        if (enemyState != Enemystate.Konckback)
        {
            CheckForPlayer();

            if (attackCDTimer > 0)
            {
                attackCDTimer -= Time.deltaTime;
            }

            if (enemyState == Enemystate.Chasing)
            {
                Chase();
            }
            else if (enemyState == Enemystate.Attacking)
            {
                if (rb != null) rb.velocity = Vector2.zero;
                Attack();
            }
        }
    }

    private void Chase()
    {
        if (enemyState == Enemystate.Chasing && player != null)
        {
            if (player.position.x > transform.position.x && facingDirection == -1 ||
                player.position.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }

            Vector2 dirction = (player.position - transform.position).normalized;
            rb.velocity = dirction * speed;
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1,
          transform.localScale.y, transform.localScale.z
         );
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(derectionPoint.position, derectionDistance, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;

            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCDTimer <= 0)
            {
                attackCDTimer = attackCD;
                ChangeState(Enemystate.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != Enemystate.Attacking)
            {
                ChangeState(Enemystate.Chasing);
            }
        }
        else
        {
            ChangeState(Enemystate.Idle);
            if (rb != null) rb.velocity = new Vector2(0, 0);
            player = null;
        }
    }

    public void Attack()
    {
        Debug.Log("attack");
    }

    public void ChangeState(Enemystate state)
    {
        if (anim == null) return; // 空值保护

        if (enemyState == Enemystate.Idle)
        {
            anim.SetBool("isidle", false);
        }
        if (enemyState == Enemystate.Chasing)
        {
            anim.SetBool("ismoving", false);
        }
        if (enemyState == Enemystate.Attacking)
        {
            anim.SetBool("isattacking", false);
        }

        enemyState = state;

        if (enemyState == Enemystate.Idle)
        {
            anim.SetBool("isidle", true);
        }
        if (enemyState == Enemystate.Chasing)
        {
            anim.SetBool("ismoving", true);
        }
        if (enemyState == Enemystate.Attacking)
        {
            anim.SetBool("isattacking", true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (derectionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(derectionPoint.position, derectionDistance);
        }
    }

    // 
    public void ResetMovement()
    {
        // 空值保护，避免空引用
        if (rb != null) rb.velocity = Vector2.zero;
        attackCDTimer = 0;
        player = null;

        // ✅ 修复：直接调用自身的ChangeState，不需要额外变量
        ChangeState(Enemystate.Idle);

        if (anim != null)
        {
            anim.SetBool("isidle", true);
            anim.SetBool("ismoving", false);
            anim.SetBool("isattacking", false);
        }

        enemyState = Enemystate.Idle;
    }
}
