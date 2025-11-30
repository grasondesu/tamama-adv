using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LogoSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "TitleScene";

    IEnumerator Start()
    {
        // 基本設定
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 1f / 60f;

        // VideoPlayer 設定（Inspectorで設定済みでも上書きOK）
        videoPlayer.playbackSpeed = 1.0f;
        videoPlayer.skipOnDrop = false;
        videoPlayer.timeReference = VideoTimeReference.InternalTime; // ←重要

        // 動画準備待ち
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        // 再生開始
        videoPlayer.Play();

        // 実際に再生が始まるまで待機
        yield return new WaitUntil(() => videoPlayer.isPlaying);

        // 再生終了待ち
        yield return new WaitWhile(() => videoPlayer.isPlaying);

        // 次のシーンへ
        SceneManager.LoadScene(nextSceneName);
    }
}