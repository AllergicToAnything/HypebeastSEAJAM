using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.SoundManagerNamespace;

public class BGM : MonoBehaviour
{
    public AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.MusicVolume = 0.15f;
        //SoundManager.PlayLoopingMusic(bgm);
    }
}
