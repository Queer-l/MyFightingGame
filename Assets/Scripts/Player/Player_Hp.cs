using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Hp : MonoBehaviour
{
    public Animator healthAnim;


    public TMP_Text healthText;
    private void Start()
    {
        FreshHealth();
    }
    private void FreshHealth()
    {
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + "/ " + StatsManager.Instance.maxHealth;
    }
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;

        healthAnim.Play("TextUpdata");
        if(StatsManager.Instance.currentHealth> StatsManager.Instance.maxHealth)
        {
            StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        }
        if (StatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        FreshHealth();
    }
}
