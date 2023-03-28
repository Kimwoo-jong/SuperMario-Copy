using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager soundmanager;
    AudioSource myAudio;

    public AudioClip jump;
    public AudioClip b_jump;
    public AudioClip coin;
    public AudioClip mushroom;
    public AudioClip brick;
    public AudioClip growup;
    public AudioClip brickboom;
    public AudioClip dead;
    public AudioClip enemyDead;
    public AudioClip clear;
    void Awake()
    {
        if (SoundManager.soundmanager == null)
        {
            SoundManager.soundmanager = this;
        }
        myAudio = GetComponent<AudioSource>();
        myAudio.volume = 0.3f;
    }
    public void JumpSound()
    {
        myAudio.PlayOneShot(jump);
    }
    public void JumpBig()
    {
        myAudio.volume = 0.8f;
        myAudio.PlayOneShot(b_jump);
    }
    public void CoinEat()
    {
        myAudio.PlayOneShot(coin);
    }
    public void Mushroom()
    {
        myAudio.PlayOneShot(mushroom);
    }
    public void BrickTouch()
    {
        myAudio.PlayOneShot(brick);
    }
    public void GrowUp()
    {
        myAudio.PlayOneShot(growup);
    }
    public void BrickBreak()
    {
        myAudio.PlayOneShot(brickboom);
    }
    public void DeadSound()
    {
        myAudio.PlayOneShot(dead);
    }
    public void EnemyDead()
    {
        myAudio.PlayOneShot(enemyDead);
    }
    public void ClearSound()
    {
        myAudio.PlayOneShot(clear);
    }
}