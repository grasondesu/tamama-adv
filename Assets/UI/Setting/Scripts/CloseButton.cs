using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] GameObject targetPanel;

    public void Close()
    {
        targetPanel.SetActive(false);
    }
}