using UnityEngine;
/// <summary>
/// эта фабрика создает красную машину
/// </summary>
public class TransportBuilder : CarBuilder 
{
    

    public override GameObject Create(int speed) //переопределенный метод, который обязан возвращать какой-то объект типа gameObject и принимает параметр int
    {
        var prefab1 = Resources.Load<GameObject>(PathToPrefab); //передается в переменную prefab1 наш объект, который найден через поиск в папке Resources
        GameObject gameObject = GameObject.Instantiate(prefab1); // создаем этот объект. Instantiate - метод юнити который создает игровой объект на сцене.




        if (gameObject.TryGetComponent<Vehicle>(out Vehicle car)) // метод который пытается получить компонент у текущего объекта, который мы указываем в скобках
        {
            car.SetSpeed(speed);
        }

        return gameObject;
    }
    
  
}
