
using TMPro;
using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private TextMeshProUGUI _menuMoneyText;
    [SerializeField] private TextMeshProUGUI _levelChooseMoneyText;

    public override void InstallBindings()
    {
        
    }

    public override void Start() 
    {
        MasterSave master = Container.Resolve<MasterSave>();
        master.LoadAllData();
        Storage storage = Container.Resolve<Storage>();
        storage.InitializeStorage(master.SaveData);


        storage.Money.OnChange += x => _menuMoneyText.text = x.ToString();
        _menuMoneyText.text = storage.Money.Value.ToString();

        storage.Money.OnChange += x => _levelChooseMoneyText.text = x.ToString();
        _levelChooseMoneyText.text = storage.Money.Value.ToString();

        // инициализация визуальных отображений пройденных и открытых уровней, и денег
    }
}
