using UnityEngine;

public class BulletAutoDestroy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("Floor") ||
            collision.gameObject.CompareTag("DeathLine"))
        {
            Destroy(gameObject);
        }
    }
}
