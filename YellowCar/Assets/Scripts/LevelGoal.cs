using UnityEngine;
using Zenject;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private int _scoreToGrow;
    [SerializeField] private CointsChanger _cointsChanger;
    [SerializeField] private GameObject _winMenu;

    private EventBus _eventBus;
    public int ScoreToGrow => _scoreToGrow;

    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Start()
    {
        _eventBus.ScoreCheck += ScoreCheck;
        _eventBus.RestartGameAction += () => _winMenu.SetActive(false);
        
    }

    

    private void ScoreCheck()
    {
       
        if (_cointsChanger.Score >= _scoreToGrow)
        {
            _eventBus.StopGameAction.Invoke();
            _winMenu.SetActive(true);
        }

    }
}
