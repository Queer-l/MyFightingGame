using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsMenuCotroller : MonoBehaviour
{
    // 开始游戏按钮点击事件
    public void OnExitGameClicked()
    {
        // 跳转到StartScene场景（确保场景已在Build Settings中添加）
        SceneManager.LoadScene("StartScene");

        UserInfoData.Instance.saveNewScore(StatsManager.Instance.score);

        if (StatsManager.Instance != null)
        {
            StatsManager.Instance.ResetToMainMenu();
        }

    }
}
