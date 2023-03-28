using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Score score;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.soundmanager.CoinEat();
        Destroy(gameObject, 0.2f);
    }
}