using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn = 1.5f;
    public float speed;
    public int damage;
    public float konckbackForce;
    public float konckbackTime;
    public float stunTime;
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    public SpriteRenderer sr;
    public Sprite buriedSprite;

    // Start is called before the first frame update
    void Start()
    {
        
        rb.velocity = direction * speed;
        RotateArrow();
        Destroy(gameObject, lifeSpawn);
    }

   public void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        damage = StatsManager.Instance.atk;
        if ((enemyLayer.value&(1 << collision.gameObject.layer))>0)
        {
            collision.gameObject.GetComponent<Enemy_Hp>().ChangeHp(-damage);
            collision.gameObject.GetComponent<Enemy_Konckback>().KonckBack(transform, konckbackForce, konckbackTime, stunTime);
        }
        else if((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            AttachToTarget(collision.gameObject.transform);
        }
    }

    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite;

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        transform.SetParent(target);
    }
}
