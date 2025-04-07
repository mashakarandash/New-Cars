
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
            base.OnMouseDown();
            _eventBus.ScoreChanged.Invoke();
            gameObject.SetActive(false);
            return;
        }

        base.OnMouseDown();
        _carPoolData.RemoveAllCarFromPolice();
        gameObject.SetActive(false);
    }
}
