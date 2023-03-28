using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTurtleControl : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed = 1.0f;
    
    private Vector3 getVec;
    private Rigidbody2D rigid;
    private Animator anim;

    public GameObject scoreUi;

    private bool isFaint = false;
    private bool isKick = false;

    private Vector2 lastVelocity;

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
        if (!isFaint)
        {
            rigid.velocity = getVec;
        }
        lastVelocity = rigid.velocity;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.SetBool("Faint", true);
            moveSpeed = 0;
            PlayerControl.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5f);          // 몬스터를 밟았을 때 위로 살짝 뜨도록.
        }
        if (col.gameObject.tag == "Fire")
        {
            // 뒤집어져서 떨어지는 애니메이션
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            SoundManager.soundmanager.EnemyDead();
            Destroy(gameObject, 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isFaint = true;
            anim.SetBool("Kick", true);

            if (rigid.velocity.x > 0 && !isKick)
            {
                rigid.velocity = Vector2.right * 8.0f;
                isKick = true;
            }

            if (rigid.velocity.x < 0 && !isKick)
            {
                rigid.velocity = Vector2.left * 8.0f;
                isKick = true;
            }
        }

        if (col.gameObject.tag == "Obstacles" || col.gameObject.tag == "Stairs")                      // 새로운 함수 ( 공부해두자 )
        {
            var speed = lastVelocity.magnitude;
            var dir = Vector2.Reflect(lastVelocity.normalized, col.contacts[0].normal);

            rigid.velocity = dir * Mathf.Max(speed, 0f);
        }

        if (col.gameObject.tag == "Dead")
        {
            Destroy(gameObject);
        }
    }
}