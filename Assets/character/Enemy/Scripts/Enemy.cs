using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WingPoopMan : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float moveSpeed;
    [SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed;
    private Rigidbody2D rigidbody2D;
    private bool jumpFlg;
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネント参照取得
        rigidbody2D = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.left;
        jumpFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ChangeMoveDirection();
        LookMoveDirec();
    }

    private void Move()
    {
        rigidbody2D.velocity = new Vector2(moveDirection.x * moveSpeed, rigidbody2D.velocity.y);
    }

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
    private void LookMoveDirec()
    {
        if (moveDirection.x < 0.0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (moveDirection.x > 0.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "InvisibleFloor")
        {
            rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpFlg = false;
        }
    }
}