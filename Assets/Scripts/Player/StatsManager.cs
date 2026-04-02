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
    public int currentHealth=10;
    public int maxHealth=10;

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
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            score = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public void UpdataMaxHealth( int amount)
    {
        maxHealth += amount;
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }

    public void UpdataATK(int amount)
    {
        atk += amount;
        
    }


    // 重置所有属性到初始状态（返回主界面时调用）
    public void ResetToMainMenu()
    {
        // 战斗属性
        atk = 2;
        weaponRange = 1f; // 你没给初始值，我默认设0
        konckForce = 5;
        konckTime = 0.15f;
        stunTime = 0.15f;

        // 血量
        maxHealth = 10;
        currentHealth = 10;

        // 移动速度
        speed = 5;

        // 技能解锁状态
        iscombatUnlocked = true;
        isbowUnlocked = false;
        isSuckBloodUnlocked = false;

        // 得分清零
        score = 0;

        // 更新UI显示
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth + "/" + maxHealth;
        }
    }
}
