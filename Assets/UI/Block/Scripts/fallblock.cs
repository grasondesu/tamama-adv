using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallblock : MonoBehaviour
{


    private Rigidbody2D rb;
    private bool playerOnFloor = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;  // 最初は床を動かないように設定
    }

    // プレイヤーが床に触れた時
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤータグが "Player" の場合
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnFloor = true;
            StartFalling();
        }
    }

    // プレイヤーが床から離れた時
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnFloor = false;
        }
    }

    // 床を落とす処理
    void StartFalling()
    {
        // 床が落下する準備ができたらRigidbody2Dを有効にする
        rb.isKinematic = false;

        Destroy(gameObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        // 追加で床が落ちるタイミングなどを調整したい場合に使用
    }
}
