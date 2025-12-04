using System.Collections.Generic;
using UnityEngine;

public class MeteoController : MonoBehaviour
{
    [Header("回転設定")]
    [SerializeField] private float minRotationSpeed = -360f;
    [SerializeField] private float maxRotationSpeed = 360f;

    [Header("衝突判定")]
    [SerializeField] private string groundTag = "Floor";
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string enemyTag = "Enemy";

    [Header("破壊対象のオブジェクト名リスト")]
    [SerializeField] private List<string> destroyObjectNames;

    private float rotationSpeed;

    void Start()
    {
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 床またはプレイヤーに衝突したら破壊
        if (collision.gameObject.CompareTag(groundTag) || collision.gameObject.CompareTag(playerTag) || collision.gameObject.CompareTag(enemyTag))
        {
            Destroy(gameObject);
            return;
        }

        // 名前で判定
        foreach (var name in destroyObjectNames)
        {
            if (collision.gameObject.name.StartsWith(name))
            {
                Destroy(gameObject);
                Debug.Log($"Destroyed by collision with {collision.gameObject.name}");
                return;
            }
        }
    }
}
