using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float moveSpeed;
    [Header("ジャンプ速度")]
    [SerializeField] private float jumpSpeed;

    private Rigidbody2D rigidbody2D;
    private Animator anim;
    private bool jumpFlg;
    private Vector2 inputDirection;

    [Header("MainManager")]
    public MainManager MainManager;

    private float overrideDirection = 0f;
    private bool isUIInputActive = false;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpFlg = false;
    }

    private void Update()
    {
        if (MainManager != null && (MainManager.isGameCleared || MainManager.isGameOvered))
        {
            // ゲーム終了時は入力無効、Move呼ばない
            return;
        }

        Vector2 finalInput = isUIInputActive ? new Vector2(overrideDirection, 0f) : inputDirection;
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
        if (direction.x > 0f)
            transform.eulerAngles = Vector3.zero;
        else if (direction.x < 0f)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.7f) // 真下からの接触のみ
                {
                    jumpFlg = false;
                    anim.SetBool("Jump", false);
                    break;
                }
            }
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
            StartCoroutine(StopPlayerSmoothly());
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
        anim.SetBool("Jump", true);
        AudioManager.Instance.PlayJumpSE();
    }

    public void Dead()
    {
        Destroy(gameObject);
        AudioManager.Instance.PlayHitSEAndThenGameOverBGM();
        AudioManager.Instance.StopDashSE();
        // ゲームオーバーも滑らず停止（必要ならMainManager内でUI表示）
    }

    public void SetMoveDirection(float x, bool isUIInput)
    {
        overrideDirection = x;
        isUIInputActive = isUIInput;
    }

    public void JumpByButton()
    {
        if (jumpFlg) return;

        rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpFlg = true;
        anim.SetBool("Jump", true);
        AudioManager.Instance.PlayJumpSE();
    }

    private IEnumerator StopPlayerSmoothly()
    {
        // アニメーションと入力停止
        anim.enabled = false;
        GetComponent<PlayerInput>().enabled = false;

        // Rigidbody2Dを滑らかに止める
        Vector2 startVelocity = rigidbody2D.velocity;
        float duration = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            rigidbody2D.velocity = Vector2.Lerp(startVelocity, Vector2.zero, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.isKinematic = true;
    }
}