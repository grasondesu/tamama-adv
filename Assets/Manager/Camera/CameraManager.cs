using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private PlayerController player;
    private Vector3 initPos;

    [SerializeField, Header("カメラをずらす距離")]
    private float xOffset;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        initPos = transform.position;
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        float x = player.transform.position.x + xOffset;
        x = Mathf.Clamp(x, initPos.x, Mathf.Infinity);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
