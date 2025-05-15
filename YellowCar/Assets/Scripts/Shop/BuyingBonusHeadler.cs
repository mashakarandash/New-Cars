using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuyingBonusHeadler : MonoBehaviour
{
    
    private Storage _storage;
    private MasterSave _masterSave;
    private int _modelID;

    [SerializeField] private int _lightningBonusCost;
    [SerializeField] private int _freezeBonusCost;
    [SerializeField] private int _taxiBonusCost;
    [SerializeField] private List<Sprite> _carModel;
    [SerializeField] private Image _model;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Image _buttonSprite;
    [SerializeField] private ButtonSetting _buttonPrice;
    [SerializeField] private ButtonSetting _buttonChoose;
    [SerializeField] private ButtonSetting _buttonNowChoosed;


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
        _masterSave.SaveData.Money = _storage.Money.Value;
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
        _masterSave.SaveData.Money = _storage.Money.Value;
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
        _masterSave.SaveData.Money = _storage.Money.Value;
    }

    public void SwipeCarModel(int i)
    {
        if (i == 1)
        {
            _modelID++;
            if (_modelID >= _carModel.Count)
            {
                _modelID = 0;
            }

            Debug.Log("swipe right");
            Sprite sprite = _carModel[_modelID];
            _model.sprite = sprite;

            CarShopModelData carSave = _masterSave.SaveData.CarSaves.FirstOrDefault(x => x.CarName == _model.sprite.name);
            ButtonBinding(carSave);
        }

        else if (i == -1)
        {
            Debug.Log("swipe left");
            _modelID--;
            if (_modelID < 0)
            {
                _modelID = _carModel.Count - 1;
            }

            Debug.Log("swipe right");
            Sprite sprite = _carModel[_modelID];
            _model.sprite = sprite;
            CarShopModelData carSave = _masterSave.SaveData.CarSaves.FirstOrDefault(x => x.CarName == _model.sprite.name);
            ButtonBinding(carSave);
        }
    }

    private void ButtonBinding(CarShopModelData carSave)
    {
        if (carSave != null)
        {
            _button.onClick.RemoveAllListeners();
            if (carSave.IsBought == true && carSave.IsChoose == true)
            {
                _buttonChoose.Button.gameObject.SetActive(false);
                _buttonNowChoosed.Button.gameObject.SetActive(true);
                _buttonPrice.Button.gameObject.SetActive(false);
                
            }
            else if (carSave.IsBought == true && carSave.IsChoose == false)
            {
                _buttonNowChoosed.Button.gameObject.SetActive(false);
                _buttonChoose.Button.gameObject.SetActive(true);
                _buttonPrice.Button.gameObject.SetActive(false);
                _buttonChoose.Button.onClick.AddListener(() => ChooseNewCarSkin(carSave));
            }
            else if(carSave.IsBought == false)
            {
                _buttonNowChoosed.Button.gameObject.SetActive(false);
                _buttonChoose.Button.gameObject.SetActive(false);
                _buttonPrice.Button.gameObject.SetActive(true);
                _buttonPrice.ButtonText.text = carSave.Cost.ToString();
                _buttonPrice.Button.onClick.AddListener(() => BuyNewCarSkin(carSave));
            }
        }

    }

    private void BuyNewCarSkin(CarShopModelData carData)
    {
        if (carData.Cost<=_storage.Money.Value)
        {
            carData.IsBought = true;
            _storage.Money.Value -= carData.Cost;
            _masterSave.SaveData.Money = _storage.Money.Value;
           
            foreach (var item in _masterSave.SaveData.CarSaves)
            {
                item.IsChoose = false;
            }
            carData.IsChoose = true;

            _buttonChoose.Button.gameObject.SetActive(false);
            _buttonNowChoosed.Button.gameObject.SetActive(true);
            _buttonPrice.Button.gameObject.SetActive(false);
        }
    }

    private void ChooseNewCarSkin(CarShopModelData carData)
    {
        foreach(var item in _masterSave.SaveData.CarSaves)
        {
            item.IsChoose = false;
        }

        
        carData.IsChoose = true;
        _buttonChoose.Button.gameObject.SetActive(false);
        _buttonNowChoosed.Button.gameObject.SetActive(true);
        _buttonPrice.Button.gameObject.SetActive(false);
        //изменить вид внутри префаба выбранной можели машин

    }

}

[Serializable]
public class ButtonSetting
{
    public Button Button;
    public TextMeshProUGUI ButtonText;


}

