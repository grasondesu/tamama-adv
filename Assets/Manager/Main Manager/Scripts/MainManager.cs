using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject gameOverUI;

    [SerializeField, Header("ゲームクリアUI")]
    private GameObject gameClearUI;

    private GameObject player;
    public bool isGameCleared = false;
    public bool isGameOvered = false;

    //private GameObject BGMManager;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

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
        gameOverUI.SetActive(true);
        isGameOvered = true;
    }

    public void ShowGameClearUI()
    {
        AudioManager.Instance.StopDashSE();
        AudioManager.Instance.PlayGameClearBGM();
        gameClearUI.SetActive(true);

        isGameCleared = true;
        Debug.Log("🎉 isGameCleared = true に設定された！");
    }
}