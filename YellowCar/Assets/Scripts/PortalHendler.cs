using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class PortalHendler : MonoBehaviour
{
    [SerializeField] private GameObject _exitPortal;
    [SerializeField] private GameObject _enterPortal;
    [SerializeField] private int _minActivationTime;
    [SerializeField] private int _maxActivationTime;
    [SerializeField] private int _minDurationTime;
    [SerializeField] private int _maxDurationTime;
    [SerializeField] private float _timeToVanish;
    [SerializeField] private float _maxSizePortal;

    private void Awake()
    {
        StartCoroutine(StartPortalActivation());
        _enterPortal.SetActive(false);
        _exitPortal.SetActive(false);
        _enterPortal.transform.localScale = Vector3.zero;
        _exitPortal.transform.localScale = Vector3.zero;
    }

    private IEnumerator StartPortalActivation()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minActivationTime, _maxActivationTime));
            _enterPortal.SetActive(true);
            _enterPortal.transform.DOScale(_maxSizePortal, _timeToVanish);
            _exitPortal.SetActive(true);
            _exitPortal.transform.DOScale(_maxSizePortal, _timeToVanish).OnComplete(()=>
            {
                var obstacle = _exitPortal.GetComponent<NavMeshObstacle>();
                obstacle.carving = false;
                obstacle.carving = true;
            });


            yield return new WaitForSeconds(Random.Range(_minDurationTime, _maxDurationTime));

            _enterPortal.transform.DOScale(0, _timeToVanish).OnComplete(() => _enterPortal.SetActive(false));
            _exitPortal.transform.DOScale(0, _timeToVanish).OnComplete(() => _exitPortal.SetActive(false));
        }

    }
}
