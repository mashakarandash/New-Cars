using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CarPool", menuName = "CarPool")]
public class ScriptableObjectPoolData : ScriptableObject, IService
{
    [SerializeField] private YellowCarBehavior _yellowCar;
    [SerializeField] private CarBehavior _redCar;
    [SerializeField] private CarBehavior _blueCar;
    [SerializeField] private CarBehavior _greenCar;
    [SerializeField] private PoliceCar _policeCar;
    [SerializeField] private Furgon _furgon;
    [SerializeField] private RainbowCar _rainbowCar;
    [SerializeField] private CarBehavior _taxiCar;

     public List <Vehicle> AllCars = new List<Vehicle>();


    public CustomPool<YellowCarBehavior> YellowCarPool { get; private set; } // свойства 
    public CustomPool<CarBehavior> RedCarPool { get; private set; }
    public CustomPool<CarBehavior> BlueCarPool { get; private set; }
    public CustomPool<CarBehavior> GreenCarPool { get; private set; }
    public CustomPool<PoliceCar> PoliceCarPool { get; private set; }
    public CustomPool<Furgon> FurgonPool { get; private set; }
    public CustomPool<RainbowCar> RainbowCarPool { get; private set; }
    public CustomPool<CarBehavior> TaxiCarPool { get; private set; }

    public void Initialization()
    {
        
        YellowCarPool = new CustomPool<YellowCarBehavior>(_yellowCar, 6);
        RedCarPool = new CustomPool<CarBehavior>(_redCar, 10);
        GreenCarPool = new CustomPool<CarBehavior>(_greenCar, 8);
        BlueCarPool = new CustomPool<CarBehavior>(_blueCar, 7);
        PoliceCarPool = new CustomPool<PoliceCar>(_policeCar, 3);
        FurgonPool = new CustomPool<Furgon>(_furgon, 3);
        RainbowCarPool = new CustomPool<RainbowCar>(_rainbowCar, 7);
        TaxiCarPool = new CustomPool<CarBehavior>(_taxiCar, 8);

        RefreshAllCarsList();
        
       

    }

    public void RemoveAllCar()
    {
        YellowCarPool.ReturnAllCarToPool();
        RedCarPool.ReturnAllCarToPool();
        GreenCarPool.ReturnAllCarToPool();
        BlueCarPool.ReturnAllCarToPool();
        PoliceCarPool.ReturnAllCarToPool();
        FurgonPool.ReturnAllCarToPool();
        RainbowCarPool.ReturnAllCarToPool();
        TaxiCarPool.ReturnAllCarToPool();
    }

    public void DestroyCars()
    {
        YellowCarPool.ReturnCars();
        RedCarPool.ReturnCars();
        BlueCarPool.ReturnCars();
        GreenCarPool.ReturnCars();
        PoliceCarPool.ReturnCars();
        FurgonPool.ReturnCars();
        RainbowCarPool.ReturnCars();
        TaxiCarPool.ReturnCars();
    }

    public void RefreshAllCarsList()
    {
        AllCars.Clear();
        AllCars.AddRange(YellowCarPool.GetAllCars());
        AllCars.AddRange(RedCarPool.GetAllCars());
        AllCars.AddRange(GreenCarPool.GetAllCars());
        AllCars.AddRange(BlueCarPool.GetAllCars());
        AllCars.AddRange(PoliceCarPool.GetAllCars());
        AllCars.AddRange(FurgonPool.GetAllCars());
        AllCars.AddRange(RainbowCarPool.GetAllCars());
        AllCars.AddRange(TaxiCarPool.GetAllCars());
    }
}
