using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed;
    [SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed;

    private Rigidbody2D rigidbody2D;
    private bool jumpFlg;

    private Vector2 inputDirection;
    private Animator anim;

    [SerializeField, Header("MainManager")]
    public MainManager MainManager;

    void Start()
    {
        // コンポーネント参照取得
        rigidbody2D = GetComponent<Rigidbody2D>();
        jumpFlg = false;
        anim = GetComponent<Animator>();
    }

    // Update（1フレームごとに1度ずつ実行）
    void Update()
    {
        Move();
        LookMoveDirec();

    }

    private void Move()
    {
        rigidbody2D.velocity = new Vector2(inputDirection.x * moveSpeed, rigidbody2D.velocity.y);
        anim.SetBool("Run", inputDirection.x != 0.0f);

        if (Mathf.Abs(inputDirection.x) > 0.1f && !jumpFlg)
        {
            AudioManager.Instance.PlayDashSE();
        }
        else
        {
            // 止まった or ジャンプ中 → 足音を止める
            AudioManager.Instance.StopDashSE();
        }
    }

    private void LookMoveDirec()
    {
        if (inputDirection.x > 0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (inputDirection.x < 0.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpFlg = false;
            anim.SetBool("Jump", jumpFlg);
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Arrow" || collision.gameObject.tag == "DeathLine")
        {
            // ヒットSE再生
            AudioManager.Instance.PlayHitSE();
            Dead();
        }
        else if (collision.gameObject.tag == "Goal")
        {
            MainManager.ShowGameClearUI();
            enabled = false;
            GetComponent<PlayerInput>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || jumpFlg) return;

        rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpFlg = true;
        anim.SetBool("Jump", jumpFlg);

        // ジャンプSEを再生
        AudioManager.Instance.PlayJumpSE();
    }

    public void Dead()
    {
        // プレイヤーを削除
        Destroy(gameObject);
        // 死亡時ヒット音を鳴らしてヒット音が鳴り終わったらゲームオーバーBGM再生
        AudioManager.Instance.PlayHitSEAndThenGameOverBGM();
        // 念のため足音を止めておく（確実）
        AudioManager.Instance.StopDashSE();
    }

}