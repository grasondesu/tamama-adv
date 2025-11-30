using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    public static UIButtonSound Instance;

    [Header("常駐AudioSource")]
    [SerializeField] private AudioSource audioSource;

    [Header("クリックSE")]
    [SerializeField] private AudioClip clickSE;

    [Header("SE有効/無効")]
    [SerializeField] private bool seEnabled = true; // 初期はON

    private void Awake()
    {
        // AudioSourceがない場合は自動で追加
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // PlayerPrefsから前回の設定を読み込む
        seEnabled = PlayerPrefs.GetInt("SE_ON", 1) == 1;
    }

    /// <summary>
    /// ボタンから呼び出す用メソッド
    /// </summary>
    public void PlayClickSE()
    {
        if (!seEnabled) return; // SE無効なら何もしない

        if (audioSource != null && clickSE != null)
        {
            // 無効でも有効化して再生
            if (!audioSource.enabled) audioSource.enabled = true;
            if (!audioSource.gameObject.activeInHierarchy)
                audioSource.gameObject.SetActive(true);

            audioSource.PlayOneShot(clickSE);
        }
    }

    /// <summary>
    /// Inspector上のButtonにこの関数を登録
    /// </summary>
    public void RegisterButton(Button button)
    {
        if (button != null)
        {
            button.onClick.AddListener(PlayClickSE);
        }
    }

    /// <summary>
    /// SEの有効/無効を切り替える
    /// </summary>
    public void SetSeEnabled(bool enabled)
    {
        seEnabled = enabled;
        PlayerPrefs.SetInt("SE_ON", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }
}
