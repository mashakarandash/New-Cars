using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Zenject;

public class CustomPool<T> where T : MonoBehaviour // T - Generic verible, универсальная переменная/класс
{
    // private YellowCarBehavior _yellowCarPrefab;
    // private List<YellowCarBehavior> _yellowCarList;
    private T _carPrefab;
    private List<T> _carList;
    private DiContainer _container;

    public CustomPool(T prefab, int InitObjectCount) // конструктор (используется всегда для инициализации)
    {
        _carPrefab = prefab;
        _carList = new List<T>();
        for (int i = 0; i < InitObjectCount; i++) //создаем машинку и засовываем ее в склад
        {
            var carGameObject = GameObject.Instantiate(_carPrefab);
            _carList.Add(carGameObject);
            carGameObject.gameObject.SetActive(false);
            
        }
    }

    public CustomPool(T prefab, int InitObjectCount, DiContainer container) // конструктор (используется всегда для инициализации)
    {
        _carPrefab = prefab;
        _carList = new List<T>();
        _container = container;
        for (int i = 0; i < InitObjectCount; i++) //создаем машинку и засовываем ее в склад
        {
            CreateCar();

        }
    }

    public T GetCar() //получение первой свободной машинки
    {

        var carObject = _carList.FirstOrDefault(car => ! car.isActiveAndEnabled);

        if (carObject == null)
        {
            carObject = CreateCar();
        }
        carObject.gameObject.SetActive(true);
        return carObject;
        
    }

    public T CreateCar() // метод создания машинки, если машинок не хватает
    {
        var carGameObject = GameObject.Instantiate(_carPrefab);
        _carList.Add(carGameObject);
        carGameObject.gameObject.SetActive(false);
        if (_container != null)
        {
            _container.Inject(carGameObject);
        }
        return carGameObject;
    }

    public void DropCarBackToPool(T carObject) // возврат машинки в неактивное состояние
    {
        carObject.gameObject.SetActive(false);
    }

    public void ReturnAllCarToPool()
    {
        foreach (var car in _carList)
        {
            if (car is Vehicle vehicle && vehicle.IsStilledByUFO == true)
            {
                continue;
            }

            if (car is YellowCarBehavior yellowCar && car.isActiveAndEnabled)
            {
                yellowCar.CanPlayTapAudio = false;
                yellowCar.OnMouseDown();
                yellowCar.CanPlayTapAudio = true;
            }
            car.gameObject.SetActive(false);
            
        }
    }

    public void ReturnCars()
    {
        foreach (var car in _carList)
        {
            car.gameObject.SetActive(false);
        }

    }

    public List<Vehicle> GetAllCars()
    {
        List<Vehicle> vehicles = new List<Vehicle>();
        foreach (var item in _carList)
        {
            vehicles.Add(item as Vehicle);
        }
        return vehicles;

       

    }
}
