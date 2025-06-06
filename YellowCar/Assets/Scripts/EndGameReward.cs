using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EndGameReward : MonoBehaviour
{

    [SerializeField] private HeartUI _heartUI;
    [SerializeField] private float _bestTime;
    [SerializeField] private TextMeshProUGUI _stopWatchText;
    
    private Storage _storage;
    private MasterSave _masterSave;
    private float _stopWatch;
    private bool _isGameActive = false;
    private EventBus _eventBus;
    private ScoreHolder _scoreHolder;
    private int _rewardModificator = 1;

   

    [Inject]
    private void Constract(EventBus eventBus, ScoreHolder scoreHolder, Storage storage, MasterSave masterSave, [Inject(Id ="BestTime")] int bestTime)
    {
        _eventBus = eventBus;
        _scoreHolder = scoreHolder;
        _storage = storage;
        _masterSave = masterSave;
        _bestTime = bestTime;
    }

    void Start()
    {
        _stopWatchText.text = $"{_stopWatch.ToString("F0")} / {_bestTime}";
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_stopWatchText.transform.parent.GetComponent<RectTransform>());
        _eventBus.StopGameAction += RewardPlayer;
        _eventBus.ShowGainMoney += SaveMoney;
        _eventBus.RestartGameAction += () =>
        {
            _stopWatch = 0;
            _isGameActive = true;
        };
    }

    public void ChangeBestTime(int bestTime)
    {
        _bestTime = bestTime;
    }

    private void SaveMoney(int moneyCount)
    {
        _storage.Money.Value += moneyCount;
        _masterSave.SaveData.AddMoney(moneyCount);
        
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
                _eventBus.ShowGainMoney?.Invoke(_scoreHolder.Score);
                break;
            case 2:
                _eventBus.ShowGainMoney?.Invoke(Mathf.RoundToInt(_scoreHolder.Score * 1.5f));
                break;
            case 3:
                _eventBus.ShowGainMoney?.Invoke(Mathf.RoundToInt(_scoreHolder.Score * 2));
                break;
                
        }
        //LevelData levelData = _storage.LevelsInformation.FirstOrDefault(x => x.SceneID == _storage.CurrentLevelID);
        //levelData.IsLevelPast = true;
        //levelData.StarsInLevel = _rewardModificator;
        //_storage.NextSceneToUnlock = _storage.CurrentLevelID +1;
        //_masterSave.SaveData.SaveNewLevel(levelData);
        if (_rewardModificator > 3)
        {
            _rewardModificator = 3;
        }

        _eventBus.ShowGainStars.Invoke(_rewardModificator);
        _rewardModificator = 1;

        
    }

    private void Update()
    {
        if (_isGameActive == true)
        {
            _stopWatch += Time.deltaTime;
            _stopWatchText.text = $"{_stopWatch.ToString("F0")} / {_bestTime}";
        }
        
    }
}
