using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("背包UI格子")]
    public InventorySlot[] itemSlots;
    private InventoryData data;

    private void Start()
    {
        RefreshInventory(); // 统一初始化
    }

    // 统一刷新整个背包
    public void RefreshInventory()
    {
        data = InventoryData.instance;
        ClearAllSlots(); // 先清空

        // 把全局数据里的物品重新刷到UI
        foreach (var item in data.myItemSOs)
        {
            AddItemToEmptySlot(item, 1);
        }
    }

    // 清空所有UI格子
    void ClearAllSlots()
    {
        foreach (var slot in itemSlots)
        {
            slot.itemSO = null;
            slot.quantity = 0;
            slot.UpdataUI();
        }
    }

    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;

        // 切换场景后 自动刷新背包
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 场景加载完成 → 强制刷新UI
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        RefreshInventory();
    }

    // 外部调用：添加物品
    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (data == null) data = InventoryData.instance;
        if (!data.myItemSOs.Contains(itemSO))
        {
            data.myItemSOs.Add(itemSO);
        }

        AddItemToEmptySlot(itemSO, quantity);
    }

    // 内部：把物品放到空的UI格子
    void AddItemToEmptySlot(ItemSO itemSO, int quantity)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.itemSO == null)
            {
                slot.itemSO = itemSO;
                slot.quantity = quantity;
                slot.UpdataUI();
                return;
            }
        }
    }

    // 刷新所有UI
    public void RefreshAllUI()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdataUI();
        }
    }
}
