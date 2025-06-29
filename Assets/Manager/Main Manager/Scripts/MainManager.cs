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

    void Start()
    {
        PlayerController foundPlayer = FindObjectOfType<PlayerController>();
        if (foundPlayer != null)
        {
            player = foundPlayer.gameObject;
        }
    }

    void Update()
    {
        ShowGameOverUI();
    }

    private void ShowGameOverUI()
    {
        // すでに表示済み or プレイヤーがまだ存在する場合は何もしない
        if (isGameOverShown || player != null) return;

        gameOverUI.SetActive(true);
        isGameOverShown = true;
    }

    public void ShowGameClearUI()
    {
        Debug.Log("★ゲームクリアUIを表示しようとしています");
        if (gameClearUI.activeSelf)
        {
            return;
        }
        gameClearUI.SetActive(true);
    }
}
