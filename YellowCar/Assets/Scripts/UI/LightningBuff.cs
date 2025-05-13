using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBuff : BuffButton
{
    [SerializeField] private ScriptableObjectPoolData _carPool;

    private void ActivateBuff()
    {
        
        foreach (Vehicle yellowCar in _carPool.YellowCarPool.GetAllCars())
        {
            if (yellowCar.gameObject.activeSelf)
            {
                yellowCar.CanPlayTapAudio = false;
                yellowCar.OnMouseDown();
                yellowCar.CanPlayTapAudio = true;
            }
        }
        MasterSave.SaveData.LightBuffCount--;
        if (MasterSave.SaveData.LightBuffCount == 0)
        {
            BlockingButton();
        }
        ValueText.text = MasterSave.SaveData.LightBuffCount.ToString();
    }

    protected override void Start()
    {
        base.Start();
        if(MasterSave.SaveData.LightBuffCount == 0)
        {
            BlockingButton();
        }

        ValueText.text = MasterSave.SaveData.LightBuffCount.ToString();
        OnBuffActivated += ActivateBuff;
    }
}
