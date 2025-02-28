using UnityEngine;

public class LevelGoal : MonoBehaviour, IService
{
    [SerializeField] private int _scoreToGrow;
    [SerializeField] private CointsChanger _cointsChanger;
    [SerializeField] private GameObject _winMenu;

    private EventBus _eventBus;
    public int ScoreToGrow => _scoreToGrow;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        _eventBus = ServiceLocator.Instance.GetRegisterService<EventBus>();
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
