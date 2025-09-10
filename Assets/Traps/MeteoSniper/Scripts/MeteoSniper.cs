using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSniper : MonoBehaviour
{
    [Header("回転設定")]
    [SerializeField] private float rotationSpeed = 90f;

    [Header("発射設定")]
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private float fireInterval = 2.0f;
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private Transform firePoint; // 発射位置（空オブジェクトなど）
    [SerializeField] private Transform playerTarget; // プレイヤーのTransform

    private float timer;

    void Update()
    {
        // ✅ ゲームオーバーまたはゲームクリアなら発射しない
        if (MainManager.Instance != null &&
            (MainManager.Instance.isGameOvered || MainManager.Instance.isGameCleared))
        {
            return;
        }
        // 回転
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // タイマー更新
        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            timer = 0f;
            FireMeteor();
        }
    }

    private void FireMeteor()
    {
        if (meteorPrefab != null && firePoint != null && playerTarget != null)
        {
            // プレイヤーへの方向ベクトルを取得・正規化
            Vector2 dir = (playerTarget.position - firePoint.position).normalized;

            // 隕石を生成して方向へ飛ばす
            GameObject meteor = Instantiate(meteorPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = dir * shootForce;
            }
        }
    }
}
