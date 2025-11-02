using UnityEngine;

public class DebrisFloat : MonoBehaviour
{
    private Vector3 initialPosition;

    [SerializeField] private float floatHeight = 3f;   // 上に浮く距離
    [SerializeField] private float duration = 2f;      // 浮くのにかかる時間

    [SerializeField] private float shakeAmount = 0.1f; // 揺れる幅
    [SerializeField] private float shakeSpeed = 20f;   // 揺れる速さ

    private float elapsed = 0f;
    private bool isFloating = false;

    void Start()
    {
        initialPosition = transform.position;
        StartFloating(); // デバッグ用に開始と同時に浮かせる
    }

    void Update()
    {
        if (!isFloating) return;

        elapsed += Time.deltaTime;

        float t = Mathf.Clamp01(elapsed / duration);

        // 基本のY上昇
        float newY = Mathf.Lerp(initialPosition.y, initialPosition.y + floatHeight, t);

        // 揺れ（sin波で振動させる）
        float shakeX = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        float shakeY = Mathf.Sin(Time.time * shakeSpeed * 1.5f) * (shakeAmount * 0.5f);

        // 位置更新
        transform.position = new Vector3(initialPosition.x + shakeX, newY + shakeY, initialPosition.z);
    }

    public void StartFloating()
    {
        isFloating = true;
        elapsed = 0f;
    }
}
