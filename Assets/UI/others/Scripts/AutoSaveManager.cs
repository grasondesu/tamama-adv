using UnityEngine;
using UnityEngine.UI;

public class AutoSaveManager : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject settingsScreen; // 設定画面UI
    public Text debugText;            // デバッグ用Text
=======
    public GameObject settingsScreen;
    public Text debugText; // デバッグ用UI
>>>>>>> becf50895b9e12a4aadb0c007717d6629c5faf33

    private int lastClearedStage;

    private void Awake()
    {
<<<<<<< HEAD
        // このオブジェクトを全シーン共通で保持
        DontDestroyOnLoad(gameObject);

        // 重複チェック
=======
        // このオブジェクトをシーン切り替えでも消さない
        DontDestroyOnLoad(gameObject);

        // 重複オブジェクトがある場合は削除
>>>>>>> becf50895b9e12a4aadb0c007717d6629c5faf33
        AutoSaveManager[] managers = FindObjectsOfType<AutoSaveManager>();
        if (managers.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        lastClearedStage = LoadLastClearedStage();
        UpdateDebugUI();
    }

<<<<<<< HEAD
    // コースクリア時に呼ぶ
    public void OnCourseCleared(int stage)
    {
        SaveLastClearedStage(stage);
        lastClearedStage = stage;
        UpdateDebugUI();
        Debug.Log("Course cleared. Stage " + stage + " saved.");
=======
    public void OnCourseCleared(int clearedStage)
    {
        SaveLastClearedStage(clearedStage);
        lastClearedStage = clearedStage;
        UpdateDebugUI();
        Debug.Log("Course cleared. Stage " + clearedStage + " saved.");
>>>>>>> becf50895b9e12a4aadb0c007717d6629c5faf33
    }

    private void SaveLastClearedStage(int stage)
    {
        PlayerPrefs.SetInt("LastClearedStage", stage);
        PlayerPrefs.Save();
    }

    public int LoadLastClearedStage()
    {
        return PlayerPrefs.GetInt("LastClearedStage", 0);
    }

    private void UpdateDebugUI()
    {
        if (debugText != null)
            debugText.text = "Last Cleared Stage: " + lastClearedStage;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            ShowSettingsScreen();
        }
    }

    private void OnApplicationQuit()
    {
        // PlayerPrefsに保存済みなので追加処理不要
    }

    private void ShowSettingsScreen()
    {
        if (settingsScreen != null)
            settingsScreen.SetActive(true);
    }
}
