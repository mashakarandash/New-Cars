using System;
using UnityEngine;

public class EventBus 
{
    public bool IsTimerActive;

    public Action ScoreChanged;

    // action - делегат, это переменная, хранящая в себе ссылки на методы

    public Action MinusLifeAction;
    public Action StopGameAction;
    public Action RestartGameAction;
    public Action ScoreCheck;
    public Action DoubleScore;
    public Action OnRestartTimer;

    public Action<int> OnTimerCount;
    public Action<int> ShowGainMoney;
    public Action<int> ShowGainStars;
}

    
