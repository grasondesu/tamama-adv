using UnityEngine;

public class DropFloor : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤー以外は無視
        if (!collision.gameObject.CompareTag("Player")) return;

        // プレイヤーに触れたら落下開始
        if (!hasFallen)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            hasFallen = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // プレイヤー以外の物体と当たっても物理的に無視
        if (!collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    private void Update()
    {
        // 一定の高さ以下に落ちたら削除
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
