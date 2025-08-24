using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed;
    [SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed;

    private Rigidbody2D rigidbody2D;
    private bool jumpFlg;

    private Vector2 inputDirection; // キーボードやゲームパッドからの入力
    private float overrideDirection = 0f; // UI入力用の上書き値
    private bool isUIInputActive = false; // UI入力が有効かどうか

    private Animator anim;

    [SerializeField, Header("MainManager")]
    public MainManager MainManager;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        jumpFlg = false;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 finalInput;

        // UI入力があれば優先
        if (isUIInputActive)
        {
            finalInput = new Vector2(overrideDirection, 0f);
        }
        else
        {
            finalInput = inputDirection;
        }

        Move(finalInput);
        LookMoveDirec(finalInput);
    }

    private void Move(Vector2 direction)
    {
        rigidbody2D.velocity = new Vector2(direction.x * moveSpeed, rigidbody2D.velocity.y);
        anim.SetBool("Run", Mathf.Abs(direction.x) > 0.1f);

        if (Mathf.Abs(direction.x) > 0.1f && !jumpFlg)
        {
            AudioManager.Instance.PlayDashSE();
        }
        else
        {
            AudioManager.Instance.StopDashSE();
        }
    }

    private void LookMoveDirec(Vector2 direction)
    {
        if (direction.x > 0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction.x < 0.0f)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            jumpFlg = false;
            anim.SetBool("Jump", false);
        }
        else if (collision.gameObject.CompareTag("Enemy") ||
                 collision.gameObject.CompareTag("Arrow") ||
                 collision.gameObject.CompareTag("DeathLine"))
        {
            AudioManager.Instance.PlayHitSE();
            Dead();
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            MainManager.ShowGameClearUI();
            enabled = false;
            GetComponent<PlayerInput>().enabled = false;
            anim.enabled = false;
        }
    }

    // キーボードやゲームパッドからの移動入力
    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    // キーボードやゲームパッドからのジャンプ入力
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || jumpFlg) return;

        rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpFlg = true;
        anim.SetBool("Jump", true);

        AudioManager.Instance.PlayJumpSE();
    }

    public void Dead()
    {
        Destroy(gameObject);
        AudioManager.Instance.PlayHitSEAndThenGameOverBGM();
        AudioManager.Instance.StopDashSE();
    }

    // UIButtonManagerから呼ばれる（UIボタン用の移動入力）
    public void SetMoveDirection(float x, bool isUIInput)
    {
        overrideDirection = x;
        isUIInputActive = isUIInput;
    }

    // UIButtonManagerから呼ばれる（UIボタン用ジャンプ）
    public void JumpByButton()
    {
        if (jumpFlg) return;

        rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpFlg = true;
        anim.SetBool("Jump", true);

        AudioManager.Instance.PlayJumpSE();
    }
}

