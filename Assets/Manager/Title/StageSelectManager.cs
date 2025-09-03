using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StageSelectManager : MonoBehaviour
{
    [System.Serializable]
    public class StageButton
    {
        public Button button;
        public int stageIndex;       // 1-15 の番号
    }

    [Header("コースごとのボタンパネル")]
    public GameObject[] coursePanels;       // Course1Panel, Course2Panel, Course3Panel

    [Header("ステージシーン配列")]
    public SceneAsset[] stageScenes;        // 配列の順番 = StageIndex-1

    [Header("ステージボタン一覧")]
    public StageButton[] stageButtons;      // 全ボタン（CoursePanel の子をまとめて登録）

    private int currentCourse = 0;          // 現在のコース番号（0～2）

    void Start()
    {
        currentCourse = 0;
        UpdateCourseDisplay();
        SetupButtons();
    }

    // ボタンにクリック処理を登録
    void SetupButtons()
    {
        foreach (StageButton sb in stageButtons)
        {
            bool unlocked = IsStageUnlocked(sb.stageIndex);

            sb.button.interactable = unlocked;

            sb.button.onClick.AddListener(() =>
            {
                int sceneArrayIndex = sb.stageIndex - 1;

                if (sceneArrayIndex >= 0 && sceneArrayIndex < stageScenes.Length)
                {
                    if (unlocked)
                    {
                        string sceneName = stageScenes[sceneArrayIndex].name;
                        SceneManager.LoadScene(sceneName);
                    }
                    else
                    {
                        Debug.LogWarning("ステージ " + sb.stageIndex + " はまだ解放されていません！");
                    }
                }
                else
                {
                    Debug.LogError("StageIndex " + sb.stageIndex + " に対応する SceneAsset が stageScenes 配列にありません");
                }
            });
        }
    }

    // コース切り替え
    public void NextCourse()
    {
        if (currentCourse < coursePanels.Length - 1)
        {
            currentCourse++;
            UpdateCourseDisplay();
        }
    }

    public void PreviousCourse()
    {
        if (currentCourse > 0)
        {
            currentCourse--;
            UpdateCourseDisplay();
        }
    }

    void UpdateCourseDisplay()
    {
        for (int i = 0; i < coursePanels.Length; i++)
        {
            coursePanels[i].SetActive(i == currentCourse);
        }
    }

    bool IsStageUnlocked(int stageIndex)
    {
        if (stageIndex == 1) return true;
        return PlayerPrefs.GetInt("StageCleared" + (stageIndex - 1), 0) == 1;
    }

    public void StageCleared(int stageIndex)
    {
        PlayerPrefs.SetInt("StageCleared" + stageIndex, 1);
        PlayerPrefs.Save();
    }

#if UNITY_EDITOR
    [ContextMenu("Check Stage Scenes")]
    void CheckStageScenes()
    {
        for (int i = 0; i < stageButtons.Length; i++)
        {
            if (stageScenes.Length < stageButtons[i].stageIndex)
            {
                Debug.LogWarning("StageIndex " + stageButtons[i].stageIndex + " に対応する SceneAsset が stageScenes 配列にありません");
            }
        }
    }
#endif
}
