using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleManager : MonoBehaviour
{
    [Header("コースセレクトシーン名")]
    [SerializeField] private string courseSelectSceneName = "CourseSelectScene";

    [Header("VideoPlayer（背景動画用）")]
    [SerializeField] private VideoPlayer videoPlayer;

    [Header("スタート用透明ボタン")]
    [SerializeField] private Button startButton; // UIでクリック判定

    void Start()
    {
        // 背景動画を再生
        if (videoPlayer != null)
            videoPlayer.Play();

        // ボタンにクリックイベントを登録
        if (startButton != null)
            startButton.onClick.AddListener(OnClickStart);
    }

    private void OnClickStart()
    {
        // UI判定を確認（念のため、ボタン上以外のクリックを無視）
        if (EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject != startButton.gameObject)
            return;

        Debug.Log("コースセレクトに遷移");
        SceneManager.LoadScene(courseSelectSceneName);
    }
}
