using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCarBehavior : Vehicle
{
    private EventBus _eventBus;
    private int Sobaka;

    private void Start()
    {
        Initialization();
        
    }

    private void Initialization()
    {
        _eventBus = ServiceLocator.Instance.GetRegisterService<EventBus>();
    }

    public override void OnMouseDown() //засчитываем очки в этом методе
    {
        /* Sobaka++;
         Debug.Log("нажатие" + Sobaka);
         _eventBus.ScoreChanged.Invoke();
         gameObject.SetActive(false);*/

        Sobaka++;
        _eventBus.ScoreChanged.Invoke();
        gameObject.SetActive(false);
    }

   

    public override void DoDestroy()
    {
        base.DoDestroy();
        _eventBus.MinusLifeAction.Invoke();
    }

    private void OnMouseUp()
    {
       /* Sobaka++;
        _eventBus.ScoreChanged.Invoke();
        gameObject.SetActive(false);*/
    }
}

public interface ICar
{
    bool LeftSide { get; set; }
    bool RightSide { get; set; }

}