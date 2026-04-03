using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // 开始游戏按钮点击事件
    public void OnStartGameClicked()
    {
        // 跳转到GameScene场景（确保场景已在Build Settings中添加）
        SceneManager.LoadScene("GameScene");
    }
}
