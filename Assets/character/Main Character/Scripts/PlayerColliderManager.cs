using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderManager : MonoBehaviour
{
    [Header("Idle/Run用コライダー")]
    public GameObject normalCollider;

    [Header("ジャンプ中の各フレーム用コライダー")]
    public GameObject[] jumpColliders;

    private int currentJumpIndex = -1;

    // ジャンプ中：指定したインデックスのコライダーだけ有効化
    public void SetJumpCollider(int index)
    {
        if (index < 0 || index >= jumpColliders.Length) return;
        if (currentJumpIndex == index) return;

        normalCollider.SetActive(false);

        for (int i = 0; i < jumpColliders.Length; i++)
            jumpColliders[i].SetActive(i == index);

        currentJumpIndex = index;
        Debug.Log($"SetJumpCollider({index}) called!");
    }

    public void DisableAllJumpColliders()
    {
        foreach (var c in jumpColliders)
            c.SetActive(false);

        currentJumpIndex = -1;
        normalCollider.SetActive(true);
    }
}