using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//점수를 관리하는 클래스
[System.Serializable]
public class Score
{
    private int m_TotalScore = 0;   //총 점수
    private int m_EatObject = 0;    //오브젝트 획득 점수
    private int m_KillEnemy = 0;    //적 오브젝트 처치 점수

    //아이템 획득 점수 프로퍼티
    public int ItemScore
    {
        get
        {
            return m_EatObject;
        }
        set
        {
            m_EatObject = value;
        }
    }
    //적 처치 시 획득 점수 프로퍼티
    public int EnemyScore
    {
        get
        {
            return m_KillEnemy;
        }
        set
        {
            m_KillEnemy = value;
        }
    }
    //점수 합산 함수
    public void TotalScore()
    {
        m_TotalScore = ItemScore + EnemyScore;
    }
}