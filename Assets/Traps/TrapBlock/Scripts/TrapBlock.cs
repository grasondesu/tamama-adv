using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBlock : MonoBehaviour
{
    [Header("ずれる方向（例: 左なら -1,0 / 右なら 1,0）")]
    [SerializeField] private Vector2 shiftDirection = Vector2.left;

    [Header("ずれる距離")]
    [SerializeField] private float shiftDistance = 1f;

    [Header("ずれる速度")]
    [SerializeField] private float shiftSpeed = 2f;

    private Vector3 startPos;
    private bool isShifting = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isShifting)
        {
            Vector3 targetPos = startPos + (Vector3)(shiftDirection.normalized * shiftDistance);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, shiftSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isShifting = true;
        }
    }
}