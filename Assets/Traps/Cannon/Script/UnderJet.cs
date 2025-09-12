using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderJet : MonoBehaviour
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

    private bool hasBeenVisible = false;
    private float timeSinceVisible = 0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        go();

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        bool isVisibleNow = (viewPos.x >= 0 && viewPos.x <= 1 &&
                             viewPos.y >= 0 && viewPos.y <= 1 &&
                             viewPos.z > 0);

        if (_hasLaunched)
        {
            if (isVisibleNow)
            {
                hasBeenVisible = true;
                timeSinceVisible = 0f; // リセット
            }
            else if (hasBeenVisible)
            {
                timeSinceVisible += Time.deltaTime;

                // ✨ 少し時間が経ってから消す（例：0.3秒）
                if (timeSinceVisible > 0.3f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void go()
    {

        if (_hasLaunched || _player == null) return;

        if (!_hasLaunched && _player.transform.position.x >= _posx && _player.transform.position.y >= _posy)
        {
            float rad = _angleDegree * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            _rb.velocity = direction.normalized * _launchSpeed;
            _hasLaunched = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Player" || tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}