using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // リトライ：今のシーンをもう一度読み込む
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // メイン画面へ戻る（シーン名を "MainMenu" と仮定）
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("共同作成");
    }

    // 次のステージへ進む（今のシーン番号 +1）
    public void NextStage()
    {
        SceneManager.LoadScene("3-1");
    }
}

