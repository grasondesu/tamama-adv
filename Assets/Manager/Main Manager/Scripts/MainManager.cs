using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [Header("プレイヤー")]
    [SerializeField] private GameObject player;

    [Header("UI")]
    [SerializeField] private GameObject gameClearUI;
    [SerializeField] private GameObject gameOverUI;

    [HideInInspector] public bool isGameCleared = false;
    [HideInInspector] public bool isGameOvered = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>()?.gameObject;

        // シーン開始時BGM再生
        AudioManager.Instance?.PlayBGMForCurrentScene();
    }

    public void ShowGameOverUI()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        isGameOvered = true;
        Debug.Log("💀 GameOver UI 表示");

        // GameOver BGM 再生
        AudioManager.Instance?.StopDashSE();
        AudioManager.Instance?.PlayGameOverBGM();
    }

    public void ShowGameClearUI()
    {
        if (isGameCleared) return;
        isGameCleared = true;

        // SE停止
        AudioManager.Instance?.StopDashSE();

        // GameClear BGM 再生
        AudioManager.Instance?.PlayGameClearBGM();

        if (gameClearUI != null)
            gameClearUI.SetActive(true);

        Debug.Log("🎉 GameClear UI 表示");
    }
}
