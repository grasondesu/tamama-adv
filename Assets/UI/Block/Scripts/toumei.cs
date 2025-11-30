using UnityEngine;

public class HiddenBlock : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab; // 出すアイテム（任意）
    private SpriteRenderer sr;
    private bool activated = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // 初期は見えないけど当たり判定は残す
        sr.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (activated) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 下からぶつかった場合（プレイヤーの頭で当たる）
                if (contact.normal.y > 0.5f)
                {
                    ActivateBlock();
                }
            }
        }
    }

    private void ActivateBlock()
    {
        activated = true;
        sr.enabled = true; // 見えるようにする

        // アイテムを出す（オプション）
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
