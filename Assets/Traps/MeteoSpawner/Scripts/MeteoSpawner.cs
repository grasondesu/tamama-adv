using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSpawner : MonoBehaviour
{
    [Header("隕石プレハブと設定")]
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnX = 10f; // 右側から出すX位置
    [SerializeField] private float targetVelocity = -5f;

    [Header("出現Y位置（交互）")]
    [SerializeField] private float[] spawnHeights = { 1f, 4f };
    private int heightIndex = 0;

    [Header("出現トリガー設定")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float triggerX = 5f; // このX位置を超えると開始

    private float timer;
    private bool isSpawning = false;

    void Update()
    {
        // ✅ ゲームオーバーまたはゲームクリアなら発射しない
        if (MainManager.Instance != null &&
            (MainManager.Instance.isGameOvered || MainManager.Instance.isGameCleared))
        {
            return;
        }
        // まだ開始していなければ、プレイヤーの位置を監視
        if (!isSpawning && playerTransform.position.x >= triggerX)
        {
            isSpawning = true;
            timer = 0f; // リセットしてすぐに1発目が出るように
        }

        // スポーン中ならタイマー進行
        if (isSpawning)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnMeteor();
            }
        }
    }

    private void SpawnMeteor()
    {
        float spawnY = spawnHeights[heightIndex];
        Vector2 spawnPos = new Vector2(spawnX, spawnY);

        GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(targetVelocity, 0f); // 横方向だけ速度をセット
        }

        heightIndex = (heightIndex + 1) % spawnHeights.Length;
    }
}
