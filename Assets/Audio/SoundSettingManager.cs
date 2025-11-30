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

    private bool isBgmOn;
    private bool isSeOn;

    private void Awake()
    {
        isBgmOn = PlayerPrefs.GetInt("BGM_ON", 1) == 1;
        isSeOn = PlayerPrefs.GetInt("SE_ON", 1) == 1;

        UpdateBgmVisual();
        UpdateSeVisual();

        bgmButton.onClick.AddListener(ToggleBGM);
        seButton.onClick.AddListener(ToggleSE);
        if (backButton != null && panelToClose != null)
            backButton.onClick.AddListener(() => panelToClose.SetActive(false));
    }

    private void ToggleBGM()
    {
        isBgmOn = !isBgmOn;
        AudioManager.Instance.SetBgmEnabled(isBgmOn);
        UpdateBgmVisual();
    }

    private void ToggleSE()
    {
        isSeOn = !isSeOn;
        AudioManager.Instance.SetSeEnabled(isSeOn);
        UIButtonSound[] allButtons = FindObjectsOfType<UIButtonSound>(true);
        foreach (var btn in allButtons)
        {
            btn.SetSeEnabled(isSeOn);
        }
        UpdateSeVisual();
    }

    private void UpdateBgmVisual()
    {
        if (bgmImage != null)
            bgmImage.sprite = isBgmOn ? bgmOnSprite : bgmOffSprite;
    }

    private void UpdateSeVisual()
    {
        if (seImage != null)
            seImage.sprite = isSeOn ? seOnSprite : seOffSprite;
    }
}
