using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _listObstacle;
    [SerializeField] private ObstacleMove _obstacleMove;
    [SerializeField, Range(0, 30)] private int _timeToMoveObstacle;
    [SerializeField, Range(5, 30)] private int _timeToRundomSpawner1;
    [SerializeField, Range(5, 30)] private int _timeToRundomSpawner2;
    [SerializeField] private ObstacleHandler _grandma;

    private CustomPool<ObstacleHandler> _grandmaPool;

    private void Start()
    {
        _grandmaPool = new CustomPool<ObstacleHandler>(_grandma, 2);
        StartCoroutine(GenerateObstacleCoroutine());
    }

    private IEnumerator GenerateObstacleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_timeToRundomSpawner1, _timeToRundomSpawner2));
            GameObject CopyOfObstacle = _grandmaPool.GetCar().gameObject;
            _obstacleMove.StartMoveObctacle(CopyOfObstacle.transform, _timeToMoveObstacle);
        }

       
    }

}
