using UnityEngine;

public class EarthquakeShake : MonoBehaviour
{
    [Header("—h‚ê‚Ì‹­‚³")]
    public float magnitude = 0.1f;   // —h‚ê‚Ì•

    [Header("—h‚ê‚Ì‘¬‚³")]
    public float frequency = 20f;    // —h‚ê‚Ì‘¬‚³
    public bool isShaking = false;   // —h‚ê’†‚©‚Ç‚¤‚©

    private Vector3 originalPos;
    private float seed;

    void Start()
    {
        originalPos = transform.localPosition;          // —h‚ê‚é‘O‚ÌˆÊ’u‚ğ•Û‘¶
        seed = Random.Range(0f, 100f);                 // ƒmƒCƒY—p‚Ìƒ‰ƒ“ƒ_ƒ€ƒV[ƒh
    }

    void Update()
    {
        if (isShaking)
        {
            float x = (Mathf.PerlinNoise(Time.time * frequency, seed) - 0.5f) * 2 * magnitude;
            float y = (Mathf.PerlinNoise(seed, Time.time * frequency) - 0.5f) * 2 * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);
        }
        else
        {
            transform.localPosition = originalPos;
        }
    }

    // —h‚êŠJn
    public void StartShake()
    {
        isShaking = true;
    }

    // —h‚ê’â~
    public void StopShake()
    {
        isShaking = false;
        transform.localPosition = originalPos;  // Œ³‚ÌˆÊ’u‚É–ß‚·
    }
}
