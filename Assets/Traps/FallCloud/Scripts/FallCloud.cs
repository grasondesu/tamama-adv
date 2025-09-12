using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCloud : MonoBehaviour
{
    public float fallDelay = 0.5f;        // プレイヤーが乗ってから落ちるまでの遅延

    private Rigidbody2D rb;
    private bool hasFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasFallen && collision.collider.CompareTag("Player"))
        {
            hasFallen = true;
            Invoke("Fall", fallDelay);
        }

        if (collision.collider.CompareTag("DeathLine"))
        {
            Destroy(gameObject);
        }
    }

    void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;

    }
}
