using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class EndGameReward : MonoBehaviour
{

    [SerializeField] private HeartUI _heartUI;
    [SerializeField] private float _bestTime;
    [SerializeField] private CointsChanger _coinsChanger;
    [SerializeField] private TextMeshProUGUI _stopWatchText;

    private float _stopWatch;
    private bool _isGameActive = true;
    private EventBus _eventBus;
    private int _rewardModificator = 1;

    [Inject]
    private void Constract(EventBus eventBus, CointsChanger cointsChanger)
    {
        _eventBus = eventBus;
        _coinsChanger = cointsChanger;
    }

    void Start()
    {
        _eventBus.StopGameAction += RewardPlayer;
        _eventBus.ShowGainMoney += x => Debug.Log("pаработано денег=" + x);
        _eventBus.RestartGameAction += () =>
        {
            _stopWatch = 0;
            _isGameActive = true;
        };
    }

    private void RewardPlayer()
    {
        Debug.Log("метод вызван");
        Debug.Log(_eventBus);
        _isGameActive = false;

        if (_heartUI.Lives == 3)
        {
            _rewardModificator++;
        }

        if (_stopWatch < _bestTime)
        {
            _rewardModificator++;
        }

        switch (_rewardModificator)
        {
            case 1:
                _eventBus.ShowGainMoney?.Invoke(_coinsChanger.Score);
                break;
            case 2:
                _eventBus.ShowGainMoney?.Invoke(Mathf.RoundToInt(_coinsChanger.Score * 1.5f));
                break;
            case 3:
                _eventBus.ShowGainMoney?.Invoke(Mathf.RoundToInt(_coinsChanger.Score * 2));
                break;
                
        }
        _eventBus.ShowGainStars.Invoke(_rewardModificator);
        _rewardModificator = 1;

        
    }

    private void Update()
    {
        if (_isGameActive == true)
        {
            _stopWatch += Time.deltaTime;
            _stopWatchText.text = _stopWatch.ToString("F2");
        }
        
    }
}
