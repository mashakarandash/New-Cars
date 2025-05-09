using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class BuffButton : MonoBehaviour
{
    [SerializeField] private Image _timer;
    [SerializeField] private int _coolDown;

    [SerializeField] protected Button Button;

    protected bool CanActivateBuff;
    private EventBus _eventBus;

    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Start()
    {
        Button.onClick.AddListener(ActivateBuff);
        _eventBus.RestartGameAction+=()=> StartCoroutine(TimerWorkCoroutine());
    }

    private IEnumerator TimerWorkCoroutine()
    {
        float clockWise = 0;
        //CanActivateBuff = false;

        while (clockWise < _coolDown)
        {
            clockWise += Time.deltaTime;
            float procent = clockWise / _coolDown;
            _timer.fillAmount = procent;

            yield return null;
        }
        CanActivateBuff = true;
    }

    public virtual void ActivateBuff()
    {
        if (CanActivateBuff == false)
        {
            return;
        }

        CanActivateBuff = false;
        StartCoroutine(TimerWorkCoroutine());
    }

}
