using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("初回発射までの待機時間")]
    [SerializeField] private float startDelay = 1f;

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float loopInterval = 2f;

    [Header("落下開始までの遅延（オプション）")]
    [SerializeField] private float hangTime = 0.5f;

    private Rigidbody2D rb;
    private bool isLooping = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(LoopJump());
    }

    private IEnumerator LoopJump()
    {
        // ✅ 最初の待機時間（ゲームスタート直後の遅延）
        yield return new WaitForSeconds(startDelay);

        while (isLooping)
        {
            // 上向きにジャンプ
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // 任意のホバリング時間がある場合
            if (hangTime > 0)
            {
                yield return new WaitForSeconds(hangTime);
                rb.gravityScale = 1f;
            }

            // 次のジャンプまで待機
            yield return new WaitForSeconds(loopInterval);
        }
    }
}
