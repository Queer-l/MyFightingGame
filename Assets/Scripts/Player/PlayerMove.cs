using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public int facingDirection = 1;
    public Player_Combat player_combat;

    private bool isKonckedback;

    // 检测键盘输入
    private void Update()
    {
        if(Input.GetButton("Slash") && player_combat.enabled == true)
        {
            player_combat.Attack();
        }
    }



    void FixedUpdate()
    {



        if (isKonckedback == false) //不被击退
        {
            //水平输入
            float horizontal = Input.GetAxis("Horizontal");
            //垂直输入
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 ||
               horizontal < 0 && transform.localScale.x > 0
               )
            {
                Flip();
            }
            //刚体速度
            rb.velocity = new Vector2(horizontal, vertical) * StatsManager.Instance.speed;
            //动画控制传参
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));


        }
    }
    //翻转朝向
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1,
          transform.localScale.y, transform.localScale.z
         );

    }

    public void Konckback( Transform enemy,float force,float stunTime)
    {
        isKonckedback = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction * force;
        StartCoroutine(KonckbackCounter(stunTime));
    }
    
    IEnumerator KonckbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity = Vector2.zero;
        isKonckedback = false;
    }
}
