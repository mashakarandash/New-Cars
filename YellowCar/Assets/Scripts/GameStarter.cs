using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _firstCam;
    [SerializeField] private CinemachineVirtualCamera _secondCam;
    [SerializeField] private GameObject _startButton;
    private EventBus _eventBus;

    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void StartGame()
    {
        _eventBus.RestartGameAction.Invoke();
        _startButton.SetActive(false);
        _secondCam.Priority = 15;
    }

    

}
