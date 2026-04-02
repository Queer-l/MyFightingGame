using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public SkillSlot[] skillSlots;
    public TMP_Text pointsText;
    


    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(slot.TryUpgradeSkill);
        }
        UpdataAblityPoints(0);
    }


    public void UpdataAblityPoints(int amount)
    {
        StatsManager.Instance.availablePoints += amount;
        pointsText.text = "Points:" + StatsManager.Instance.availablePoints;
    }

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointsSpent;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdataAblityPoints;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointsSpent;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdataAblityPoints;
    }

    private void HandleAbilityPointsSpent(SkillSlot skillSlot)
    {
        if(StatsManager.Instance.availablePoints > 0)
        {
            UpdataAblityPoints(-1);
        }
    }

    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach (SkillSlot slot in skillSlots)
        {
            if(!slot.isUnlocked&&slot.CanUnlockedSkill())
            {
                slot.Unlock();
            }
        }

    }
}
