using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCloud : MonoBehaviour
{
    [Header("追いかけ対象")]
    public Transform target;

    [Header("プレイヤーを感知する距離")]
    public float detectRange = 5f;

    [Header("最大追いかけ距離（追いかけ開始位置からの最大移動距離）")]
    public float maxChaseDistance = 5f;

    [Header("移動速度")]
    public float moveSpeed = 2f;

    private bool isChasing = false;
    private Vector2 chaseStartPos;

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }

    void Update()
    {
        // ✅ ゲームクリア時は何もしない
        if (MainManager.Instance != null && MainManager.Instance.isGameCleared)
        {
            return;
        }

        if (target == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (!isChasing && distanceToPlayer <= detectRange)
        {
            // プレイヤーを感知して追跡開始
            isChasing = true;
            chaseStartPos = transform.position;
        }

        if (isChasing)
        {
            float distanceChased = Vector2.Distance(transform.position, chaseStartPos);

            if (distanceChased < maxChaseDistance)
            {
                // プレイヤーの方向に移動
                Vector2 direction = (target.position - transform.position).normalized;
                transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            }
            else
            {
                // 最大追跡距離を超えたら止まる
                isChasing = false;
            }
        }
    }
}
