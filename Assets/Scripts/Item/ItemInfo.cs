using UnityEngine;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptipnText;
    public TMP_Text moreDescriptipnText;

    private RectTransform infoPanelRect;
    private bool isShow = false;

    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
        HideItemInfo();
    }

    private void Update()
    {
        if (isShow)
            FollowMouse(); // 只要显示，就每帧跟随
    }

    public void ShowItemInfo(ItemSO itemSO)
    {
        isShow = true;
        infoPanel.alpha = 1;

        itemNameText.text = itemSO.itemName;
        itemDescriptipnText.text = itemSO.itemDescription;
        moreDescriptipnText.text = itemSO.moreDescription;

        FollowMouse();
    }

    public void HideItemInfo()
    {
        isShow = false;
        infoPanel.alpha = 0;
    }

    public void FollowMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        infoPanelRect.position = mousePos + new Vector2(20, -20);
    }
}