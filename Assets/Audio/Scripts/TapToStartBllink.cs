using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TapToStartBlink : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tapText;
    [SerializeField] private float blinkSpeed = 0.3f; // 点滅速度

    private Color baseColor;

    void Start()
    {
        if (tapText == null)
        {
            tapText = GetComponent<TextMeshProUGUI>();
        }

        baseColor = tapText.color;
    }

    void Update()
    {
        if (tapText == null) return;

        // α値をサイン波で変化（0.1〜1.0）
        float alpha = (Mathf.Sin(Time.time * blinkSpeed * Mathf.PI) + 1f) / 2f;
        alpha = Mathf.Lerp(0.1f, 1f, alpha);

        tapText.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
    }
}