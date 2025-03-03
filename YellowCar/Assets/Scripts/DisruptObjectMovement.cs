using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class DisruptObjectMovement : MonoBehaviour
{
  
    [SerializeField] private int _intervalToMoveMin;
    [SerializeField] private int _intervalToMoveMax;
    [SerializeField] private List<DisruptObjectSetting> _disruptObjects;
    [SerializeField] private int _cloudAnimationDuration;

    void Start()
    {
        StartCoroutine(StartMovementCloudCoroutine());
    }

   private IEnumerator StartMovementCloudCoroutine()
    {
        while (true)
        {

            yield return new WaitForSeconds(UnityEngine.Random.Range(_intervalToMoveMin, _intervalToMoveMax));
            foreach (var cloud in _disruptObjects)
            {
                cloud.Cloud.position = cloud.PointStart.position;
                cloud.Cloud.DOMove(cloud.PointEnd.position, _cloudAnimationDuration).SetEase(Ease.Linear);
            }
        }
    }
}

[Serializable]

public struct DisruptObjectSetting
{
    public Transform PointStart;
    public Transform PointEnd;
    public Transform Cloud;
}