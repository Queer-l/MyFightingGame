using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask playerLayer;
    public float konckbackForce;
    public float stunTime;



    //´¥Åö
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            HitPlayer(collision);
        }
        
    }
    //´¥ÅöÍæ¼ÒÐ§¹û
    private void HitPlayer(Collision2D collision)
    {
       // collision.gameObject.GetComponent<Player_Hp>().ChangeHealth(-damage);
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if(hits.Length > 0)
        {
            hits[0].GetComponent<Player_Hp>().ChangeHealth(-damage);
            hits[0].GetComponent<PlayerMove>().Konckback(transform,konckbackForce,stunTime);
        }
    }

    public void ResetCombatState()
    {

    }
}
