using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public static BoxManager instance;

    public string itemTag;
    private int itemNum;
    public GameObject[] objectPrefabs;

    private Animator anim;

    private bool isPlayerTouch = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if(BoxManager.instance == null)
        {
            BoxManager.instance = this;
        }
    }
    void Start()
    {
        for (itemNum = 0; itemNum < objectPrefabs.Length; itemNum++)
        {
            if(objectPrefabs[itemNum].CompareTag(itemTag))
            {
                break;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Head")
        {
            //Debug.Log("머리");
            if (!isPlayerTouch)
            {
                anim.SetTrigger("Touch");
                Invoke("SpawnItem", 0.25f);
                isPlayerTouch = true;
            }
        }

        if (col.gameObject.tag == "Head_super")
        {
            if (!isPlayerTouch && gameObject.tag == "mushbox")
            {
                //Debug.Log("슈퍼마리오머리!");
                anim.SetTrigger("Touch");
                Invoke("SpawnFlower", 0.25f);
                isPlayerTouch = true;
            }

            if(!isPlayerTouch)
            {
                anim.SetTrigger("Touch");
                Invoke("SpawnItem", 0.25f);
                isPlayerTouch = true;
            }
        }
    }
    void SpawnItem()
    {
        Instantiate(objectPrefabs[itemNum], new Vector2(transform.position.x, transform.position.y + 1.0f), transform.rotation);
    }
    void SpawnFlower()
    {
        Instantiate(objectPrefabs[2], new Vector2(transform.position.x, transform.position.y + 0.9f), transform.rotation);
    }
}