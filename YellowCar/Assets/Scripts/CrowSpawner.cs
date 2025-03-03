using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CrowSpawner : MonoBehaviour
{

    [SerializeField] private Transform _startMovement;
    [SerializeField] private Transform _endMovement;
    [SerializeField] private Transform _crow;
    [SerializeField] private float _minDiapasonToSpawn;
    [SerializeField] private float _maxDiapasonToSpawn;
    [SerializeField] private int _timeToFly;
    [SerializeField] private int _scaleCrow;
    [SerializeField] private int _vanish;

    private EventBus _eventBus;
    private bool _gameIsActive = true;

    [Inject]

    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    void Start()
    {
        
        _eventBus.StopGameAction += StopGame;
        _eventBus.RestartGameAction += RestartGame;
        StartCoroutine(StartSpawningCrowCoroutine());
    }



    private void StopGame()
    {
        _gameIsActive = false;
    }

    private void RestartGame()
    {
        _gameIsActive = true;
    }

    private IEnumerator StartSpawningCrowCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minDiapasonToSpawn, _maxDiapasonToSpawn));
            if (_gameIsActive == false)
            {
                continue;
            }
            Tween animationInfo;
            animationInfo = _crow.DOMove(_endMovement.position, _timeToFly).From(_startMovement.position);
            _crow.DOScale(_scaleCrow, _timeToFly).SetEase(Ease.Linear);
            yield return animationInfo.WaitForCompletion();
            animationInfo = _crow.DOScale(15f, 0.25f);
            yield return animationInfo.WaitForCompletion();
            animationInfo = _crow.GetComponent<Image>().DOFade(0, _vanish);
            yield return animationInfo.WaitForCompletion();
            _crow.GetComponent<Image>().DOFade(1, 0);
            _crow.DOScale(1, 0);


            _crow.transform.position = _startMovement.position;
        }
    }

}
