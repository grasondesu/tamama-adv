using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private int gameOverCount = 0;
    private BannerView _bannerView;
    private InterstitialAd _interstitialAd;

#if UNITY_ANDROID
    private string _bannerAdId = "ca-app-pub-3940256099942544/6300978111";
    private string _interstitialAdId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
    private string _bannerAdId = "ca-app-pub-3940256099942544/2934735716";
    private string _interstitialAdId = "ca-app-pub-3940256099942544/4414689104";
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
    // テストデバイスIDのリストを作成（AdMob画面やログで確認したIDを入れる）
    RequestConfiguration requestConfiguration = new RequestConfiguration
    {
        TestDeviceIds = new System.Collections.Generic.List<string> 
        { 
            "A5093476-7B36-4431-B0E7-A38CE3519942" // ここに自分の端末IDを貼り付ける
        }
    };
    MobileAds.SetRequestConfiguration(requestConfiguration);

    MobileAds.Initialize(initStatus =>
    {
        LoadBannerAd();
        LoadInterstitialAd();
    });
}

    // --- バナー広告（変更なし） ---
    private void LoadBannerAd()
    {
        if (_bannerView != null) _bannerView.Destroy();
        _bannerView = new BannerView(_bannerAdId, AdSize.Banner, AdPosition.Bottom);
        _bannerView.OnBannerAdLoaded += () => { CheckBannerVisibility(); };
        _bannerView.LoadAd(new AdRequest());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) { CheckBannerVisibility(); }

    private void CheckBannerVisibility()
    {
        if (_bannerView == null) return;
        if (SceneManager.GetActiveScene().buildIndex == 0) _bannerView.Show();
        else _bannerView.Hide();
    }

    // --- インタースティシャル広告（強化版） ---
    public void LoadInterstitialAd()
    {
        // 既存の広告があれば破棄して新しくロードする
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("[AdLog] インタースティシャル広告をロード開始...");
        InterstitialAd.Load(_interstitialAdId, new AdRequest(), (ad, error) =>
        {
            if (error != null)
            {
                Debug.LogError("[AdLog] ロード失敗: " + error.GetMessage());
                return;
            }
            _interstitialAd = ad;
            Debug.Log("[AdLog] ロード成功！準備完了。");
        });
    }

    public void ShowInterstitialOnGameOver()
    {
        gameOverCount++;
        Debug.Log($"[AdLog] ゲームオーバー回数: {gameOverCount}");

        // 【ここがポイント！】
        // 7回目の直前（6回目）で、広告がもし消えていても大丈夫なように再ロードをかける
        if (gameOverCount % 7 == 6)
        {
            Debug.Log("[AdLog] 次回(7回目)に向けて広告をリフレッシュします。");
            LoadInterstitialAd();
        }

        if (gameOverCount % 7 == 0)
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("[AdLog] 7回目：広告を表示します。");
                _interstitialAd.Show();
                
                // 表示が終わった直後に「次」をロードしておく
                LoadInterstitialAd();
            }
            else
            {
                // 万が一準備が間に合わなかった場合、8回目でリベンジするためにロード
                Debug.LogWarning("[AdLog] 7回目ですが準備が間に合いませんでした。再ロードします。");
                LoadInterstitialAd();
            }
        }
    }
}