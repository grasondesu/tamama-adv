using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowLift : MonoBehaviour
{
    public Transform liftTarget;         // 追従対象（リフト）
    public float followSpeed = 2f;       // 追従速度
    public float yOffset = 0f;           // 高さの調整

    private float fixedX;                // 最初のX座標を保持

    void Start()
    {
        fixedX = transform.position.x;   // カメラのXを固定して記録
    }

    void LateUpdate()
    {
        if (liftTarget == null) return;

        float targetY = liftTarget.position.y + yOffset;
        float smoothedY = Mathf.Lerp(transform.position.y, targetY, followSpeed * Time.deltaTime);

        transform.position = new Vector3(fixedX, smoothedY, transform.position.z);
    }
}