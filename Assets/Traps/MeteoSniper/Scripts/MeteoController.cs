using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoController : MonoBehaviour
{
    [Header("回転設定")]
    [SerializeField] private float minRotationSpeed = -360f; // 最小回転速度
    [SerializeField] private float maxRotationSpeed = 360f;  // 最大回転速度

    [Header("衝突判定")]
    [SerializeField] private string groundTag = "Floor";     // 地面のタグ
    [SerializeField] private string playerTag = "Player";

    private float rotationSpeed;

    void Start()
    {
        // ランダムな回転速度を決定
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    void Update()
    {
        // 回転処理（Z軸まわりにクルクル）
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag) || collision.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject);
        }
    }
}
