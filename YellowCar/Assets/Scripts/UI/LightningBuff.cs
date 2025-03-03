using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBuff : BuffButton
{
    [SerializeField] private ScriptableObjectPoolData _carPool;

    public override void ActivateBuff()
    {
        base.ActivateBuff();

        foreach (var yellowCar in _carPool.YellowCarPool.GetAllCars())
        {
            if (yellowCar.gameObject.activeSelf)
            {
               yellowCar.OnMouseDown();

            }
        }
    }
}
