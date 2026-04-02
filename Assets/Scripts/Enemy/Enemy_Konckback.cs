using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Konckback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemy_Movement;
    private Coroutine knockbackCoroutine;

    private void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (enemy_Movement == null) enemy_Movement = GetComponent<Enemy_Movement>();
    }

    public void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (enemy_Movement == null) enemy_Movement = GetComponent<Enemy_Movement>();
    }

    public void KonckBack(Transform forceTransform, float konckForce, float konckTime, float stunTime)
    {
        // ✅ 关键修复：非活跃物体直接拒绝执行，避免报错
        if (!gameObject.activeInHierarchy) return;

        // 空值保护
        if (rb == null || enemy_Movement == null) return;

        // 停止旧协程，避免叠加
        if (knockbackCoroutine != null)
            StopCoroutine(knockbackCoroutine);

        enemy_Movement.ChangeState(Enemystate.Konckback);
        knockbackCoroutine = StartCoroutine(KonckTimer(konckTime, stunTime));

        Vector2 direction = (transform.position - forceTransform.position).normalized;
        rb.velocity = direction * konckForce;
    }

    IEnumerator KonckTimer(float konckTime, float stunTime)
    {
        yield return new WaitForSeconds(konckTime);
        if (rb != null) rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        if (enemy_Movement != null) enemy_Movement.ChangeState(Enemystate.Idle);
        knockbackCoroutine = null;
    }

    public void ResetKnockback()
    {
        // 停止协程
        if (knockbackCoroutine != null)
        {
            StopCoroutine(knockbackCoroutine);
            knockbackCoroutine = null;
        }

        // 清空速度
        if (rb != null) rb.velocity = Vector2.zero;

        // 切回Idle
        if (enemy_Movement != null) enemy_Movement.ChangeState(Enemystate.Idle);
    }
}