using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;


    public TMP_Text healthText;

    [Header("Cambat Stats")]
    public int atk = 4;
    public float weaponRange = 1;
    public float konckForce = 5;
    public float konckTime = .15f;
    public float stunTime = .15f;
    
    [Header("HP Stats")]
    public int currentHealth = 10;
    public int maxHealth = 10;

    [Header("Movement Stats")]
    public float speed = 5;

    [Header("技能解锁状态")]
    public bool iscombatUnlocked = true;
    public bool isbowUnlocked = false;
    public bool isSuckBloodUnlocked = false;
    [Header("技能点数")]
    public int availablePoints;
    [Header("当前得分")]
    public int score = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            score = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void UpdataMaxHealth(int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }

    public void UpdataATK(int amount)
    {
        atk += amount;

    }


}