using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject gameOverUI;

    [SerializeField, Header("ゲームクリアUI")]
    private GameObject gameClearUI;

    private GameObject player;

    //private GameObject BGMManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        AudioManager.Instance.PlayCourseBGM();
    }

    // Update is called once per frame
    void Update()
    {
        ShowGameOverUI();
    }

    private void ShowGameOverUI()
    {
        if (player != null) return;

        // ゲームオーバージングルを再生
        // if (gameOverBGM != null)
        // {
        //     bgmManager.PlayGameOverBGM();
        // }
        // UI表示

        gameOverUI.SetActive(true);
        //bgmManager.PlayGameOverBGM();
    }

    public void ShowGameClearUI()
    {
        // 足音などを止める（プレイヤーが動けなくなるなら）
        AudioManager.Instance.StopDashSE();

        // ゲームクリアBGMを再生
        AudioManager.Instance.PlayGameClearBGM();
        gameClearUI.SetActive(true);
    }
}