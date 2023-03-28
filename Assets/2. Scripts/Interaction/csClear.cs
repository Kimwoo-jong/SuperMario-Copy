using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csClear : MonoBehaviour
{
    public GameObject flagMario;
    public GameObject Mario;
    public GameObject Flag;
    public GameObject Castle;
    public BoxCollider2D flag_Bottom;

    private bool isClear = false;

    private Animator anim;

    private Text text;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        text = GetComponent<Text>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !isClear)
        {
            isClear = true;

            anim.SetTrigger("Clear");
            
            BottomTriggerOn();
            BGMManager.instance.myAudio.Stop();
            SoundManager.soundmanager.ClearSound();
        }
    }
    public void BottomTriggerOn()
    {
        Castle.GetComponent<BoxCollider2D>().isTrigger = true;
        Flag.GetComponent<BoxCollider2D>().enabled = false;
        flag_Bottom.isTrigger = true;
    }
}
