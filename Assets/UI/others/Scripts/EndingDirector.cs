using UnityEngine;
using System.Collections;

public class EndingDirector : MonoBehaviour
{
    [SerializeField] GameObject congratulations;
    [SerializeField] TypewriterText thankYou;
    [SerializeField] GameObject titleButton;

    void Start()
    {
        StartCoroutine(EndingSequence());
    }

    IEnumerator EndingSequence()
    {
        // Congratulation 表示済み（虹色は常時）
        yield return new WaitForSecondsRealtime(0.8f);

        // タイプライター開始
        yield return StartCoroutine(thankYou.Play());

        // ボタン表示
        titleButton.SetActive(true);
    }
}
