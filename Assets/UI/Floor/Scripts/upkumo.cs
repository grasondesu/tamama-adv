using UnityEngine;

public class CloudMover2D : MonoBehaviour
{
    [Header("移動設定")]
    public float speed = 2f;          // 上昇スピード
    public bool moveForever = true;   // 無限に上昇するか
    public float maxDistance = 10f;   // moveForever=false の場合の移動距離

    [Header("プレイヤー関連")]
    public bool parentPlayer = true;  // プレイヤーを雲の子にするか

    private Vector3 startPos;
    private bool isMoving = false;
    private float moved = 0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (!isMoving) return;

        float delta = speed * Time.deltaTime;
        transform.Translate(Vector3.up * delta, Space.World);
        moved += delta;

        if (!moveForever && moved >= maxDistance)
        {
            isMoving = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        // 当たった方向を確認（上から乗ったときだけ反応）
        Vector2 normal = collision.contacts[0].normal;
        if (normal.y < -0.5f) // プレイヤーが上から踏んだとき
        {
            Debug.Log("雲に乗った！");
            isMoving = true;

            if (parentPlayer)
                collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Debug.Log("雲から離れた");
        if (parentPlayer)
            collision.transform.SetParent(null);

        // 離れたら止めたいなら下を有効化
        // isMoving = false;
    }
}
