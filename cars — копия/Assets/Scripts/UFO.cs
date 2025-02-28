using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UFO : MonoBehaviour
{
    [SerializeField] private float _speedOfStiil;
    [SerializeField] private Transform _positionForStill;
    [SerializeField] private int _timeToApperance;
    [SerializeField] private int _timeToStill;
    [SerializeField] private int _timeToFlyForStill;

    private bool _canStill;
    private Tween _ufoMovingAnimation;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToApperance);
            _ufoMovingAnimation = transform.DOMove(_positionForStill.position, _timeToFlyForStill).SetEase(Ease.Linear).SetAutoKill(false);
            yield return _ufoMovingAnimation.WaitForCompletion();
            _canStill = true;
            yield return new WaitForSeconds(_timeToStill);
            _canStill = false;
            _ufoMovingAnimation.PlayBackwards();
            yield return _ufoMovingAnimation.WaitForRewind();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canStill == false)
        {
            return;
        }

        if (other.TryGetComponent(out Vehicle carToStill))
        {
            carToStill.NavMeshAgent.isStopped = true;
            carToStill.IsStilledByUFO = true;
            carToStill.transform.DOMove(transform.position, _speedOfStiil).SetEase(Ease.Linear);
            carToStill.transform.DOScale(0, _speedOfStiil).SetEase(Ease.Linear).OnComplete(()=>
            {
                carToStill.gameObject.SetActive(false);
                carToStill.transform.DOScale(1, 0);
                carToStill.IsStilledByUFO = false;
            });
        }
    }
}
