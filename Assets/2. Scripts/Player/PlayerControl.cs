using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    public static AnimController animatorControl;

    public GameObject marioHead;
    public GameObject s_marioHead;

    public GameObject flagMario;            // 작은 마리오
    public GameObject flag_sMario;          // 슈퍼 마리오
    public GameObject flag_rMario;          // 꽃 마리오

    public Rigidbody2D bullet;

    private Animator anim;

    [HideInInspector]
    public bool dirRight = true;

    [HideInInspector]
    public bool isJump = false;
    public float jumpForce = 0.0f;

    private bool grounded = false;
    private Transform groundCheck;

    private Rigidbody2D rigid;
    private CapsuleCollider2D capsule;
    private BoxCollider2D box;
    private SpriteRenderer sprite;

    public float moveForce = 0.0f;
    public float maxSpeed = 5f;                 // 기본 이동시 제한할 속도
    public float maxAccSpeed = 6.5f;              // 가속 버튼 클릭시 제한할 속도
    private float bulletSpeed = 10f;

    [HideInInspector]
    public bool isMarioGrow = false;            // 마리오가 버섯을 먹었는지 판단
    [HideInInspector]
    public bool isMarioFlower = false;          // 마리오가 꽃을 먹었는지 판단
    [HideInInspector]
    public bool isHurt = false;                // 몬스터 피격이 되었는가 판단
    [HideInInspector]
    public bool isPaused = false;

    private bool isDead;

    public int playerHp = 1;                    // 작은 마리오의 Hp
    [HideInInspector]
    public int maxPlayerHp = 3;                 // 꽃 먹은 마리오의 최대 체력

    private float flagMarioTime = 0.1f;

    void Awake()
    {
        if (PlayerControl.instance == null)
        {
            PlayerControl.instance = this;
        }

        anim = GetComponent<Animator>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        capsule = gameObject.GetComponent<CapsuleCollider2D>();
        box = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        anim.SetInteger("Hp", 1);
        groundCheck = transform.Find("Groundcheck");
        capsule.isTrigger = false;
        box.isTrigger = false;
        s_marioHead.SetActive(false);
        isDead = false;

        gameObject.transform.position = new Vector2(-73.8f, 2.0f);

        jumpForce = 8.0f;
        moveForce = 350;
    }
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            isJump = true;
        }

        Duck();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rigid.velocity.x < maxSpeed)
        {
            rigid.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
        {
            rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.normalized.x) * maxSpeed, rigid.velocity.y);
        }
        // 만약 플레이어가 왼쪽을 바로볼때 플레이어를 오른쪽으로 이동하게 입력했다면 
        if (h > 0 && !dirRight)
            // 플레이어를 뒤집어라
            Flip();
        // 그렇지 않고 만약 플레이어가 오른쪽을 바로볼때 플레이어를 왼쪽으로 이동하게 입력했다면 
        else if (h < 0 && dirRight)
            // 플레이어를 뒤집어라
            Flip();

        if (isJump)
        {
            rigid.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("Jump");
            if (!isMarioGrow)
            {
                SoundManager.soundmanager.JumpSound();
            }
            if (isMarioGrow)
            {
                SoundManager.soundmanager.JumpBig();
            }
            isJump = false;
        }

        if (!grounded)
        {
            anim.SetBool("Ground", false);
        }

        if (grounded)
        {
            anim.SetBool("Ground", true);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (maxSpeed < maxAccSpeed)
            {
                maxSpeed += 1.2f * Time.deltaTime;
            }
            else
            {
                maxSpeed = maxAccSpeed;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                InvokeRepeating("Attack", 0.5f, 1.5f);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            maxSpeed = 4.5f;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Mushroom")
        {
            capsule.size = new Vector2((float)1.12f, 0.18f);
            capsule.offset = new Vector2((float)0.0f, -1.0f);
            box.size = new Vector2((float)1.01f, 1.0f);
            SoundManager.soundmanager.GrowUp();

            marioHead.SetActive(false);
            s_marioHead.SetActive(true);
            isMarioGrow = true;

            playerHp++;
            if (playerHp > 2)
            {
                playerHp = 2;
            }
        }

        if (col.gameObject.tag == "Monster")
        {
            playerHp--;
            anim.SetBool("Hit", true);
            if (anim.GetInteger("Hp") == 3 || playerHp == 3)
            {
                anim.SetInteger("Hp", 1);
                playerHp = 1;

                box.size = new Vector2((float)0.86f, 1.0f);
                capsule.size = new Vector2((float)0.86f, 0.14f);
                capsule.offset = new Vector2((float)0.0f, -0.5f);

                marioHead.SetActive(true);
                s_marioHead.SetActive(false);
                isMarioFlower = false;
            }

            else if (anim.GetInteger("Hp") == 2 || playerHp == 2)
            {
                anim.SetInteger("Hp", 1);
                playerHp = 1;

                box.size = new Vector2((float)0.86f, 1.0f);
                capsule.size = new Vector2((float)0.86f, 0.14f);
                capsule.offset = new Vector2((float)0.0f, -0.5f);

                marioHead.SetActive(true);
                s_marioHead.SetActive(false);
                isMarioGrow = false;
            }
            if (playerHp > 0)
            {
                isHurt = true;
                StartCoroutine("Invincible", 0.5f);
            }

            if (playerHp <= 0)
            {
                Die();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Flower")
        {
            capsule.size = new Vector2((float)1.12f, 0.18f);
            capsule.offset = new Vector2((float)0.0f, -1.0f);
            box.size = new Vector2((float)1.01f, 1.0f);
            SoundManager.soundmanager.GrowUp();

            marioHead.SetActive(false);
            s_marioHead.SetActive(true);
            isMarioFlower = true;

            Destroy(col.gameObject);

            playerHp = 3;
            if (playerHp > maxPlayerHp)
            {
                playerHp = maxPlayerHp;
            }
        }
        if (col.gameObject.tag == "Flag")
        {
            if (Time.time > flagMarioTime + 0.1f)
            {
                flagMarioTime = Time.time;
                //Debug.Log("실행");

                gameObject.SetActive(false);
                if (playerHp == 1)
                {
                    flagMario.SetActive(true);
                    Instantiate(flagMario, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    flagMario.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -0.01f);
                }
                else if (playerHp == 2)
                {
                    flag_sMario.SetActive(true);
                    Instantiate(flag_sMario, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    flag_sMario.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -0.01f);
                }

                else if (playerHp == 3)
                {
                    flag_rMario.SetActive(true);
                    Instantiate(flag_rMario, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    flag_rMario.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -0.01f);
                }
            }
            this.enabled = false;
        }
    }
    void Flip()
    {
        dirRight = !dirRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void Duck()
    {
        if (isMarioGrow)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                anim.SetBool("Duck", true);
                capsule.offset = new Vector2((float)0.0f, -0.67f);
                maxSpeed = 0f;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                anim.SetBool("Duck", false);
                capsule.offset = new Vector2((float)0.0f, -1.0f);
                maxSpeed = 5.0f;
            }
        }
    }
    void Attack()
    {
        if (isMarioFlower && dirRight && Input.GetKey(KeyCode.LeftShift))                   // 플레이어가 우측을 향할 때 나갈 불의 방향
        {
            anim.SetTrigger("Attack");
            Rigidbody2D fire = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            fire.velocity = Vector2.right * bulletSpeed;
        }

        if (isMarioFlower && !dirRight && Input.GetKey(KeyCode.LeftShift))                   // 플레이어가 좌측을 향할 때 나갈 불의 방향
        {
            anim.SetTrigger("Attack");
            Rigidbody2D fire = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            fire.velocity = Vector2.left * bulletSpeed;
        }
    }
    //죽으면 2.5초 뒤 재시작 됨.(csDeadZone 스크립트에서 관리)
    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            anim.SetTrigger("Die");

            SoundManager.soundmanager.DeadSound();
            BGMManager.instance.myAudio.Stop();
            maxSpeed = 0f;

            rigid.velocity = new Vector2(0f, 6.0f);

            gameObject.layer = LayerMask.NameToLayer("Die");
            transform.Find("Blockcheck").gameObject.SetActive(false);
        }
    }
    IEnumerator Invincible()
    {
        int countTime = 0;
        while (countTime < 10)
        {
            if (countTime % 2 == 0)
                sprite.color = new Color32(255, 255, 255, 90);
            else
                sprite.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        sprite.color = new Color32(255, 255, 255, 255);

        isHurt = false;
        anim.SetBool("Hit", false);
        if (playerHp < 0)
        {
            playerHp = 1;
        }

        yield return null;
    }
}