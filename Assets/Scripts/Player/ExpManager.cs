using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expGrawthMultiplier = 1.2f;  //经验增长倍数
    public Slider expSlider;
    public TMP_Text currentLevelText;

    public static event Action<int> OnLevelUp;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            GainExperience(2);
        }
    }
    private void Start()
    {
        UpdataUI();
    }

    private void OnEnable()
    {
        Enemy_Hp.OnMonsterDefeated += GainExperience;
    }

    private void OnDisable()
    {
        Enemy_Hp.OnMonsterDefeated -= GainExperience;
    }

    //经验升级
    public void GainExperience(int amount)
    {
        currentExp += amount;
        if(currentExp >= expToLevel)
        {
            LevelUp();
        }
        UpdataUI();
    }

    public void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrawthMultiplier);
        OnLevelUp?.Invoke(1);
    }

    public void UpdataUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "level: " + level;
    }
}
