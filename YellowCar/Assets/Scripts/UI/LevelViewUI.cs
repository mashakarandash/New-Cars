using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelViewUI : MonoBehaviour
{
    [SerializeField] private Sprite _1Start;
    [SerializeField] private Sprite _2Start;
    [SerializeField] private Sprite _3Start;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public int LevelNumber;

    public void Initialize(int ganeStars, bool isUnlocked)
    {
        switch(ganeStars)
        {
            case 1:
                _image.sprite = _1Start;
                break;
            case 2:
                _image.sprite = _2Start;
                break;
            case 3:
                _image.sprite = _3Start;
                break;
        }
        if (isUnlocked == true)
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }
    }
}
