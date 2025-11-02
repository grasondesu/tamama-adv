using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [Header("メインパネル（タイトルなど）")]
    [SerializeField] private GameObject mainPanel;

    [Header("設定パネル（パネルA）")]
    [SerializeField] private GameObject settingsPanel;

    [Header("音量パネル（パネルB）")]
    [SerializeField] private GameObject volumePanel;

    [Header("コース選択シーン名")]
    [SerializeField] private string courseSelectSceneName = "CourseSelectScene";

    // ポーズ状態の管理
    private bool isPaused = false;

    void Start()
    {
        // 最初は設定・音量パネルを非表示
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (volumePanel != null) volumePanel.SetActive(false);

        // ゲームは最初から動作中
        isPaused = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        // パネルAまたはパネルBが開いている場合は停止
        if ((settingsPanel != null && settingsPanel.activeSelf) ||
            (volumePanel != null && volumePanel.activeSelf))
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    // ========================================================
    // 設定パネル（パネルA）を開く
    // ========================================================
    public void OpenSettingsPanel()
    {
        if (settingsPanel != null) settingsPanel.SetActive(true);
        if (volumePanel != null) volumePanel.SetActive(false);
        isPaused = true; // ゲームを一時停止
        Debug.Log("設定パネルを開きました。");
    }

    // ========================================================
    // 音量パネル（パネルB）を開く
    // ========================================================
    public void OpenVolumePanel()
    {
        if (volumePanel != null) volumePanel.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        isPaused = true; // 継続して停止中
        Debug.Log("音量パネルを開きました。");
    }

    // ========================================================
    // コース選択画面へ遷移
    // ========================================================
    public void GoToCourseSelect()
    {
        Debug.Log("コース選択画面へ遷移します。");
        Time.timeScale = 1f; // シーン切り替え時は時間を戻す
        SceneManager.LoadScene(courseSelectSceneName);
    }

    // ========================================================
    // 設定パネルを閉じてメインパネルへ戻る
    // ========================================================
    public void CloseSettingsPanel()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (volumePanel != null) volumePanel.SetActive(false);
        isPaused = false; // ゲームを再開
        Debug.Log("設定を閉じ、メイン画面に戻りました。");
    }

    // ========================================================
    // 現在ポーズ中かを他スクリプトから参照するための関数
    // ========================================================
    public bool IsPaused()
    {
        return isPaused;
    }
}
