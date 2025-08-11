using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBlock : MonoBehaviour
{
    [Header("動かしたい頂点座標リスト")]
    public List<Vector2> pathPoints = new List<Vector2>()
    {
        new Vector2(0f, 2f),
        new Vector2(-2f, 0f),
        new Vector2(2f, 0f)
    };

    [Header("移動速度")]
    public float moveSpeed = 2f;

    private int currentTargetIndex = 0;
    private GameObject player;
    private Transform playerTransform;
    private bool isCarryingPlayer = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;

        if (pathPoints.Count == 0) return;

        // 初期位置に一番近い頂点の次に向かう
        float minDist = float.MaxValue;
        for (int i = 0; i < pathPoints.Count; i++)
        {
            float dist = Vector2.Distance(transform.position, pathPoints[i]);
            if (dist < minDist)
            {
                minDist = dist;
                currentTargetIndex = (i + 1) % pathPoints.Count;
            }
        }
    }

    void Update()
    {
        // 乗っている間も滑らずに一緒に動く
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (pathPoints.Count == 0) return;

        Vector2 target = pathPoints[currentTargetIndex];
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % pathPoints.Count;
        }
    }

    // プレイヤーが乗ったら親子にして一緒に動く
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCarryingPlayer)
        {
            isCarryingPlayer = true;
            collision.transform.parent = this.transform;
        }
    }

    // プレイヤーが離れたら親子解除
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isCarryingPlayer)
        {
            isCarryingPlayer = false;
            collision.transform.parent = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var point in pathPoints)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }
}