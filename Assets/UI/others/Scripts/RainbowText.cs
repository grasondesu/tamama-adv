using TMPro;
using UnityEngine;

public class RainbowText : MonoBehaviour
{
    TextMeshProUGUI text;
    float time;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.ForceMeshUpdate();
        var mesh = text.mesh;
        var colors = mesh.colors;

        for (int i = 0; i < colors.Length; i++)
        {
            float h = Mathf.Repeat(time + i * 0.02f, 1f);
            colors[i] = Color.HSVToRGB(h, 1f, 1f);
        }

        mesh.colors = colors;
        text.canvasRenderer.SetMesh(mesh);

        time += Time.unscaledDeltaTime * 0.2f;
    }
}
