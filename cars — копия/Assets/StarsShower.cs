using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class StarsShower : MonoBehaviour
{
    [SerializeField] private List<GameObject> _stars;
    [SerializeField] private TextMeshProUGUI _money;

    void Start()
    {
        EventBus _eventBus = ServiceLocator.Instance.GetRegisterService<EventBus>();
        _eventBus.ShowGainStars += ShowStars;
        _eventBus.RestartGameAction += () =>
          {
              foreach (var star in _stars)
              {
                  star.transform.DOScale(1, 0);
                  star.SetActive(false);
              }
          };
        _eventBus.ShowGainMoney += ShowMoney;
    }

    private void ShowMoney(int obj)
    {
        _money.text = obj.ToString();
    }

    private void ShowStars(int starsCount)
    {
        StartCoroutine(AnimateStarsApperanceCoroutine(starsCount));
    }

    private IEnumerator AnimateStarsApperanceCoroutine(int  starsCount)
    {
        for (int i = 0; i < starsCount; i++)
        {
            _stars[i].SetActive(true);
            _stars[i].transform.DOScale(1.5f, 1).SetLoops(2, LoopType.Yoyo);
            yield return new WaitForSeconds(1);
        }
    }
}
