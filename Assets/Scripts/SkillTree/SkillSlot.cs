using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillSO skillSO;
    public List<SkillSlot> prerequisiteSkillSlots;
    public int currentLevel;
    public bool isUnlocked;


    public Image skillIcon;
    public TMP_Text skillLevelText;
    public Button skillButton;

    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;
    private void OnValidate()
    {
        if (skillSO != null && skillLevelText!=null)
        {
            UpdataUI();
        }  
    }

    private void UpdataUI()
    {
        skillIcon.sprite = skillSO.skillIcon;

        if(isUnlocked)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.grey;
        }

    }

    public void TryUpgradeSkill()
    {
        if(isUnlocked&&currentLevel < skillSO.maxLevel && StatsManager.Instance.availablePoints > 0)
        {
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this);
            if(currentLevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);
            }
            UpdataUI();
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
        UpdataUI();
    }

    public bool CanUnlockedSkill()
    {
        foreach (SkillSlot slot in prerequisiteSkillSlots)
        {
            if (!slot.isUnlocked || slot.currentLevel < slot.skillSO.maxLevel)
            {
                return false;
            }
        }

        return true;
    }
}

