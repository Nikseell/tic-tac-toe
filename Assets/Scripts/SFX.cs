using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFX : MonoBehaviour
{
    public static SFX instance = null;

    public AudioSource soundSource;

    public AudioClip[] clips;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        InitializeManager();
    }

    private void InitializeManager()
    {

    }

    public void PlaySound(int index)
    {
        soundSource.PlayOneShot(clips[index]);
    }
}
