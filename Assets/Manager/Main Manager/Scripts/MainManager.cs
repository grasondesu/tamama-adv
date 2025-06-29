using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject gameOverUI;

    [SerializeField, Header("ゲームクリアUI")]
    private GameObject gameClearUI;

    private GameObject player;
    private bool isGameOverShown = false;

    //private GameObject BGMManager;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        AudioManager.Instance.PlayCourseBGM();
    }

    void Update()
    {
        ShowGameOverUI();
    }

    private void ShowGameOverUI()
    {
        // すでに表示済み or プレイヤーがまだ存在する場合は何もしない
        if (isGameOverShown || player != null) return;

        // ゲームオーバージングルを再生
        // if (gameOverBGM != null)
        // {
        //     bgmManager.PlayGameOverBGM();
        // }
        // UI表示

        gameOverUI.SetActive(true);
<<<<<<< HEAD
        //bgmManager.PlayGameOverBGM();
=======
        isGameOverShown = true;
>>>>>>> main
    }

    public void ShowGameClearUI()
    {
<<<<<<< HEAD
        // 足音などを止める（プレイヤーが動けなくなるなら）
        AudioManager.Instance.StopDashSE();

        // ゲームクリアBGMを再生
        AudioManager.Instance.PlayGameClearBGM();
=======
        Debug.Log("★ゲームクリアUIを表示しようとしています");
        if (gameClearUI.activeSelf)
        {
            return;
        }
>>>>>>> main
        gameClearUI.SetActive(true);
    }
}
