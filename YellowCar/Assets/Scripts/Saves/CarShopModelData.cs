using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShopModelData 
{
    public bool IsBought;
    public string CarName;
    public int Cost;
    public bool IsChoose;

    public CarShopModelData(string carName, int cost)
    {
        CarName = carName;
        Cost = cost;
        if (cost == 0)
        {
            IsBought = true;
            IsChoose = true;
        }
    }
}
