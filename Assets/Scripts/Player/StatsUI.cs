using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup statsCanvas;

    private bool statsOpen = false;

    private void Start()
    {
        UpdataAllStats();
    }

    private void Update()
    {
        UpdataAllStats();
    }


    public void UpdataAtk()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = " ATK: " + StatsManager.Instance.atk;
    }
    public void UpdataSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = " SPEED: " + StatsManager.Instance.speed;
    }

    public void UpdataAllStats()
    {
        UpdataAtk();
        UpdataSpeed();
    }
}
