using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _initialPos;

    public void ShowMenu()
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.DOAnchorPos(_initialPos.anchoredPosition, 1).SetEase(Ease.InOutSine)
            .OnComplete(() => rect.DOAnchorPos(Vector2.zero, 0.2f).SetEase(Ease.InOutSine));
    }
}
