using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField] private Transform _startToMove;
    [SerializeField] private Transform _endToMove;
    [SerializeField] private int _animationDuration;
    [SerializeField] private int _minInterval;
    [SerializeField] private int _maxInterval;

    public IEnumerator StartCroassingTheRoad()
    {
        yield return new WaitForSeconds(Random.Range(_minInterval, _maxInterval));
        transform.DOMove(_endToMove.position, _animationDuration).From(_startToMove.position).SetEase(Ease.Linear);

    }
}
