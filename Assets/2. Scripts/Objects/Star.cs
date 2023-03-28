using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject star;

    [HideInInspector]
    public float moveSpeed = 4.0f;

    private Vector3 getVec;
    private Rigidbody2D rigid;

    void Awake()
    {
        rigid = star.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        star.AddComponent<CapsuleCollider2D>();
        SoundManager.soundmanager.Mushroom();
    }

    // Update is called once per frame
    void Update()
    {
        getVec = new Vector2(transform.localScale.x * moveSpeed, rigid.velocity.y);
        rigid.velocity = getVec;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Obstacles")
        {
            moveSpeed = -moveSpeed;
            this.GetComponent<SpriteRenderer>().flipX = (moveSpeed < 0);
        }
    }
}