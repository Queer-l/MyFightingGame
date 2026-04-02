using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hp : MonoBehaviour
{
    public int expReward = 2;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;

    public int enemyScore = 1;


    public int currentHp;
    public int maxHp;


    private void Start()
    {
        currentHp = maxHp;
    }

    public void ChangeHp(int amount)
    {
        currentHp += amount;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        if(currentHp <= 0)
        {
            OnMonsterDefeated(expReward);
            StatsManager.Instance.score = StatsManager.Instance.score + enemyScore;
            // 回收到生成器对象池
            EnemySpawner_2D spawner = FindObjectOfType<EnemySpawner_2D>();
            if (spawner != null)
            {
                spawner.ReturnEnemyToPool(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            

        }
    }
    public void ResetHp()
    {
        currentHp = maxHp;
    }
}
