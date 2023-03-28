using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed = 1.0f;

    private Vector3 getVec;
    private Rigidbody2D rigid;
    private Animator anim;

    public GameObject scoreUi;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        getVec = new Vector2(transform.localScale.x * -moveSpeed, rigid.velocity.y);
        rigid.velocity = getVec;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            anim.SetBool("Die", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            SoundManager.soundmanager.EnemyDead();
            PlayerControl.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5f);          // 몬스터를 밟았을 때 위로 살짝 뜨도록.
            Destroy(gameObject, 0.07f);
        }
        if(col.gameObject.tag == "Fire")
        {
            // 뒤집어져서 떨어지는 애니메이션
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            SoundManager.soundmanager.EnemyDead();
            Destroy(gameObject, 0.5f);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Dead")
        {
            Destroy(gameObject);
        }
    }
}