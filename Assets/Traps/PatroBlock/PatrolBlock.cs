using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBlock : MonoBehaviour
{
    [Header("移動範囲")]
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;

    [Header("停止するプレイヤー距離")]
    [SerializeField] private float stopRange = 2f;

    private Vector3 startPos;
    private bool movingRight = true;
    private GameObject player;

    void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        // プレイヤーまでの距離
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= stopRange)
        {
            // 停止
            return;
        }

        // 左右パトロール移動
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= startPos.x + moveDistance)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= startPos.x - moveDistance)
                movingRight = true;
        }
    }
}
