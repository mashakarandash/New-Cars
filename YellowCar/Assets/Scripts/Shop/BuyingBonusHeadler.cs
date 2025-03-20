using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuyingBonusHeadler : MonoBehaviour
{
    
    private Storage _storage;
    private MasterSave _masterSave;
    [SerializeField] private int _lightningBonusCost;
    [SerializeField] private int _freezeBonusCost;
    [SerializeField] private int _taxiBonusCost;

    [Inject]
    private void Constract(Storage storage, MasterSave masterSave)
    {
        _storage = storage;
        _masterSave = masterSave;
    }

    public void BuyLightningBonus()
    {
        if (_storage.Money.Value < _lightningBonusCost)
        {
            return;
        }
        _storage.Money.Value = _storage.Money.Value - _lightningBonusCost;
        _storage.LightningBonusCount.Value++;
        _masterSave.SaveData.SaveAllBonus(_storage.LightningBonusCount.Value, _storage.FreezeBonusCount.Value, _storage.TaxiBonusCount.Value);
    }

    public void BuyFreezeBonus()
    {
        if (_storage.Money.Value < _freezeBonusCost)
        {
            return;
        }
        _storage.Money.Value = _storage.Money.Value - _freezeBonusCost;
        _storage.FreezeBonusCount.Value++;
        _masterSave.SaveData.SaveAllBonus(_storage.LightningBonusCount.Value, _storage.FreezeBonusCount.Value, _storage.TaxiBonusCount.Value);
    }

    public void BuyTaxiBonus()
    {
        if (_storage.Money.Value < _taxiBonusCost)
        {
            return;
        }
        _storage.Money.Value = _storage.Money.Value - _taxiBonusCost;
        _storage.TaxiBonusCount.Value++;
        _masterSave.SaveData.SaveAllBonus(_storage.LightningBonusCount.Value, _storage.FreezeBonusCount.Value, _storage.TaxiBonusCount.Value);
    }
}
