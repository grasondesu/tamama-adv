using System.Collections;
using UnityEngine;

public class CannonTwoShot : MonoBehaviour
{
    [Header("発射角度（度）")]
    [SerializeField] private float _angleDegree = 0f;

    [Header("発射速度")]
    [SerializeField] private float _launchSpeed = 5f;

    [Header("発射開始座標判定（プレイヤーがこれを越えたら発射）")]
    [SerializeField] private float _posX;
    [SerializeField] private float _posY;

    [Header("1発目の弾")]
    [SerializeField] private GameObject _firstShotPrefab;

    [Header("2発目の弾")]
    [SerializeField] private GameObject _secondShotPrefab;

    [Header("2発目までのディレイ秒数")]
    [SerializeField] private float _secondDelay = 0.5f;

    [Header("プレイヤー")]
    [SerializeField] private PlayerController _player;

    private bool hasEntered = false;   
    private Coroutine shotRoutine = null;
    private int shotCount = 0;         // ★ 発射した弾の数（最大2）

    void Update()
    {
        CheckShotArea();
    }

    private void CheckShotArea()
    {
        // プレイヤーが範囲に入ったかどうか
        bool isInside =
            _player.transform.position.x >= _posX &&
            _player.transform.position.y >= _posY;

        // プレイヤーが範囲に入り、まだ撃っていない & コルーチン未実行
        if (isInside && !hasEntered && shotCount < 2 && shotRoutine == null)
        {
            hasEntered = true;
            shotRoutine = StartCoroutine(FireTwoShots());
        }

        // 範囲外に出たとき（はじめて false になった瞬間だけ）
        if (!isInside)
        {
            hasEntered = false;

            // ★ 途中で出ても shotCount はリセットしない（2発まで仕様）
            // コルーチン中なら止めるだけ
            if (shotRoutine != null)
            {
                StopCoroutine(shotRoutine);
                shotRoutine = null;
            }
        }
    }

    private IEnumerator FireTwoShots()
    {
        // 1発目
        Debug.Log("1発目発射");
        Fire(_firstShotPrefab);
        shotCount++;

        // まだ1発しか撃っていない場合だけ2発目へ
        if (shotCount < 2)
        {
            yield return new WaitForSeconds(_secondDelay);

            Debug.Log("2発目発射");
            Fire(_secondShotPrefab);
            shotCount++;
        }

        // 完了
        shotRoutine = null;
    }

    private void Fire(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        float rad = _angleDegree * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        rb.linearVelocity = dir * _launchSpeed;
    }
}