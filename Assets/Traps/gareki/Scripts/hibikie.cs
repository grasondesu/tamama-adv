using UnityEngine;

public class CrackDisappearOnPlayerTouch : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            gameObject.SetActive(false); // 自分（ヒビ）を非表示
            Debug.Log("ヒビ消えた！");
        }
    }
}
