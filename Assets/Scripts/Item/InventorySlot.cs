using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
{
    public ItemSO itemSO;
    public int quantity;
    public Image itemImage;

    [SerializeField] private ItemInfo itemInfo;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("检测到了鼠标进入");
        if (itemSO != null)
        {
            itemInfo.ShowItemInfo(itemSO);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfo.HideItemInfo();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(itemSO != null)
        {
            itemInfo.FollowMouse();
        }
    }

    public void UpdataUI()
    {
        if(itemSO != null)
        {
            
            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true);
        }
        else
        {
            itemImage.gameObject.SetActive(false);
        }
    }
}
