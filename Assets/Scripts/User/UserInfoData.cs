using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UserInfoData : MonoBehaviour
{
    public static UserInfoData  Instance;

    [Header("历史最高得分")]
    public int highestScore = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void saveNewScore(int newScore)
    {
        if(newScore > highestScore)
        {
            highestScore = newScore;
            //highestScoreText.text = "Highest:" + highestScore.ToString();
        }
    }
}
