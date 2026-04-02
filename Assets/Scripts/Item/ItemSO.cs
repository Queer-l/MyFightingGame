using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite icon;

    [Header("MoreDescription")]
    [TextArea] public string moreDescription;

    [Header("ŒÔ∆∑ Ù–‘")]
    public int atk_up;
    public int maxhp_up;
    public int speed_up;


}
