using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gareki : MonoBehaviour
{
    [SerializeField, Header("飛ばす角度（度）")]
    private float _angleDegree;

    [SerializeField, Header("発射速度")]
    private float _launchSpeed = 5f;

    [SerializeField, Header("発射するプレイヤー位置X以上")]
    private float _posx;

    [SerializeField, Header("発射するプレイヤー位置Y以上")]
    private float _posy;

    public PlayerController _player;

    private Rigidbody2D _rb;
    private bool _hasLaunched = false;

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
        if (!_hasLaunched && _player.transform.position.x >= _posx && _player.transform.position.y >= _posy)
        {
            // 角度から方向ベクトルを作成！！
            float rad = _angleDegree * Mathf.Deg2Rad; // ← 角度をラジアンに変換
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            // velocityで初速セット！
            _rb.linearVelocity = direction.normalized * _launchSpeed;
            _hasLaunched = true;
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
