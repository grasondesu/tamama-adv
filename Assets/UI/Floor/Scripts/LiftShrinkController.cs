using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftShrinkController : MonoBehaviour
{
    [Header("足場リスト（広い順）")]
    public List<GameObject> platforms;

    [Header("縮小間隔（秒）")]
    public float shrinkInterval = 10f;

    [Header("警告エフェクト設定")]
    public float warningTime = 2f;
    public Color warningColor = Color.red;
    public float flashRate = 0.2f;

    private int currentIndex = 0;

    void Start()
    {
        Debug.Log("===== LiftShrinkController Start =====");

        for (int i = 0; i < platforms.Count; i++)
        {
            Debug.Log($"platforms[{i}] = {platforms[i].name}");

            platforms[i].SetActive(true); // 全部アクティブに

            SetPlatformVisible(platforms[i], i == 0); // 最初の足場だけ visible=true
        }

        StartCoroutine(ShrinkRoutine());
    }

    IEnumerator ShrinkRoutine()
    {
        while (currentIndex < platforms.Count - 1)
        {
            Debug.Log($"[ShrinkRoutine] Waiting for next shrink. currentIndex={currentIndex}");

            yield return new WaitForSeconds(shrinkInterval - warningTime);

            Debug.Log($"[ShrinkRoutine] FlashWarning for next platform: index={currentIndex + 1}, name={platforms[currentIndex + 1].name}");
            yield return StartCoroutine(FlashWarning(platforms[currentIndex + 1]));

            Debug.Log($"[ShrinkRoutine] Switching platform to index={currentIndex + 1}, name={platforms[currentIndex + 1].name}");
            SwitchToPlatform(currentIndex + 1);
        }

        Debug.Log("[ShrinkRoutine] No more platforms to shrink.");
    }

    IEnumerator FlashWarning(GameObject nextPlatform)
    {
        SpriteRenderer sr = nextPlatform.GetComponent<SpriteRenderer>();
        Collider2D col = nextPlatform.GetComponent<Collider2D>();

        if (sr == null)
        {
            Debug.LogError($"❌ SpriteRenderer が見つかりません: {nextPlatform.name}");
            yield break;
        }

        Debug.Log($"[FlashWarning] Start: {nextPlatform.name} - sr.enabled={sr.enabled}");

        sr.enabled = true;
        if (col != null) col.enabled = false;

        // 1フレーム待機して描画を安定させる
        yield return null;

        Color originalColor = sr.color;
        float timer = 0f;

        while (timer < warningTime)
        {
            sr.color = warningColor;
            Debug.Log($"[FlashWarning] {nextPlatform.name} color ON");
            yield return new WaitForSeconds(flashRate);

            sr.color = originalColor;
            Debug.Log($"[FlashWarning] {nextPlatform.name} color OFF");
            yield return new WaitForSeconds(flashRate);

            timer += flashRate * 2;
        }

        sr.color = originalColor;

        Debug.Log($"[FlashWarning] End: {nextPlatform.name}");
    }

    void SwitchToPlatform(int nextIndex)
    {
        if (nextIndex >= platforms.Count)
        {
            Debug.LogWarning($"[SwitchToPlatform] nextIndex {nextIndex} is out of range.");
            return;
        }

        Debug.Log($"[SwitchToPlatform] Hiding current platform: index={currentIndex}, name={platforms[currentIndex].name}");
        SetPlatformVisible(platforms[currentIndex], false);

        Debug.Log($"[SwitchToPlatform] Showing next platform: index={nextIndex}, name={platforms[nextIndex].name}");
        SetPlatformVisible(platforms[nextIndex], true);

        currentIndex = nextIndex;
    }

    void SetPlatformVisible(GameObject platform, bool visible)
    {
        SpriteRenderer sr = platform.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.enabled = visible;
            Debug.Log($"[SetPlatformVisible] {platform.name} SpriteRenderer.enabled = {visible}");
        }

        Collider2D col = platform.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = visible;
            Debug.Log($"[SetPlatformVisible] {platform.name} Collider2D.enabled = {visible}");
        }
    }
}