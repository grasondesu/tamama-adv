using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneBGM
{
    public string sceneName;
    public AudioClip clip;
    public bool loop = true;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("シーンごとのBGM")]
    public List<SceneBGM> sceneBGMs = new List<SceneBGM>();

    [Header("特別BGM")]
    public AudioClip gameOverBGM;
    public AudioClip gameClearBGM;

    [Header("SE Clips")]
    public AudioClip dashSE;
    public AudioClip jumpSE;
    public AudioClip hitSE;

    private AudioSource bgmSource;
    private AudioSource dashSeSource;
    private AudioSource jumpSeSource;
    private AudioSource hitSeSource;

    private bool bgmEnabled = true;
    private bool seEnabled = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            bgmSource = transform.Find("BGMSource").GetComponent<AudioSource>();
            dashSeSource = transform.Find("DashSESource").GetComponent<AudioSource>();
            jumpSeSource = transform.Find("JumpSESource").GetComponent<AudioSource>();
            hitSeSource = transform.Find("HitSESource").GetComponent<AudioSource>();

            SceneManager.sceneLoaded += OnSceneLoaded;

            bgmEnabled = PlayerPrefs.GetInt("BGM_ON", 1) == 1;
            seEnabled = PlayerPrefs.GetInt("SE_ON", 1) == 1;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (bgmEnabled)
            PlayBGMForCurrentScene();
        else
            StopBGM();
    }

    #region === BGM ===
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (!bgmEnabled || clip == null) return;

        if (bgmSource.clip == clip)
        {
            if (!bgmSource.isPlaying)
                bgmSource.Play();
            return;
        }

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();

        bgmSource.clip = null;
    }

    public void PlayBGMForCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        foreach (var sb in sceneBGMs)
        {
            if (sb.sceneName == sceneName && sb.clip != null)
            {
                PlayBGM(sb.clip, sb.loop);
                return;
            }
        }
        StopBGM();
    }

    public void PlayGameClearBGM()
    {
        if (!bgmEnabled) return;
        if (gameClearBGM != null)
            PlayBGM(gameClearBGM, loop: false);
        else
            Debug.LogWarning("⚠️ GameClearBGM が設定されていません");
    }

    public void PlayGameOverBGM()
    {
        if (!bgmEnabled) return;
        if (gameOverBGM != null)
            PlayBGM(gameOverBGM, loop: false);
        else
            Debug.LogWarning("⚠️ GameOverBGM が設定されていません");
    }
    #endregion

    #region === SE ===
    public void PlayDashSE()
    {
        if (!seEnabled) return;
        if (!dashSeSource.isPlaying)
        {
            dashSeSource.clip = dashSE;
            dashSeSource.loop = true;
            dashSeSource.Play();
        }
    }

    public void StopDashSE()
    {
        if (dashSeSource.isPlaying)
            dashSeSource.Stop();
    }

    public void PlayJumpSE() { if (seEnabled) jumpSeSource.PlayOneShot(jumpSE); }
    public void PlayHitSE() { if (seEnabled) hitSeSource.PlayOneShot(hitSE); }

    public void StopAllSE()
    {
        dashSeSource.Stop();
        jumpSeSource.Stop();
        hitSeSource.Stop();
    }
    #endregion

    #region === ON/OFF ===
    public void SetBgmEnabled(bool enabled)
    {
        bgmEnabled = enabled;
        PlayerPrefs.SetInt("BGM_ON", enabled ? 1 : 0);
        PlayerPrefs.Save();

        if (!enabled)
            StopBGM();
        else
            PlayBGMForCurrentScene();
    }

    public void SetSeEnabled(bool enabled)
    {
        seEnabled = enabled;
        PlayerPrefs.SetInt("SE_ON", enabled ? 1 : 0);
        PlayerPrefs.Save();

        if (!enabled)
            StopAllSE();
    }
    #endregion
}





