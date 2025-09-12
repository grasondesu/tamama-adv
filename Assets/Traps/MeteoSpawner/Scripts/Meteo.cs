using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    [Header("回転設定")]
    [SerializeField] private float minRotationSpeed = -360f;
    [SerializeField] private float maxRotationSpeed = 360f;

    [Header("衝突で破壊するタグ一覧")]
    [SerializeField] private string[] destroyTags = { "Ground", "MeteoDestroyWall", "Player" };

    private float rotationSpeed;

    void Start()
    {
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    void Update()
    {
        // 回転処理
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // ✅ ゲームクリア or オーバーなら自壊
        if (MainManager.Instance != null &&
            (MainManager.Instance.isGameCleared || MainManager.Instance.isGameOvered))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (string tag in destroyTags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                Destroy(gameObject);
                break;
            }
        }
    }
}
