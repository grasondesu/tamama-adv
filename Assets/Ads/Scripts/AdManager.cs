using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System.Collections.Generic;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private int gameOverCount = 0;
    private InterstitialAd _interstitialAd;

    // --- ID設定（ドキュメント準拠のテストID） ---
#if UNITY_ANDROID
    private string _interstitialAdId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
    // iOS用のサンプル広告ユニットID（ドキュメント参照）
    private string _interstitialAdId = "ca-app-pub-3940256099942544/4411468910";
#endif

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 【重要】ドキュメントに基づいたテストデバイスの設定
        // iOS実機で表示させるには、ここのリストにIDを追加するのが最も確実です
        List<string> testDeviceIds = new List<string>
        {
            AdRequest.TestDeviceSimulator, // シミュレーター用
            // --- ここにあなたのiPhoneのテストデバイスID（英数字32桁程度）を貼り付けてください ---
            "cd266182677e45fc41e093da42f881be" 
        };

        RequestConfiguration requestConfiguration = new RequestConfiguration
        {
            TestDeviceIds = testDeviceIds
        };
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // 初期化
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob初期化完了");
            LoadInterstitialAd();
        });
    }

   
    // --- インタースティシャル広告 ---
    public void LoadInterstitialAd()
    {
        if (_interstitialAd != null) { _interstitialAd.Destroy(); _interstitialAd = null; }

        Debug.Log("インタースティシャル広告ロード開始...");
        InterstitialAd.Load(_interstitialAdId, new AdRequest(), (ad, error) =>
        {
            if (error != null) { Debug.LogError("ロード失敗: " + error.GetMessage()); return; }
            _interstitialAd = ad;

            _interstitialAd.OnAdFullScreenContentClosed += () => {
                Debug.Log("広告が閉じられました");
                LoadInterstitialAd(); // 次回分をロード
            };
        });
    }

    public void ShowInterstitialOnGameOver()
    {
        gameOverCount++;
        // 7回に1回表示
        if (gameOverCount % 7 == 0)
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("インタースティシャル表示実行");
                _interstitialAd.Show();
            }
            else
            {
                Debug.Log("広告の準備ができていません。ロードを再試行します。");
                LoadInterstitialAd();
            }
        }
        // 6回目（次で表示）の時に最新の広告をロードしておく
        else if (gameOverCount % 7 == 6)
        {
            LoadInterstitialAd();
        }
    }
}