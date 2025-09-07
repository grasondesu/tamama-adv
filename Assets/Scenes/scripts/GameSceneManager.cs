using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // 現在のステージ番号を取得
    int GetCurrentStageIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    // リトライ（今のシーンをもう一度読み込む）
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // メインメニューへ戻る（0番を MainMenu にしておく）
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    // 次のステージへ
    public void NextStage()
    {
        int currentIndex = GetCurrentStageIndex();

        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            Debug.Log("これ以上ステージがありません");
        }
    }
}
