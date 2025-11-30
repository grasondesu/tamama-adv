using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class kaminari : MonoBehaviour
{
    public float lifetime = 1.0f;

    void Start()
    {
        Destroy(gameObject, lifetime);  // アニメ終わったら消す
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("プレイヤーに命中！");
            collision.GetComponent<PlayerController>().Dead();  // プレイヤーが死ぬ
        }
    }
}