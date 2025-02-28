using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiRemoverBuff : BuffButton
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField] private GameObject _buttonTaxi;
    [SerializeField] private ScriptableObjectPoolData _allCars;

 

    private void ActivateButton()
    {
        _buttonTaxi.SetActive(true);
    }

    public override void ActivateBuff()
    {
        
        base.ActivateBuff();
        _allCars.TaxiCarPool.ReturnCars();
        foreach (var spawner in _spawners)
        {
            spawner.CanCreateTaxiCar = false;

        }
        EventBus eventBus = ServiceLocator.Instance.GetRegisterService<EventBus>();
        eventBus.RestartGameAction += ActivateButton;
        _buttonTaxi.SetActive(false);
    }
}
