using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM Clips（インスペクターで複数登録可能）")]
    public List<AudioClip> bgmClips = new List<AudioClip>();

    [Header("既存のBGM（互換性維持用）")]
    public AudioClip courseBGM;
    public AudioClip gameClearBGM;
    public AudioClip gameOverBGM;

    [Header("SE Clips")]
    public AudioClip dashSE;
    public AudioClip jumpSE;
    public AudioClip hitSE;

    private AudioSource bgmSource;
    private AudioSource dashSeSource;
    private AudioSource jumpSeSource;
    private AudioSource hitSeSource;

    // =====================
    // BGM・SE ON/OFF
    // =====================
    private bool bgmEnabled = true;
    private bool seEnabled = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

            // AudioSourceを取得
            bgmSource = transform.Find("BGMSource").GetComponent<AudioSource>();
            dashSeSource = transform.Find("DashSESource").GetComponent<AudioSource>();
            jumpSeSource = transform.Find("JumpSESource").GetComponent<AudioSource>();
            hitSeSource = transform.Find("HitSESource").GetComponent<AudioSource>();

            // シーン切り替え時に自動再生
            SceneManager.sceneLoaded += OnSceneLoaded;

            // PlayerPrefsから初期状態を取得
            bgmEnabled = PlayerPrefs.GetInt("BGM_ON", 1) == 1;
            seEnabled = PlayerPrefs.GetInt("SE_ON", 1) == 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #region ==== シーンごとの自動BGM ====
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!bgmEnabled) return;

        switch (scene.name)
        {
            case "Title":
                if (bgmClips.Count > 0)
                    PlayBGM(bgmClips[0]);
                break;

            case "Course":
                PlayCourseBGM();
                break;

            case "Boss":
                if (bgmClips.Count > 1)
                    PlayBGM(bgmClips[1]);
                break;

            case "GameClear":
                PlayGameClearBGM();
                break;

            case "GameOver":
                PlayGameOverBGM();
                break;

            default:
                StopBGM();
                break;
        }
    }
    #endregion

    #region ==== BGM ====
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (!bgmEnabled) return;
        if (clip == null) return;

        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void PlayCourseBGM()
    {
        PlayBGM(courseBGM);
    }

    public void PlayGameClearBGM()
    {
        if (!bgmEnabled) return;

        bgmSource.Stop();
        bgmSource.clip = gameClearBGM;
        bgmSource.loop = false;
        bgmSource.Play();
    }

    public void PlayGameOverBGM()
    {
        if (!bgmEnabled) return;

        bgmSource.Stop();
        bgmSource.clip = gameOverBGM;
        bgmSource.loop = false;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }
    #endregion

    #region ==== SE ====
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

    public void PlayJumpSE()
    {
        if (!seEnabled) return;
        jumpSeSource.PlayOneShot(jumpSE);
    }

    public void PlayHitSE()
    {
        if (!seEnabled) return;
        hitSeSource.PlayOneShot(hitSE);
    }

    public void PlayHitSEAndThenGameOverBGM()
    {
        if (!seEnabled)
        {
            PlayGameOverBGM();
            return;
        }

        StartCoroutine(PlayHitSEThenGameOverCoroutine());
    }

    private IEnumerator PlayHitSEThenGameOverCoroutine()
    {
        hitSeSource.PlayOneShot(hitSE);
        yield return new WaitForSeconds(hitSE.length);
        PlayGameOverBGM();
    }

    // 一括停止（SEすべて）
    public void StopAllSE()
    {
        dashSeSource.Stop();
        jumpSeSource.Stop();
        hitSeSource.Stop();
    }
    #endregion

    #region ==== ON/OFF制御 ====
    public void SetBgmEnabled(bool enabled)
    {
        bgmEnabled = enabled;
        PlayerPrefs.SetInt("BGM_ON", enabled ? 1 : 0);
        PlayerPrefs.Save();

        if (!enabled)
            StopBGM();
        else
        {
            // BGMが止まっていてONになったら自動で再生
            if (SceneManager.GetActiveScene().name == "Course")
                PlayCourseBGM();
        }
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

