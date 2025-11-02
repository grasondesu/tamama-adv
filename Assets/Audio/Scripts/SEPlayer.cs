using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    public static SEPlayer Instance;

    private AudioSource audio;

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
        audio.loop = false;
        audio.playOnAwake = false;
    }

    public void PlaySE(AudioClip clip)
    {
        audio.PlayOneShot(clip); // ← 同じ音を何度も鳴らせる
    }
}
