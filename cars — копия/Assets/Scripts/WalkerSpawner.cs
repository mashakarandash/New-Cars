using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerSpawner : MonoBehaviour
{
    [SerializeField] private TraficLight _traficLight;
    [SerializeField] private List<Walker> _walkers;

    void Start()
    {
        _traficLight.GreenLightEvent += x=>StartWalkers(x);
    }

    private void StartWalkers(bool x)
    {
        if (x == false)
        {
            foreach (Walker walker in _walkers)
            {
                StartCoroutine(walker.StartCroassingTheRoad());
            }
        }
    }
}
