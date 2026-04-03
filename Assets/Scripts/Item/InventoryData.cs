
using System.Collections.Generic;
using UnityEngine;

// 全局唯一数据存储，跨场景不销毁
public class InventoryData : MonoBehaviour
{
    // 单例
    public static InventoryData instance;

    // 核心数据（只有这里存）
    public List<ItemSO> myItemSOs = new List<ItemSO>();
    public int[] itemCounts = new int[40];

    [Header("物品解锁状态")]
    public bool[] itemStats = new bool[40];

    [Header("道具池")]
    public List<ItemSO> itemSOPool = new List<ItemSO>();
    private void Awake()
    {
        // 单例保证唯一
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
