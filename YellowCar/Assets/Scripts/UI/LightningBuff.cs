using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LightningBuff : BuffButton
{
    [SerializeField] private ScriptableObjectPoolData _carPool;
    [SerializeField] private List<LightningPart> _lightningPart;

    [Inject]
    private void Constract(List<LightningPart> lightningPart)
    {
        _lightningPart = lightningPart;
    }

    private void ActivateBuff()
    {
        int i = 0;
        foreach (Vehicle yellowCar in _carPool.YellowCarPool.GetAllCars())
        {
            if (yellowCar.gameObject.activeSelf)
            {
                if (i < _lightningPart.Count)
                {
                    StartCoroutine(LightningStrike(yellowCar.transform.position, _lightningPart[i]));
                }
                yellowCar.CanPlayTapAudio = false;
                yellowCar.OnMouseDown();
                yellowCar.CanPlayTapAudio = true;
                i++;
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

    private IEnumerator LightningStrike(Vector3 carPosition, LightningPart lightningTarget)
    {
        lightningTarget.Target.position = carPosition;
        lightningTarget.LighteningScript.enabled = true;
        yield return new WaitForSeconds(0.5f);
        lightningTarget.LighteningScript.enabled = false;
    }
}
