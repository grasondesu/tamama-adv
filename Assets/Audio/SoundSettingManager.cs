using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingManager : MonoBehaviour
{
    [Header("BGM設定")]
    [SerializeField] private Button bgmButton;
    [SerializeField] private Image bgmImage;
    [SerializeField] private Sprite bgmOnSprite;
    [SerializeField] private Sprite bgmOffSprite;

    [Header("SE設定")]
    [SerializeField] private Button seButton;
    [SerializeField] private Image seImage;
    [SerializeField] private Sprite seOnSprite;
    [SerializeField] private Sprite seOffSprite;

    [Header("パネル閉じる用")]
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject panelToClose;

    private bool isBgmOn = true;
    private bool isSeOn = true;

    private const string BGM_PREF_KEY = "BGM_ON";
    private const string SE_PREF_KEY = "SE_ON";

    private void Awake()
    {
        // 保存していた設定を読み込み
        isBgmOn = PlayerPrefs.GetInt(BGM_PREF_KEY, 1) == 1;
        isSeOn = PlayerPrefs.GetInt(SE_PREF_KEY, 1) == 1;

        UpdateBgmVisual();
        UpdateSeVisual();

        // 状態に応じてAudioManagerへ反映
        ApplyBgmState();
        ApplySeState();

        // ボタン登録
        if (bgmButton != null)
            bgmButton.onClick.AddListener(ToggleBGM);

        if (seButton != null)
            seButton.onClick.AddListener(ToggleSE);

        if (backButton != null && panelToClose != null)
            backButton.onClick.AddListener(() => panelToClose.SetActive(false));
    }

    #region BGM
    private void ToggleBGM()
    {
        isBgmOn = !isBgmOn;
        PlayerPrefs.SetInt(BGM_PREF_KEY, isBgmOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateBgmVisual();
        ApplyBgmState(); // ← AudioManagerへ反映
    }

    private void UpdateBgmVisual()
    {
        if (bgmImage != null)
            bgmImage.sprite = isBgmOn ? bgmOnSprite : bgmOffSprite;
    }

    private void ApplyBgmState()
    {
        if (AudioManager.Instance == null) return;

        if (isBgmOn)
        {
            // ONのとき、必要に応じて再生（例：コースBGM）
            AudioManager.Instance.PlayCourseBGM();
        }
        else
        {
            // OFFなら停止
            AudioManager.Instance.StopBGM();
        }
    }
    #endregion

    #region SE
    private void ToggleSE()
    {
        isSeOn = !isSeOn;
        PlayerPrefs.SetInt(SE_PREF_KEY, isSeOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateSeVisual();
        ApplySeState(); // ← AudioManagerへ反映
    }

    private void UpdateSeVisual()
    {
        if (seImage != null)
            seImage.sprite = isSeOn ? seOnSprite : seOffSprite;
    }

    private void ApplySeState()
    {
        if (AudioManager.Instance == null) return;

        // AudioManagerに現在の状態を教えておく
        AudioManager.Instance.SetSeEnabled(isSeOn);
    }
    #endregion
}

