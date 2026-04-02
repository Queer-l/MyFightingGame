using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Loot : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer sr;
    public Animator anim;
    public bool isUnlocked = true;
    public static event Action<ItemSO, int> OnItemLooted;
    public int quantity;
    public bool isGot = false;
    [Header("IDºÅ")]
    public int id;
    private void OnValidate()
    {
        if (itemSO == null)
        {
            return;
        }
        sr.sprite = itemSO.icon;
        this.name = itemSO.itemName;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&isGot == false)
        {
            isGot = true;
            anim.Play("GetItem");
            OnItemLooted?.Invoke(itemSO, quantity);
            InventoryData.instance.itemStats[id] = false;
            useItemSO();
            Destroy(gameObject,0.2f);
            
        }
    }
    public void Start()
    {
        if (itemSO == null)
        {
            return;
        }
        else
        {
            if (InventoryData.instance.itemStats[id]==false)
            {
                Destroy(gameObject);
            }

        }
    }

    public void useItemSO()
    {
        StatsManager.Instance.atk += itemSO.atk_up;
        StatsManager.Instance.maxHealth += itemSO.maxhp_up;
        StatsManager.Instance.speed += itemSO.speed_up;
    }
}
