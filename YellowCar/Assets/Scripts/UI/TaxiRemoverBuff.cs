using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TaxiRemoverBuff : BuffButton
{
   
    [SerializeField] private GameObject _buttonTaxi;
    [SerializeField] private Image _timerTaxi;
    [SerializeField] private ScriptableObjectPoolData _allCars;

    private EventBus _eventBus;

    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    protected override void Start()
    {
        base.Start();
        if (MasterSave.SaveData.TaxiBuffCount == 0)
        {
            BlockingButton();
        }
        ValueText.text = MasterSave.SaveData.TaxiBuffCount.ToString();
        OnBuffActivated += ActivateBuff;
    }

    private void ActivateButton()
    {
        _timerTaxi.enabled = true;
        Image.color = Color.white;
    }

    private void ActivateBuff()
    {
       
        _allCars.TaxiCarPool.ReturnCars();
        _eventBus.NoCreateTaxiAction.Invoke();

        MasterSave.SaveData.TaxiBuffCount--;
        if (MasterSave.SaveData.TaxiBuffCount == 0)
        {
            BlockingButton();
        }
        ValueText.text = MasterSave.SaveData.TaxiBuffCount.ToString();

        _eventBus.RestartGameAction += ActivateButton;
        Image.color = Color.gray;
        _timerTaxi.enabled = false;
    }
}
