using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMG : MonoBehaviour
{
    private AudioSource bgmPlayer;
    // Start is called before the first frame update
    void Start()
    {
        bgmPlayer = GetComponent<AudioSource>();
    }

    public void SetBGM(AudioClip bgm)
    {
        bgmPlayer.clip = bgm;
        bgmPlayer.Play();
    }
}
