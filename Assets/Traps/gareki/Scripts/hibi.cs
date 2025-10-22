using UnityEngine;

public class DebrisSimpleCrack : MonoBehaviour
{
    [SerializeField] private GameObject crackImage; // ひび画像（非表示でセット）
    [SerializeField] private string playerTag = "Player"; // プレイヤータグ

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に当たったら
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (crackImage != null)
            {
                crackImage.SetActive(true); // ヒビを表示
            }

            Debug.Log("💥 がれきが地面に当たってひび発生！");

            Destroy(gameObject); // がれきを消す
        }
    }

    // プレイヤーが触れたらヒビを消す
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (crackImage != null)
            {
                crackImage.SetActive(false); // ヒビ非表示
                Debug.Log("🟢 ヒビが消えた！");
            }
        }
    }
}
