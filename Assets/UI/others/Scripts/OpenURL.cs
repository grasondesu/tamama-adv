using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url;

    public void Open()
    {
        Application.OpenURL(url);
        Debug.Log("利用規約を開きました");
    }
}