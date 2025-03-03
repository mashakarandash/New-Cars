using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleMove : MonoBehaviour
{ 
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _finishPoint;

    public void StartMoveObctacle(Transform objectToMove, int timeToFinishMovement)
    {
        objectToMove.position = _startPoint.position;
        objectToMove.DOMove(_finishPoint.position, timeToFinishMovement).OnComplete(() => objectToMove.gameObject.SetActive(false));
    }

}
