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
        Vector3 direction = _finishPoint.position - _startPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        objectToMove.rotation = Quaternion.LookRotation(direction);

    }
}
