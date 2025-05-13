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
    
    [SerializeField] private float _vanishAnimationSpeed;
   

    private EventBus _eventBus;

    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void StartGame(GameObject startButton, Transform vanishPosition)
    {
        _eventBus.RestartGameAction.Invoke();
        
        _secondCam.Priority = 15;
        startButton.transform.DOMoveY(vanishPosition.position.y, _vanishAnimationSpeed).SetEase(Ease.InOutSine);
    }

    

}
