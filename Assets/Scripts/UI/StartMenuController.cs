using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    // 开始游戏按钮点击事件
    public void OnStartGameClicked()
    {
        // 跳转到GameScene场景（确保场景已在Build Settings中添加）
        SceneManager.LoadScene("GameScene");
    }

    // 退出游戏按钮点击事件
    public void OnExitGameClicked()
    {
        // 编辑器中点击会停止运行，打包后会关闭应用
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        Debug.Log("游戏退出");
    }
}