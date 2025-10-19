using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM Clips")]
    public AudioClip courseBGM;
    public AudioClip gameClearBGM;
    public AudioClip gameOverBGM;

    [Header("SE Clips")]
    public AudioClip dashSE;
    public AudioClip jumpSE;
    public AudioClip hitSE;

    // AudioSources（自動取得）
    private AudioSource bgmSource;
    private AudioSource dashSeSource;
    private AudioSource jumpSeSource;
    private AudioSource hitSeSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

            // 子オブジェクトのAudioSourceを名前で取得
            bgmSource = transform.Find("BGMSource").GetComponent<AudioSource>();
            dashSeSource = transform.Find("DashSESource").GetComponent<AudioSource>();
            jumpSeSource = transform.Find("JumpSESource").GetComponent<AudioSource>();
            hitSeSource = transform.Find("HitSESource").GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region BGM

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }
    public void PlayCourseBGM()
    {
        PlayBGM(courseBGM);
    }
    public void PlayGameClearBGM()
    {
        bgmSource.Stop(); // コースBGMを止める

        bgmSource.clip = gameClearBGM;
        bgmSource.loop = false; // ループしない
        bgmSource.Play();
    }
    public void PlayGameOverBGM()
    {
        // コースBGMを止める
        bgmSource.Stop();

        // ゲームオーバーBGMをセットして再生
        bgmSource.clip = gameOverBGM;
        bgmSource.loop = false;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    #endregion

    #region SE

    public void PlayDashSE()
    {
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
        {
            dashSeSource.Stop();
        }
    }

    public void PlayJumpSE()
    {
        jumpSeSource.PlayOneShot(jumpSE);
    }

    public void PlayHitSE()
    {
        hitSeSource.PlayOneShot(hitSE);
    }
    public void PlayHitSEAndThenGameOverBGM()
    {
        //コルーチンは時間の流れを扱える処理のこと。ある処理をして、〇秒待ってから次の処理をするみたいなことができる
        StartCoroutine(PlayHitSEThenGameOverCoroutine());
    }

    private IEnumerator PlayHitSEThenGameOverCoroutine()
    {
        // 死亡効果音を再生
        hitSeSource.PlayOneShot(hitSE);

        // 効果音の再生が終わるまで待機
        yield return new WaitForSeconds(hitSE.length);

        // 効果音が終わったらゲームオーバーBGMを再生
        PlayGameOverBGM();
    }

    #endregion
}