using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    private PlayerControl PlayerControl;

    public GameObject explosion;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Head")
        {
            //Debug.Log("마리오 닿음");
            if (!PlayerControl.instance.isMarioGrow)
            {
                //Debug.Log("조그만 마리오");
                anim.SetTrigger("Touch");
                SoundManager.soundmanager.BrickTouch();
            }
        }
        if(col.gameObject.tag == "Head_super")
        {
            //Debug.Log("큰 마리오");
            anim.SetBool("Break", true);
            Instantiate(explosion, transform.position, Quaternion.identity);
            SoundManager.soundmanager.BrickBreak();
            Destroy(gameObject, 0.5f);
        }
    }
}