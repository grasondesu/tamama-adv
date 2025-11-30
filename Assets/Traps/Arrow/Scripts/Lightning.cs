using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Lightning : MonoBehaviour
{
    [Tooltip("各スプライトに対応するPolygonCollider2Dを並べてセット")]
    public Sprite[] sprites; // アニメーションに使うスプライト
    public PolygonCollider2D[] colliders; // spritesと同じ順番でColliderをセット

    private SpriteRenderer sr;
    private Sprite lastSprite;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lastSprite = sr.sprite;

        // 最初は全Colliderを無効化
        foreach (var col in colliders)
        {
            col.enabled = false;
        }

        EnableColliderForSprite(sr.sprite);
    }

    void Update()
    {
        if (sr.sprite != lastSprite)
        {
            lastSprite = sr.sprite;
            EnableColliderForSprite(sr.sprite);
        }
    }

    void EnableColliderForSprite(Sprite sprite)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            colliders[i].enabled = (sprites[i] == sprite);
        }
    }
}