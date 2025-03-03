using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class CointsChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField, Range(0,30)] private float _doubleScoreBonusDuration;
    private LevelGoal _levelGoal;
    private EventBus _eventBus;
    public int Score = 0;
    private bool _canMulti = false;

    [Inject]
    private void Constract(EventBus eventBus, LevelGoal levelGoal)
    {
        _eventBus = eventBus;
        _levelGoal = levelGoal;
    }

    void Start()
    {
        
        _text.text = $"Очки: {Score} / {_levelGoal.ScoreToGrow}";
        _eventBus.ScoreChanged += ChangeScore;// таким образом мы подписываемся на события ScoreChanged, которые находятся в классе EventBus
        _eventBus.RestartGameAction += NullCoins;
        _eventBus.DoubleScore += DoubleScore;
       
    }

    public void ChangeScore()
    {
        if (_canMulti)
        {
            Score++;
            Score++;
        }
        else
        {
            Score++;
        }
        _text.text = $"Очки: {Score} / {_levelGoal.ScoreToGrow}";
        _eventBus.ScoreCheck.Invoke();
    }

    private void NullCoins()
    {
        Score = 0;
        _text.text = $"Очки: {Score}";
    }

    private void DoubleScore()
    {
        StartCoroutine(TimeToScore());
    }

    public IEnumerator TimeToScore()
    {
        _canMulti = true;
        yield return new WaitForSeconds(_doubleScoreBonusDuration);
        _canMulti = false;
       
    }

    





    
    
}
