using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    public AudioSource myAudio;

    public AudioClip BGM;
    void Awake()
    {
        if (BGMManager.instance == null)
        {
            BGMManager.instance = this;
        }
        myAudio = GetComponent<AudioSource>();
    }
    void Start()
    {
        BGMStart();
    }

    public void BGMStart()
    {
        myAudio.PlayOneShot(BGM);
        myAudio.volume = 0.1f;
        myAudio.loop = true;
    }
}