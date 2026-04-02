using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetHighestScore : MonoBehaviour
{

    [Header("历史最高得分")]
    public int highestScore = 0;
    public TMP_Text highestScoreText;
    // Start is called before the first frame update
    void Start()
    {
        highestScore = UserInfoData.Instance.highestScore;
        highestScoreText.text = "Highest:" + highestScore.ToString();
    }

  
}
