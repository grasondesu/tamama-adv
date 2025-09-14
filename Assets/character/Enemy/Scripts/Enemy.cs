using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WingPoopMan : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed = 3f;

    [SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed = 5f;

    private Rigidbody2D rigidbody2D;
    private bool jumpFlg; // ジャンプ済みフラグ
    private Vector2 moveDirection;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.left;
        jumpFlg = false;
    }

    void Update()
    {
        Move();
        ChangeMoveDirection();
        LookMoveDirec();
    }

    // 左右移動
    private void Move()
    {
        rigidbody2D.velocity = new Vector2(moveDirection.x * moveSpeed, rigidbody2D.velocity.y);
    }

    // 壁に当たったら方向を反転
    private void ChangeMoveDirection()
    {
        Vector2 halfSize = transform.lossyScale / 2.0f;
        int layerMask = LayerMask.GetMask("TransparentWall");
        RaycastHit2D ray = Physics2D.Raycast(transform.position, -transform.right, halfSize.x + 0.4f, layerMask);
        if (ray.transform == null) return;
        if (ray.transform.tag == "TransparentWall")
        {
            moveDirection = -moveDirection;
        }
    }

    // 向きを進行方向に合わせる
    private void LookMoveDirec()
    {
        if (moveDirection.x < 0.0f)
            transform.eulerAngles = Vector3.zero;
        else if (moveDirection.x > 0.0f)
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }

    // 着地時のジャンプ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Floor" || collision.gameObject.tag == "InvisibleFloor") && !jumpFlg)
        {
            // Y方向の速度をリセットしてからジャンプ
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

            jumpFlg = true; // この接地でジャンプ済みにする
        }
    }

    // 空中に出たらジャンプフラグをリセット
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "InvisibleFloor")
        {
            jumpFlg = false;
        }
    }
}
