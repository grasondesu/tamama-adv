using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryPlayerCloud : MonoBehaviour
{
    [Header("Movement Settings")]
    public float descendSpeed = 2f;           // 下がる速度
    public float moveSpeed = 2f;              // 右に進む速度
    public float delayBeforeFall = 0.1f;      // 着地後の遅延時間
    public float waitAtBottom = 0.5f;         // 下がり切った後の停止時間

    [Header("Target Positions")]
    public float targetY = 0f;                // 下がり切るY座標
    public float targetX = 50f;               // 右に進む目標X座標

    private bool playerLanded = false;
    private bool descending = false;
    private bool movingRight = false;

    private float contactTime = 0f;
    private float requiredContactTime = 0.05f;

    // プレイヤーが乗ったときの処理
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // プレイヤーを床の子にして追従させる
            collision.transform.SetParent(this.transform);

            if (!playerLanded)
            {
                contactTime += Time.deltaTime;

                if (contactTime >= requiredContactTime)
                {
                    playerLanded = true;
                    Invoke("StartDescending", delayBeforeFall);
                }
            }
        }
    }

    // プレイヤーが離れたときの処理
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // プレイヤーの親子関係を解除して自由に動けるようにする
            collision.transform.SetParent(null);

            if (!playerLanded)
            {
                contactTime = 0f;
            }
        }
    }

    void StartDescending()
    {
        descending = true;
    }

    void StartMovingRight()
    {
        movingRight = true;
    }

    void Update()
    {
        if (descending)
        {
            if (transform.position.y > targetY + 0.01f)
            {
                transform.position += Vector3.down * descendSpeed * Time.deltaTime;
            }
            else
            {
                descending = false;
                transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
                Invoke("StartMovingRight", waitAtBottom);
            }
        }
        else if (movingRight)
        {
            if (transform.position.x < targetX - 0.01f)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            else
            {
                movingRight = false;
                transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
            }
        }
    }
}
