using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class HeartUI : MonoBehaviour
{
    [SerializeField] private List<Image> _images;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _timerText;
    
    private int _lives = 3;
    private EventBus _eventBus;
    private ScriptableObjectPoolData _carPoolData;

    public int Lives => _lives;

    private void Initialization()
    {
        _eventBus = ServiceLocator.Instance.GetRegisterService<EventBus>();
        _eventBus.MinusLifeAction += MinusLife;
        _carPoolData = ServiceLocator.Instance.GetRegisterService<ScriptableObjectPoolData>();
        _eventBus.DoubleScore += StartTimer;
        _eventBus.OnTimerCount += ChangeTimerValue;
    }

    public void MinusLife()
    {
        foreach (var image in _images)
        {
            if(image.enabled == true)
            {
                image.enabled = false;
                _lives--;
                if (_lives < 1)
                {
                    _eventBus.StopGameAction.Invoke();
                    _gameOverPanel.SetActive(true);

                    
                }
                return;
            }
        }
    }

    private void Start()
    {
        Initialization();
       
    }

    public void RestartGame()
    {
        _eventBus.RestartGameAction.Invoke();
        foreach (var image in _images)
        {
            image.enabled = true;
        }
        _lives = 3;

        _carPoolData.DestroyCars();
    }

    private void StartTimer()
    {
        _timerText.gameObject.SetActive(true);

    }

    private void ChangeTimerValue(int valueTimer)
    {
     //   Debug.Log(valueTimer);
        _timerText.text = valueTimer.ToString();
        if (valueTimer == 0)
        {
            _timerText.gameObject.SetActive(false);
        }
    }
}
