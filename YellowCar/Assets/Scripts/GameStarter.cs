using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Zenject;
using DG.Tweening;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _firstCam;
    [SerializeField] private CinemachineVirtualCamera _secondCam;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private Transform _vanishPosition;
    [SerializeField] private float _vanishAnimationSpeed;
    [SerializeField] private Transform _uIYellCarAnimation;

    private EventBus _eventBus;

    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Start()
    {
        _uIYellCarAnimation.DORotate(new Vector3(0, 0, 360), 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void StartGame()
    {
        _eventBus.RestartGameAction.Invoke();
        
        _secondCam.Priority = 15;
        _startButton.transform.DOMoveY(_vanishPosition.position.y, _vanishAnimationSpeed).SetEase(Ease.InOutSine);
    }

    

}
