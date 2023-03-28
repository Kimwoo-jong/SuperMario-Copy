using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csFlagMario : MonoBehaviour
{
    public static csFlagMario instance;
    private Animator anim;
    private Rigidbody2D rigid;

    [HideInInspector]
    public bool isTouchFlag = false;

    // Start is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        instance = null;
    }
    void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            anim.SetBool("Ground", true);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Clear")
        {
            anim.SetTrigger("Clear");
            isTouchFlag = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Goal" && isTouchFlag)
        {
            rigid.velocity = Vector2.right * 1.5f;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Goal")
        {
            Destroy(gameObject);
        }
    }
}