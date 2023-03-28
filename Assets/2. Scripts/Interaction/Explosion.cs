using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool isExplode = false;
    public string sortingLayer;
    ParticleSystem particle;
    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    void Start()
    {
        particle.GetComponent<Renderer>().sortingLayerName = sortingLayer;
        if(!isExplode)
        {
            particle.Play();
            Destroy(gameObject, 0.5f);
        }
    }
}