using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VoitingMan : MonoBehaviour
{
    [SerializeField] private ScriptableObjectPoolData _allCars;
    [SerializeField] private float _convertionDuration;
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _finish;
    [SerializeField] private int _waitingTime;
    [SerializeField] private int _minTimeToExist;
    [SerializeField] private int _maxTimeToExist;
    [SerializeField] private float _timeToWalk;
    [SerializeField] private bool _canInteract;
    

    private bool _isCarsConverted;

    private void Start()
    {
        StartCoroutine(StartToExis());
    }

    private void OnMouseDown()
    {
        if (_isCarsConverted == false && _canInteract == true)
        {
            StartCoroutine(ConvertAllCarsInToYellowCars());
            transform.DOScale(0, 0);
            _canInteract = false;
        }

    }

    private IEnumerator ConvertAllCarsInToYellowCars()
    {
        if (_isCarsConverted)
        {
            yield return null;
        }
        else
        {
            _isCarsConverted = true;
            _allCars.RefreshAllCarsList();
            foreach (var car in _allCars.AllCars)
            {
                car.ConvertCarInToYellowCar();
            }

            yield return new WaitForSeconds(_convertionDuration);
            _isCarsConverted = false;

            foreach (var car in _allCars.AllCars)
            {
                car.ConvertCarInToDefault();
            }
        }
    }

    private IEnumerator StartToExis()
    {
        while (true)
        {
            int randomNumber = UnityEngine.Random.Range(_minTimeToExist, _maxTimeToExist);
            yield return new WaitForSeconds(randomNumber);
            transform.DOScale(1, 0);
            Tween tween = transform.DOMove(_finish.position, _timeToWalk).From(_start.position);
            yield return tween.WaitForCompletion();
            _canInteract = true;
            yield return new WaitForSeconds(_waitingTime);

            if (transform.localScale != Vector3.zero)
            {
                _canInteract = false;
                transform.DOMove(_start.position, _timeToWalk).From(_finish.position).OnComplete(() => gameObject.SetActive(false));
            }
        }
    }
}
