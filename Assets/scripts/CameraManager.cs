using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        this._player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this._player.transform.position;
        transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);

    }   
       
    
}
