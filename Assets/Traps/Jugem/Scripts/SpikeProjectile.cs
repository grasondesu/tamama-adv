using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeProjectile : MonoBehaviour
{
    [Header("放物線設定")]
    public Vector2 initialVelocity = new Vector2(3f, 5f); // 初速度（X、Y）
    public float gravity = -9.8f; // 重力加速度（負の値）
    public float arcDuration = 1.0f; // 放物線運動を行う時間（秒）

    private float timer;
    private Vector3 startPosition;
    private bool usePhysics = false;
    private Rigidbody2D rb;

    void Start()
    {
        startPosition = transform.position;
        timer = 0f;
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.isKinematic = true; // 最初は物理無効にしてスクリプトで動かす
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void Update()
    {
        if (!usePhysics)
        {
            timer += Time.deltaTime;
            if (timer < arcDuration)
            {
                // 放物線運動の位置計算
                float x = initialVelocity.x * timer;
                float y = initialVelocity.y * timer + 0.5f * gravity * timer * timer;

                transform.position = startPosition + new Vector3(x, y, 0);

                // 回転（スピン）させる例（1秒で360度回転）
                float rotationSpeed = 360f;
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
            else
            {
                // 放物線終了、物理挙動に切り替え
                usePhysics = true;
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.linearVelocity = new Vector2(initialVelocity.x, initialVelocity.y + gravity * timer);
                }
            }
        }
    }
}
