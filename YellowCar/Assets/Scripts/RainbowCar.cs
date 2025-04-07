using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RainbowCar : Vehicle
{
    [SerializeField] private float _delay;
    [SerializeField] private Renderer _renderer;

    private EventBus _eventBus;
    private Material _newMaterial;

    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Start()
    {
        _newMaterial = _renderer.material;
        _renderer.material = _newMaterial;
        StartCoroutine(ColorChange());
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

        if (_renderer.material.color == Color.yellow)
        {
            PlayTapAudio(_yellowCarTapAudio);
            _eventBus.ScoreChanged.Invoke();
        }
        else
        {
            PlayTapAudio(_anotherCarTapAudio);
            _eventBus.MinusLifeAction.Invoke();
        }
        gameObject.SetActive(false);
    }

    public Color RandomColor()
    {
        Color color = new Color();
        int i = Random.Range(0, 4);
        switch (i)
        {
            case 0:
                color = Color.red;
                break;
            case 1:
                color = Color.green;
                break;
            case 2:
                color = Color.blue;
                break;
            case 3:
                color = Color.yellow;
                break;
            default:
                Debug.Log("значение не входит в диапазон");
                break;
        }
        return color;
    }

    public IEnumerator ColorChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);
            if (!IsTemporaryYellowCar)
            {
               _renderer.material.color = RandomColor();

            }

        }
    }

    
}
