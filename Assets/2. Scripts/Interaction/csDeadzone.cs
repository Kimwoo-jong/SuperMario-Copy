using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class csDeadzone : MonoBehaviour
{
    public static csDeadzone instance;

    private int playerHp;
    private bool isDie;
    void Awake()
    {
        if(instance != null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        playerHp = PlayerControl.instance.playerHp;
        isDie = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !isDie)
        {
            isDie = true;
            playerHp = 0;
            PlayerControl.instance.Die();
            Invoke("Reload", 2.5f);                    // 사망 후 N초의 지연시간을 가진 후 재시작
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene("Mario_Mojak");
    }
    void Dead()
    {
        PlayerControl.instance.Die();
    }
}