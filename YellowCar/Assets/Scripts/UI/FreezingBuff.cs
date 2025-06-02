using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FreezingBuff : BuffButton
{
    [SerializeField] private ScriptableObjectPoolData _allCars;
    [SerializeField] private float _buffDuration;
    [SerializeField] private int _freezingSpeed;
    [SerializeField] private Image _freezingImage;

    protected override void Start()
    {
        base.Start();
        if (MasterSave.SaveData.FreezeBuffCount == 0)
        {
            BlockingButton();
        }
        ValueText.text = MasterSave.SaveData.FreezeBuffCount.ToString();
        OnBuffActivated += ActivateBuff;
    }

    private void ActivateBuff()
    {
        StartCoroutine(FreezeCarsCoroutine());
        MasterSave.SaveData.FreezeBuffCount--;
        if (MasterSave.SaveData.FreezeBuffCount == 0)
        {
            BlockingButton();
        }
        ValueText.text = MasterSave.SaveData.FreezeBuffCount.ToString();
    }

    private IEnumerator FreezeCarsCoroutine()
    {
        foreach (var car in _allCars.AllCars)
        {
            car.SaveSpeed = car.Speed;
            car.NavMeshAgent.speed = _freezingSpeed;
        }
        Tween tween = _freezingImage.DOFade(1, 2);

       // Debug.Log(_buffDuration + "анчало фриза");
        yield return new WaitForSeconds(_buffDuration);
        _freezingImage.DOFade(0, 1);
       // Debug.Log(_buffDuration + "rонец фриза");
        foreach (var car in _allCars.AllCars)
        {
            car.NavMeshAgent.speed = car.SaveSpeed;
        }
    }
}
