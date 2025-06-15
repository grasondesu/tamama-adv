using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public static BGMPlayer Instance; // シングルトン的に使う

    private AudioSource audio;

    public AudioClip gameBGM;
    public AudioClip gameOverBGM;

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
            return;
        }

        audio = gameObject.AddComponent<AudioSource>();
        audio.loop = true;
        audio.volume = 0.5f;

        if (gameBGM != null)
        {
            PlayBGM(gameBGM);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }

    public void StopBGM()
    {
        audio.Stop();
    }
    public void PlayGameOverBGM(AudioClip clip)
    {
        audio.clip = gameOverBGM;
        audio.Play();
    }
}
