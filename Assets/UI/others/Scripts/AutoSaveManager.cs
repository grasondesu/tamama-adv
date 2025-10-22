using UnityEngine;
using UnityEngine.UI;

public class AutoSaveManager : MonoBehaviour
{
    public GameObject settingsScreen;
    public Text debugText; // デバッグ用UI

    private int lastClearedStage;

    private void Awake()
    {
        // このオブジェクトをシーン切り替えでも消さない
        DontDestroyOnLoad(gameObject);

        // 重複オブジェクトがある場合は削除
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

    public void OnCourseCleared(int clearedStage)
    {
        SaveLastClearedStage(clearedStage);
        lastClearedStage = clearedStage;
        UpdateDebugUI();
        Debug.Log("Course cleared. Stage " + clearedStage + " saved.");
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
