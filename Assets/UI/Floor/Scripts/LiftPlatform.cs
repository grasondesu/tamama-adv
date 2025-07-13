using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftPlatform : MonoBehaviour
{
    [Header("Lift Settings")]
    public float liftSpeed = 2f;       // 上昇速度
    public float targetY = 10f;        // 上昇終了Y座標
    public bool autoStart = true;      // ゲーム開始と同時に上昇するか

    private bool isMovingUp = false;

    void Start()
    {
        if (autoStart)
        {
            isMovingUp = true;
        }
    }

    void Update()
    {
        if (isMovingUp)
        {
            if (transform.position.y < targetY - 0.01f)
            {
                transform.position += Vector3.up * liftSpeed * Time.deltaTime;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
                isMovingUp = false;
            }
        }
    }

    // 外部から呼び出せる開始関数
    public void StartLift()
    {
        isMovingUp = true;
    }

    // プレイヤーを子オブジェクトにして一緒に運ぶ
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    // プレイヤーが離れたら親子解除
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
