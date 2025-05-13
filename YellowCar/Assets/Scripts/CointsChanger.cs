using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class CointsChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField, Range(0,30)] private float _doubleScoreBonusDuration;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private float _vanishAnimationSpeed;
    [SerializeField] private Transform _vanishPosition;

    private int _scoreToGrow;
    private ScoreHolder _scoreHolders;
    private EventBus _eventBus;
    private bool _canMulti = false;

    [Inject]
    private void Constract(EventBus eventBus, [Inject (Id = "ScoreToGrow")] int scoreToGrow, ScoreHolder scoreHolders)
    {
        _eventBus = eventBus;
        _scoreToGrow = scoreToGrow;
        _scoreHolders = scoreHolders;
    }
    
    void Start()
    {
        
        _text.text = $"{_scoreHolders.Score} / {_scoreToGrow}";
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
        _eventBus.ScoreChanged += ChangeScore;// таким образом мы подписываемся на события ScoreChanged, которые находятся в классе EventBus
        _eventBus.RestartGameAction += NullCoins;
        _eventBus.RestartGameAction += RestartGame;
        _eventBus.DoubleScore += DoubleScore;
       
    }

    public void ChangeScore()
    {
        if (_canMulti)
        {
            _scoreHolders.Score++;
            _scoreHolders.Score++;
        }
        else
        {
            _scoreHolders.Score++;
        }
        _text.text = $"{_scoreHolders.Score} / {_scoreToGrow}";
        ScoreCheck();
    }

    private void NullCoins()
    {
        _scoreHolders.Score = 0;
        _text.text = $"{_scoreHolders.Score} / {_scoreToGrow}";
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

    private void ScoreCheck()
    {

        if (_scoreHolders.Score >= _scoreToGrow)
        {
            _eventBus.StopGameAction.Invoke();
            _winMenu.transform.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, _vanishAnimationSpeed).SetEase(Ease.InOutSine);
        }

    }

    private void RestartGame()
    {
        _winMenu.transform.DOMoveY(_vanishPosition.position.y, _vanishAnimationSpeed).SetEase(Ease.InOutSine);
    }
}
