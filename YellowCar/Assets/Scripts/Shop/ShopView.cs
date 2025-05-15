using TMPro;
using UnityEngine;
using Zenject;

public class ShopView : MonoBehaviour
{
    private Storage _storage;
    private MasterSave _masterSave;
    [SerializeField] private TextMeshProUGUI _moneyText;
   /* [SerializeField] private TextMeshProUGUI _freezeCountBonusText;
    [SerializeField] private TextMeshProUGUI _lightningCountBonusText;
    [SerializeField] private TextMeshProUGUI _taxiCountBonusText;*/

    [Inject]
    private void Constract(Storage storage, MasterSave masterSave)
    {
        _storage = storage;
        _masterSave = masterSave;
    }

    /* private void OnEnable()
     {
         _moneyText.text =  "";
     }*/
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        _storage.Money.OnChange += x => _moneyText.text = x.ToString();
       /* _storage.FreezeBonusCount.OnChange += x => _freezeCountBonusText.text = x.ToString();
        _storage.LightningBonusCount.OnChange += x => _lightningCountBonusText.text = x.ToString();
        _storage.TaxiBonusCount.OnChange += x => _taxiCountBonusText.text = x.ToString();*/

        _moneyText.text = _storage.Money.Value.ToString();
       /* _freezeCountBonusText.text = _storage.FreezeBonusCount.Value.ToString();
        _lightningCountBonusText.text = _storage.FreezeBonusCount.Value.ToString();
        _taxiCountBonusText.text = _storage.TaxiBonusCount.Value.ToString();*/
    }
    public void SaveAllData()
    {
        _masterSave.SaveAllData();
    }

    private void OnDisable()
    {
        Debug.Log("покупки сохранены");
        _storage.Money.RemoveAllListeners();
        _storage.FreezeBonusCount.RemoveAllListeners();
        _storage.LightningBonusCount.RemoveAllListeners();
        _storage.TaxiBonusCount.RemoveAllListeners();

        //_masterSave.SaveAllData();
    }

   
}

