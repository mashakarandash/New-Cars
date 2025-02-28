using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayButton : MonoBehaviour
{
    [SerializeField] public Transform _transformCar;

    public void JumpCar()
    {
        _transformCar.DOLocalMoveY(_transformCar.localPosition.y + 15, 0.2f).SetLoops(2, LoopType.Yoyo);
    }
}
