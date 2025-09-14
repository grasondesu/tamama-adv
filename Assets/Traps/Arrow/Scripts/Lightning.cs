using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Collider2D hitbox;  // Inspector で設定する用
    public float lifetime = 1.0f;

    void Start()
    {
        if (hitbox != null)
            hitbox.enabled = false;  // 最初はOFF
        Destroy(gameObject, lifetime);  // 自動で消える
    }

    // アニメーションイベントから呼ぶ用
    public void EnableCollider()
    {
        if (hitbox != null)
            hitbox.enabled = true;
    }

    public void DisableCollider()
    {
        if (hitbox != null)
            hitbox.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()?.Dead();
        }
    }
}