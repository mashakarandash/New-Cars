
using Zenject;

public class PoliceCar : Vehicle
{
    private EventBus _eventBus;
    private ScriptableObjectPoolData _carPoolData;

    [Inject]

    private void Constract(EventBus eventBus, ScriptableObjectPoolData carPoolData)
    {
        _eventBus = eventBus;
        _carPoolData = carPoolData;
    }


    private void Start()
    {
        
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
