using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameSceneManager : MonoBehaviour
{
    [Header("ステージシーン配列")]
    public SceneAsset[] stageScenes; // 配列の順番 = StageIndex-1

    // 現在のステージ番号を取得
    int GetCurrentStageIndex()
    {
        string currentName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < stageScenes.Length; i++)
        {
            if (stageScenes[i].name == currentName)
                return i + 1; // StageIndex = 配列インデックス+1
        }
        Debug.LogWarning("現在のシーンは stageScenes 配列に登録されていません: " + currentName);
        return -1;
    }

    // リトライ：今のシーンをもう一度読み込む
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // メイン画面へ戻る（SceneAsset 配列に MainMenu を追加しておく）
    public void GoToMainMenu()
    {
        if (stageScenes.Length > 0)
        {
            SceneManager.LoadScene(stageScenes[0].name); // stageScenes[0] を MainMenu にする
        }
        else
        {
            Debug.LogWarning("stageScenes 配列が空です");
        }
    }

    // 次のステージへ進む
    public void NextStage()
    {
        int currentIndex = GetCurrentStageIndex();
        if (currentIndex < 0) return;

        if (currentIndex < stageScenes.Length)
        {
            SceneManager.LoadScene(stageScenes[currentIndex].name); // 次のステージ = currentIndex +1
        }
        else
        {
            Debug.Log("これ以上ステージがありません");
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Check Stage Scenes")]
    void CheckStageScenes()
    {
        if (stageScenes == null || stageScenes.Length == 0)
        {
            Debug.LogWarning("stageScenes 配列が空です");
        }
    }
#endif
}
