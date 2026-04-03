using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsMenuCotroller : MonoBehaviour
{
    // 开始游戏按钮点击事件
    public void OnExitGameClicked()
    {
        Debug.Log("按钮被点击了！");
        UserInfoData.Instance.saveNewScore(StatsManager.Instance.score);
        Time.timeScale = 1;
        // 跳转到StartScene场景（确保场景已在Build Settings中添加）
        SceneManager.LoadScene("StartScene");

        

    }
}
