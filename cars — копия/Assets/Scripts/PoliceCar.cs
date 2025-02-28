
public class PoliceCar : Vehicle
{
    private EventBus _eventBus;
    private ScriptableObjectPoolData _carPoolData;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        _eventBus = ServiceLocator.Instance.GetRegisterService<EventBus>();
        _carPoolData = ServiceLocator.Instance.GetRegisterService<ScriptableObjectPoolData>();
    }

    public override void OnMouseDown()
    {
        if (IsTemporaryYellowCar)
        {
            _eventBus.ScoreChanged.Invoke();
            gameObject.SetActive(false);
            return;
        }  

        _carPoolData.RemoveAllCar();
        gameObject.SetActive(false);
    }
}
