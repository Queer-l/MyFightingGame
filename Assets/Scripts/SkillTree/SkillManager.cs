using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player_Combat combat;
    public Player_Bow bow;
    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
    }
    public void HandleAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;

        switch (skillName)
        {
            case "MaxHealthUp":
                StatsManager.Instance.UpdataMaxHealth(1);
                break;
            case "Sword Slash":
                StatsManager.Instance.iscombatUnlocked = true;
                break;
            case "Bow":
                StatsManager.Instance.isbowUnlocked = true;
                break;
            case "ATK_Up":
                StatsManager.Instance.UpdataATK(1);
                break;
            case "SuckBlood":
                StatsManager.Instance.isSuckBloodUnlocked = true;
                break;

            default:
                UnityEngine.Debug.LogWarning("UnKown Skill"+ skillName);
                break;
        }
    }
}
