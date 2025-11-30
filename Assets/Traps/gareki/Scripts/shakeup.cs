using UnityEngine;

public class DebrisFloatCycle : MonoBehaviour
{
    private Vector3 initialPosition;

    [SerializeField] private float floatHeight = 1f;   // ã‚É“®‚­Å‘å‹——£
    [SerializeField] private float period = 2f;       // 1‰•œ‚É‚©‚©‚éŠÔi•bj

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (period <= 0f) return; // ƒ[ƒŠ„–h~

        // ƒTƒCƒ“”g‚ğ0?1‚É³‹K‰»
        float cycle = (Mathf.Sin((Time.time / period) * Mathf.PI * 2f) + 1f) / 2f;

        // ‰ŠúˆÊ’u‚©‚çã‚É‚¾‚¯“®‚©‚·
        float newY = initialPosition.y + cycle * floatHeight;

        transform.position = new Vector3(
            initialPosition.x,
            newY,
            initialPosition.z
        );
    }
}
