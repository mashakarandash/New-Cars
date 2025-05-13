using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class BuffButton : MonoBehaviour
{
    [SerializeField] private int _coolDown;

    [SerializeField] protected Image Timer;
    [SerializeField] protected Image Image;
    [SerializeField] protected TextMeshProUGUI ValueText;
    [SerializeField] protected Button Button;

    private EventBus _eventBus;
    private bool _isBlocked = false;

    protected bool CanActivateBuff;
    protected MasterSave MasterSave;
    protected event Action OnBuffActivated;

    [Inject]
    private void Constract(EventBus eventBus, MasterSave masterSave)
    {
        _eventBus = eventBus;
        MasterSave = masterSave;
    }

    protected virtual void Start()
    {
        Button.onClick.AddListener(ActivateBuff);
        _eventBus.RestartGameAction+=()=> StartCoroutine(TimerWorkCoroutine());
        
    }

    private IEnumerator TimerWorkCoroutine()
    {
        Debug.Log("тдет перезаряд");
        float clockWise = 0;
        //CanActivateBuff = false;

        while (clockWise < _coolDown)
        {
            clockWise += Time.deltaTime;
            float procent = clockWise / _coolDown;
            Timer.fillAmount = procent;

            yield return null;
        }
        CanActivateBuff = true;
    }

    private void ActivateBuff()
    {
        if (CanActivateBuff == false || _isBlocked == true)
        {
            return;
        }

       // MasterSave.SaveAllData();
        CanActivateBuff = false;
        OnBuffActivated.Invoke();
        StartCoroutine(TimerWorkCoroutine());
    }

    protected void BlockingButton()
    {
        Image.color = Color.gray;
        Timer.enabled = false;
        _isBlocked = true;
    }

}
