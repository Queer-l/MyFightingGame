using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Animator anim;
    public float cooldown = 0.5f;
    public float timer ;
    public Transform attackPoint;
    public LayerMask enemyLayer;
   

    private void Update()
    {
        if(timer>0)
        {
            timer -= Time.deltaTime;
        }

    }
    public void Attack()//»Ó¿³
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);

            timer = cooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Hp>().ChangeHp(-StatsManager.Instance.atk);
            enemies[0].GetComponent<Enemy_Konckback>().KonckBack(transform, StatsManager.Instance.konckForce, StatsManager.Instance.konckTime , StatsManager.Instance.stunTime);
            SuckBlood();
        }
    }


    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }

    public void SuckBlood()
    {
        if (StatsManager.Instance.isSuckBloodUnlocked)
        {
            this.GetComponent<Player_Hp>().ChangeHealth(StatsManager.Instance.atk);
        }
    }
}
