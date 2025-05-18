using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField, Header("打ち出し角度")]
    private Vector2 _direction;

    [SerializeField, Header("横位置")]
    private float _posx;

    [SerializeField, Header("縦位置")]
    private float _posy;

    public PlayerController _player;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        go();
    }

    private void go()
    {
        if (_player.transform.position.x >= _posx && _player.transform.position.y >= _posy)
        {
            _rb.AddForce(_direction * 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Floor" || tag == "Enemy" || tag == "DeathLine")
        {
            Destroy(gameObject);
        }
    }


}


