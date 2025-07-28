using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugemController : MonoBehaviour
{
    [Header("ターゲット")]
    [SerializeField] private Transform player;

    [Header("追従設定")]
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private float baseY = 8f; // ベースY座標

    [Header("ふわふわ浮遊設定")]
    [SerializeField] private float floatAmplitude = 0.5f; // 上下の振れ幅
    [SerializeField] private float floatSpeed = 2f;       // 揺れの速さ

    [Header("棘の投下設定")]
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private float dropInterval = 2f;

    private float dropTimer;
    private float floatTimer;

    void Update()
    {
        // ✅ ゲームクリア時は何もしない
        if (MainManager.Instance != null && MainManager.Instance.isGameCleared)
        {
            return;
        }

        // 横方向追尾（Yはあとで決める）
        float targetX = Mathf.Lerp(transform.position.x, player.position.x, followSpeed * Time.deltaTime);

        // Y座標をふわふわ揺らす
        floatTimer += Time.deltaTime;
        float floatOffset = Mathf.Sin(floatTimer * floatSpeed) * floatAmplitude;
        float currentY = baseY + floatOffset;

        transform.position = new Vector3(targetX, currentY, transform.position.z);

        // 棘落としタイマー
        dropTimer += Time.deltaTime;
        if (dropTimer >= dropInterval)
        {
            DropSpike();
            dropTimer = 0f;
        }
    }

    private void DropSpike()
    {
        if (spikePrefab != null && dropPoint != null)
        {
            Instantiate(spikePrefab, dropPoint.position, Quaternion.identity);
        }
    }

    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
    }
}
