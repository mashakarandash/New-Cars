using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartButton : MonoBehaviour
{
    private GameStarter _gameStarter;

    [Inject]
    private void Constract(GameStarter gameStarter)
    {
        _gameStarter = gameStarter;
    }

    public void StartGame()
    {
        _gameStarter.StartGame();
    }
}
