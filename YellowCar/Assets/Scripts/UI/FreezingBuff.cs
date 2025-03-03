using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingBuff : BuffButton
{
    [SerializeField] private ScriptableObjectPoolData _allCars;
    [SerializeField] private float _buffDuration;
    [SerializeField] private int _freezingSpeed;

    public override void ActivateBuff()
    {
        base.ActivateBuff();
        StartCoroutine(FreezeCarsCoroutine());
    }

    private IEnumerator FreezeCarsCoroutine()
    {
        foreach (var car in _allCars.AllCars)
        {
            car.SaveSpeed = car.Speed;
            car.NavMeshAgent.speed = _freezingSpeed;
        }
        yield return new WaitForSeconds(_buffDuration);

        foreach (var car in _allCars.AllCars)
        {
            car.NavMeshAgent.speed = car.SaveSpeed;
        }
    }
}
