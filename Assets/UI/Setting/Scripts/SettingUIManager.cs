using UnityEngine;
using UnityEngine.UI;

public class SettingUIManager : MonoBehaviour
{
    [Header("設定パネル")]
    [SerializeField] private GameObject mainPanel; // 設定メイン
    [SerializeField] private GameObject bgmPanel;  // BGM調整
    [SerializeField] private GameObject tosPanel;  // TOS調整（旧SEPanel）

    void Start()
    {
        // 設定パネルは初期非表示
        mainPanel.SetActive(false);
        bgmPanel.SetActive(false);
        tosPanel.SetActive(false);
    }

    // 設定ボタン押下 → メイン設定パネル表示
    public void OpenMainPanel()
    {
        mainPanel.SetActive(true);
        bgmPanel.SetActive(false);
        tosPanel.SetActive(false);
        mainPanel.transform.SetAsLastSibling(); // 前面に
    }

    // BGMボタン押下
    public void OpenBGMPanel()
    {
        mainPanel.SetActive(false);
        bgmPanel.SetActive(true);
        tosPanel.SetActive(false);
        bgmPanel.transform.SetAsLastSibling(); // 前面に
    }

    // TOSボタン押下
    public void OpenTOSPanel()
    {
        mainPanel.SetActive(false);
        bgmPanel.SetActive(false);
        tosPanel.SetActive(true);
        tosPanel.transform.SetAsLastSibling(); // 前面に
    }

    // 設定パネルの戻るボタン押下
    public void ClosePanels()
    {
        mainPanel.SetActive(false);
        bgmPanel.SetActive(false);
        tosPanel.SetActive(false);
    }
}


