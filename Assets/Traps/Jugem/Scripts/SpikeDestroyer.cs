using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDestroyer : MonoBehaviour
{
    [SerializeField] private string groundTag = "Ground";
    [SerializeField] private string playerTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag) || collision.gameObject.CompareTag(playerTag))
        {
            Destroy(gameObject);
        }
    }
}
