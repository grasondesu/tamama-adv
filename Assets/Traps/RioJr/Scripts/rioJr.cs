using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rioJr : MonoBehaviour
{
    [SerializeField, Header("オブジェクト")]
    private GameObject rio;

    [SerializeField, Header("横位置")]
    private float _posx;

    [SerializeField, Header("縦位置")]
    private float _posy;

    public PlayerController _player;


    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        show();
    }
    private void show()
    {
        if (_player == null) return;   // ← プレイヤーがデストロイされた時に参照しないようにする。

        if (_player.transform.position.x >= _posx && _player.transform.position.y >= _posy)
        {
            rio.gameObject.SetActive(true);
        }

        else
        {
            rio.gameObject.SetActive(false);
        }
    }

}

