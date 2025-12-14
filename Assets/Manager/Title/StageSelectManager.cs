using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StageButton
{
    public Button button;
    public int stageIndex;   // 1から始まるステージ番号
    
    // ★ステージごとに用意するロック画像
    public GameObject lockImage; 
}

public class StageSelectManager : MonoBehaviour
{
    // PlayerPrefsで使用するキー (ステージの最大開放レベルを保存)
    private const string MAX_LEVEL_KEY = "MaxLevel"; 

    [Header("コースごとのボタンパネル")]
    // ★修正: シンプルなGameObject配列に戻す
    public GameObject[] coursePanels;       // Course1Panel, Course2Panel, ...

    [Header("ステージシーン名配列")]
    public string[] stageSceneNames;        // 配列の順番 = stageIndex - 1

    [Header("ステージボタン一覧")]
    // ★全15個のボタンをここに登録
    public StageButton[] stageButtons;      

    private int currentCourse = 0;          // 現在のコース番号（0～）

    void Start()
    {
        currentCourse = 0;
        
        // 1. ステージロック状態の更新
        UpdateStageLockStates();
        
        // 2. コースパネル表示の更新
        UpdateCourseDisplay();
        
        // 3. ボタンにクリック処理を登録
        SetupButtons();
    }
    
    /// <summary>
    /// PlayerPrefsの値に基づき、ステージボタンのロック状態を更新する
    /// </summary>
    void UpdateStageLockStates()
    {
        // PlayerPrefsから、クリア済みの最大ステージ番号+1 を取得 (初期値はステージ1が開放されているとして 1)
        int maxLevelUnlocked = PlayerPrefs.GetInt(MAX_LEVEL_KEY, 1); 

        foreach (StageButton sb in stageButtons)
        {
            // 現在のボタンのステージ番号が、開放済みの最大レベル以下であれば true
            bool isUnlocked = sb.stageIndex <= maxLevelUnlocked;
            
            // ボタンの操作可否を設定
            sb.button.interactable = isUnlocked;
            
            // ロック画像の表示/非表示を設定
            if (sb.lockImage != null)
            {
                // アンロックされていなければロック画像を表示
                sb.lockImage.SetActive(!isUnlocked); 
            }
        }
    }

    // ボタンにクリック処理を登録
    void SetupButtons()
    {
        foreach (StageButton sb in stageButtons)
        {
            int index = sb.stageIndex;

            sb.button.onClick.AddListener(() =>
            {
                int sceneArrayIndex = index - 1;
                if (sceneArrayIndex >= 0 && sceneArrayIndex < stageSceneNames.Length)
                {
                    // ★ご自身のカスタムSceneManagerを使用する場合は以下のように修正してください
                    // SceneManager.Instance.LoadScene(stageSceneNames[sceneArrayIndex]);
                    
                    // 組み込みのSceneManagerを使用
                    SceneManager.LoadScene(stageSceneNames[sceneArrayIndex]);
                }
                else
                {
                    Debug.LogError("StageIndex " + index + " に対応するシーン名が stageSceneNames 配列にありません");
                }
            });
        }
    }

    // コース切り替え (Next/PreviousCourse のロジックは変更なし)
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
    
    /* ----------------------------------------------------
     * 【重要】ステージクリア時に MainManager から呼び出すメソッド 
     * ---------------------------------------------------- */
    
    /// <summary>
    /// ステージクリア時に、次のステージの開放をPlayerPrefsに保存する
    /// </summary>
    /// <param name="currentStageIndex">現在クリアしたステージの番号 (1から始まる)</param>
    public static void UnlockNextStage(int currentStageIndex)
    {
        int nextStage = currentStageIndex + 1;
        int maxLevelUnlocked = PlayerPrefs.GetInt(MAX_LEVEL_KEY, 1); 

        // 現在の最大開放レベルよりも、クリアした次のステージ番号が大きい場合のみ更新
        if (nextStage > maxLevelUnlocked)
        {
            PlayerPrefs.SetInt(MAX_LEVEL_KEY, nextStage);
        }

        PlayerPrefs.Save();
    }
}