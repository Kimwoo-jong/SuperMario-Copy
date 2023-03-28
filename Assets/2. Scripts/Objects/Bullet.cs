using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public int fireHp = 5;
    void Update()
    {
        Destroy(gameObject, 3.0f);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            //Debug.Log("2");
            Destroy(gameObject, 0.5f);
            Destroy(col.gameObject, 0.5f);
        }

        if(col.gameObject.tag == "Obstacles" || col.gameObject.tag == "Stairs")
        {
            fireHp--;
            if(fireHp == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
