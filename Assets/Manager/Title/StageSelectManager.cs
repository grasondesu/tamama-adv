using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StageButton
{
    public Button button;
    public int stageIndex;   // 1から始まるステージ番号
}

public class StageSelectManager : MonoBehaviour
{
    [Header("コースごとのボタンパネル")]
    public GameObject[] coursePanels;       // Course1Panel, Course2Panel, ...

    [Header("ステージシーン名配列")]
    public string[] stageSceneNames;        // 配列の順番 = stageIndex - 1

    [Header("ステージボタン一覧")]
    public StageButton[] stageButtons;      // 全ボタン（CoursePanel の子をまとめて登録）

    private int currentCourse = 0;          // 現在のコース番号（0～）

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
            int index = sb.stageIndex;  // クロージャ対策でローカルにコピー

            sb.button.onClick.AddListener(() =>
            {
                int sceneArrayIndex = index - 1;
                if (sceneArrayIndex >= 0 && sceneArrayIndex < stageSceneNames.Length)
                {
                    SceneManager.LoadScene(stageSceneNames[sceneArrayIndex]);
                }
                else
                {
                    Debug.LogError("StageIndex " + index + " に対応するシーン名が stageSceneNames 配列にありません");
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
}
