using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    [Header("常駐AudioSource")]
    [SerializeField] private AudioSource audioSource;

    [Header("クリックSE")]
    [SerializeField] private AudioClip clickSE;

    private void Awake()
    {
        // AudioSourceがない場合は自動で追加
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// ボタンから呼び出す用メソッド
    /// </summary>
    public void PlayClickSE()
    {
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
}