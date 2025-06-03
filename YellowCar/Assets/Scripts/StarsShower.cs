using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Zenject;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarsShower : MonoBehaviour
{
    [SerializeField] private List<Image> _stars;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private Sprite _starWin;
    [SerializeField] private Sprite _starOver;

    private EventBus _eventBus;
    private MasterSave _masterSave;

    [Inject]
    private void Constract(EventBus eventBus, MasterSave masterSave)
    {
        _eventBus = eventBus;
        _masterSave = masterSave;
    }

    void Start()
    {
        _eventBus.ShowGainStars += ShowStars;
        _eventBus.RestartGameAction += () =>
          {
              foreach (var star in _stars)
              {
                  star.transform.DOScale(1, 0);
                 // star.gameObject.SetActive(false);
              }
          };
        _eventBus.ShowGainMoney += ShowMoney;
    }

    private void ShowMoney(int obj)
    {
        _money.text = "+ " + obj.ToString();
    }

    private void ShowStars(int starsCount)
    {
        _masterSave.SaveData.SaveLevelCondition(SceneManager.GetActiveScene().buildIndex, starsCount);
        StartCoroutine(AnimateStarsApperanceCoroutine(starsCount));
    }

    private IEnumerator AnimateStarsApperanceCoroutine(int  starsCount)
    {
        int k = 0;
        Debug.Log(starsCount);
        for (int i = 0; i < starsCount; i++)
        {
          //  _stars[i].gameObject.SetActive(true);
            _stars[i].sprite = _starWin;

            k++;
            _stars[i].transform.DOScale(1.2f, 1).SetLoops(2, LoopType.Yoyo);
            yield return new WaitForSeconds(1);
        }
        if (k < 2)
        {
            for (int f = k; f < starsCount; f++)
            {
              //  _stars[f].gameObject.SetActive(true);
                _stars[f].sprite = _starOver;
                
            }
        }
    }
}
