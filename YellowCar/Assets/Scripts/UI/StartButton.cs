using UnityEngine;
using Zenject;
using DG.Tweening;

public class StartButton : MonoBehaviour
{
    private GameStarter _gameStarter;

    [SerializeField] private Transform _vanishPosition;
    [SerializeField] private Transform _uIYellCarAnimation;

    [Inject]
    private void Constract(GameStarter gameStarter)
    {
        _gameStarter = gameStarter;
    }

    private void Start()
    {
        _uIYellCarAnimation.DORotate(new Vector3(0, 0, 360), 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void StartGame()
    {
        _gameStarter.StartGame(gameObject, _vanishPosition);
    }
}
