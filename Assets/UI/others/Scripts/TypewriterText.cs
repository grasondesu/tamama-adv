using TMPro;
using UnityEngine;
using System.Collections;

public class TypewriterText : MonoBehaviour
{
    [TextArea]
    public string message = "Thank you for playing";
    public float interval = 0.05f;

    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    public IEnumerator Play()
    {
        text.text = "";

        foreach (char c in message)
        {
            text.text += c;
            yield return new WaitForSecondsRealtime(interval);
        }
    }
}
