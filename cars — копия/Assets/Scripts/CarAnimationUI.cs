using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarAnimationUI : MonoBehaviour
{
    [SerializeField] private float _speedAnimation;
    
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360), _speedAnimation, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

}
