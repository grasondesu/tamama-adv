using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBar : MonoBehaviour
{
    [Header("火の玉Prefab")]
    [SerializeField] private GameObject fireBallPrefab;

    [Header("火の玉の数")]
    [SerializeField] private int ballCount = 5;

    [Header("火の玉の間隔")]
    [SerializeField] private float ballSpacing = 0.5f;

    [Header("回転速度（度/秒）")]
    [SerializeField] private float rotationSpeed = 90f;

    [Header("回転方向（trueなら反時計回り）")]
    [SerializeField] private bool counterClockwise = true;

    [Header("Sorting設定")]
    [SerializeField] private string sortingLayerName = "Default";
    [SerializeField] private int sortingOrder = 0;

    private void Start()
    {
        CreateFireBalls();
    }

    private void Update()
    {
        float dir = counterClockwise ? 1f : -1f;
        transform.Rotate(0, 0, dir * rotationSpeed * Time.deltaTime);
    }

    private void CreateFireBalls()
    {
        for (int i = 0; i < ballCount; i++)
        {
            Vector3 pos = new Vector3((i + 1) * ballSpacing, 0, 0);
            GameObject ball = Instantiate(fireBallPrefab, transform);
            ball.transform.localPosition = pos;

            // レイヤー順序を設定
            SpriteRenderer sr = ball.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = sortingLayerName;
                sr.sortingOrder = sortingOrder;
            }
        }
    }
}