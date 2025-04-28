using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField, Header("ゲームクリア")]
    private GameObject _gameClearUI;

    private void OnTriggerEnter2D( Collider2D other )
    { 
      if (other.CompareTag("Player"))
         {
            _gameClearUI.SetActive(true);
         }
     }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
