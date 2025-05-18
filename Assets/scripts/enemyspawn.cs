using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    [SerializeField, Header("敵")]
    private GameObject _enemy;

    private PlayerController _player;
    private GameObject _enemyObj;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _enemyObj = null;
    }

    // Update is called once per frame
    void Update()
    {
        _SpawnEnemy();
    }

    private void _SpawnEnemy()
    {
        if (_player == null) return;

        Vector3 PlayerPos = _player.transform.position;
        Vector3 cameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Vector3 scale = _enemy.transform.lossyScale;

        float distance = Vector2.Distance(transform.position, new Vector2(PlayerPos.x, transform.position.y));
        float spawnDis = Vector2.Distance(PlayerPos, new Vector2(cameraMaxPos.x + scale.x / 2.0f, PlayerPos.y));
        if (distance <= spawnDis && _enemyObj == null)
        {
            _enemyObj = Instantiate(_enemy);
            _enemyObj.transform.position = transform.position;
            transform.parent = _enemyObj.transform;
        }

    }
}
