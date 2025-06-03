
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private TextMeshProUGUI _menuMoneyText;
    [SerializeField] private TextMeshProUGUI _levelChooseMoneyText;
    [SerializeField] private List<Sprite> _carModel;
    [SerializeField] private List<int> _carsCost;

    private Storage _storage;

    public override void InstallBindings()
    {
        
    }

    public void OnDestroy()
    {
        _storage.Money.RemoveAllListeners();
    }

    public override void Start() 
    {
        MasterSave master = Container.Resolve<MasterSave>();
        master.LoadAllData();
        Storage storage = Container.Resolve<Storage>();
        _storage = storage;
        storage.InitializeStorage(master.SaveData);


        storage.Money.OnChange += x => _menuMoneyText.text = x.ToString();
        _menuMoneyText.text = storage.Money.Value.ToString();

        storage.Money.OnChange += x =>
        {
            _levelChooseMoneyText.text = x.ToString();
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
        };
        _levelChooseMoneyText.text = storage.Money.Value.ToString();

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
        // инициализация визуальных отображений пройденных и открытых уровней, и денег
    }

    [ContextMenu("SaveNewCarSkins")]
    public void SetNewCarsModelToSave()
    {
        MasterSave master = Container.Resolve<MasterSave>();

        List<CarShopModelData> list = new List<CarShopModelData>();
        int i = 0;
        foreach (var item in _carModel)
        {
            list.Add(new CarShopModelData(item.name, _carsCost[i]));
            i++;
        }

        master.SaveData.CarSaves = list.ToArray();
        master.SaveAllData();
    }
}
