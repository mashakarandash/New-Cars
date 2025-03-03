using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class YellowCarBehavior : Vehicle
{
    private EventBus _eventBus;
    private int Sobaka;

    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
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