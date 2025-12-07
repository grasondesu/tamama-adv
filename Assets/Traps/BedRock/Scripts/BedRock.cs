using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedRock : MonoBehaviour
{
    [Header("岩のPrefab")]
    [SerializeField] private GameObject rockPrefab;

    [Header("岩の生成位置")]
    [SerializeField] private Transform firstRockSpawnPoint;
    [SerializeField] private Transform secondRockSpawnPoint;

    [Header("落下速度（等速落下速度）")]
    [SerializeField] private float fallSpeed = 5f;

    [Header("プレイヤー発動条件のX座標とY座標")]
    [SerializeField] private float triggerX; // これ以上になったら
    [SerializeField] private float triggerY; // これ以上になったら

    [Header("２個目の岩を落とすまでの遅延")]
    [SerializeField] private float secondRockDelay = 1.5f;

    [Header("床のレイヤー名")]
    [SerializeField] private string floorLayerName = "Floor";

    private GameObject player;
    private GameObject firstRock;

    private enum TrapState
    {
        WaitingForPlayer,
        FirstRockDropped,
        WaitingForPlayerToEvade,
        SecondRockDropped
    }

    private TrapState currentState = TrapState.WaitingForPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        switch (currentState)
        {
            case TrapState.WaitingForPlayer:
                Vector2 playerPos = player.transform.position;
                if (playerPos.x >= triggerX && playerPos.y >= triggerY)
                {
                    DropFirstRock();
                    currentState = TrapState.FirstRockDropped;
                }
                break;

            case TrapState.FirstRockDropped:
                if (firstRock != null)
                {
                    if (player.transform.position.x < firstRock.transform.position.x)
                    {
                        currentState = TrapState.WaitingForPlayerToEvade;
                        StartCoroutine(DropSecondRockAfterDelay());
                    }
                }
                else
                {
                    currentState = TrapState.WaitingForPlayer;
                }
                break;

            case TrapState.WaitingForPlayerToEvade:
                // 待機中（コルーチンで処理）
                break;

            case TrapState.SecondRockDropped:
                // トラップ完了
                break;
        }
    }

    private void DropFirstRock()
    {
        firstRock = Instantiate(rockPrefab, firstRockSpawnPoint.position, Quaternion.identity);
        SetupRock(firstRock);
    }

    private IEnumerator DropSecondRockAfterDelay()
    {
        yield return new WaitForSeconds(secondRockDelay);

        GameObject secondRock = Instantiate(rockPrefab, secondRockSpawnPoint.position, Quaternion.identity);
        SetupRock(secondRock);

        currentState = TrapState.SecondRockDropped;
    }

    private void SetupRock(GameObject rock)
    {
        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f; // 重力OFFにして
        }

        RockMovement mover = rock.GetComponent<RockMovement>();
        if (mover == null)
        {
            mover = rock.AddComponent<RockMovement>();
        }
        mover.fallSpeed = fallSpeed;

        RockCollisionHandler handler = rock.GetComponent<RockCollisionHandler>();
        if (handler == null)
        {
            handler = rock.AddComponent<RockCollisionHandler>();
        }
        handler.floorLayerName = floorLayerName;
    }
}

// 岩の等速落下制御スクリプト
public class RockMovement : MonoBehaviour
{
    public float fallSpeed = 5f; // Inspectorで調整可能

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, -fallSpeed); // 一定速度で真下に落ちる
        }
    }
}

// 岩の床接触判定用スクリプト
public class RockCollisionHandler : MonoBehaviour
{
    [HideInInspector]
    public string floorLayerName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(floorLayerName))
        {
            Destroy(gameObject);
        }
    }
}