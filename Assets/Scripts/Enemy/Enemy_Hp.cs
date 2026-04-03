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

    // ========== 掉落物品新增配置 ==========
    [Header("=== 物品掉落设置 ===")]
    [Range(0, 100)] public int dropChance = 30; // 掉落概率（百分比）
    public GameObject lootPrefab; // 拖入你的【地面拾取物品预制体】
    public Vector3 dropOffset = new Vector3(0, 0.5f,0); // 物品生成偏移
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
            TryDropItem();

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


    void TryDropItem()
    {
        Debug.Log("===== 开始执行物品掉落逻辑 =====");

        // 概率判断
        Debug.Log("掉落概率判断：当前随机值 vs 设定概率 = " + Random.Range(0, 100) + " / " + dropChance);
        if (Random.Range(0, 100) >= dropChance)
        {
            Debug.Log("===== 未触发掉落，概率不满足 =====");
            return;
        }
        Debug.Log("===== 触发物品掉落 =====");

        // 安全判断
        if (InventoryData.instance == null)
        {
            Debug.LogError("错误：InventoryData 单例为空！");
            return;
        }
        if (InventoryData.instance.itemSOPool == null || InventoryData.instance.itemSOPool.Count == 0)
        {
            Debug.LogError("错误：物品池 itemSOPool 为空！");
            return;
        }
        if (lootPrefab == null)
        {
            Debug.LogError("错误：lootPrefab 未赋值！");
            return;
        }

        Debug.Log("物品池数量：" + InventoryData.instance.itemSOPool.Count);

        // 随机物品
        ItemSO randomItem = InventoryData.instance.itemSOPool[Random.Range(0, InventoryData.instance.itemSOPool.Count)];
        Debug.Log("随机到的物品：" + randomItem.itemName);

        // 生成物品
        GameObject droppedItem = Instantiate(lootPrefab, transform.position + dropOffset, Quaternion.identity);
        Debug.Log("物品已生成：" + droppedItem.name);

        // 设置图层
        droppedItem.layer = 10;
        Debug.Log("物品图层已设置为：10");

        // 赋值物品
        Loot loot = droppedItem.GetComponent<Loot>();
        if (loot != null)
        {
            loot.itemSO = randomItem;
            Debug.Log("成功给 Loot 脚本赋值物品：" + randomItem.itemName);
        }
        else
        {
            Debug.LogError("错误：生成的物品上没有找到 Loot 脚本！");
        }

        Debug.Log("===== 掉落逻辑执行完毕 =====");
    }
    public void ResetHp()
    {
        currentHp = maxHp;
    }
}
