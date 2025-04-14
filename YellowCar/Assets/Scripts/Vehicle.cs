using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public abstract class Vehicle : MonoBehaviour
{
    public int Speed => _speed;

    public int SaveSpeed;

    public int Index;
    public bool GreenLightDetected = true;
    public bool IsStilledByUFO;
    public bool CanPlayTapAudio = true;

    private int _speed = 2;
    private bool _needOvertake;
    private Color _defaultColor;

    protected bool IsTemporaryYellowCar;
    protected AudioSource AudioEffects;
    

    [SerializeField] public NavMeshAgent NavMeshAgent;

    [SerializeField] private float _distanceBetweenCar;
    [SerializeField] private ParticleSystem _collisionParticle;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] protected AudioClip _yellowCarTapAudio;
    [SerializeField] protected AudioClip _anotherCarTapAudio;

    private TraficLight _traficLight;

    public virtual void OnMouseDown()
    {
        if(CanPlayTapAudio)
        {
            if (IsTemporaryYellowCar)
            {
                PlayTapAudio(_yellowCarTapAudio);
            }
            else
            {
                PlayTapAudio(_anotherCarTapAudio);
            }
        }
    }
    // встроенный метод юнити для унаследованный классов от Monobehavior, вызывается при нажатии на объект с колайдером на котором висит данный скрипn

    public void SetSpeed(int speed) => _speed = speed; // установление скорости
        
    

   /* protected void OnCollisionEnter(Collision collision) // метод обработки физики столкновения, работает только тогда, когда на двух объектах с которыми произошло столкновение есть компонент коллайдер и RigidBody. но у обоих компонентов должен быть коллайдер.
    {
        
        if (collision.gameObject.layer == 6)// layer - спец. часть движка юнити, отвечающая за слои столкновения и взаимодействия слоев. мы можем в настройках отключить взаимодействие некоторых слоев друг с другом, использовать как параметр для сортировки.
        {
            collision.gameObject.TryGetComponent<Vehicle>(out Vehicle transport); //TryGetComponent - это специальный  метод класса MonoBehavior, который действует так, в <классс который хотим получить>(out компонент который возвращаем для дальнейшей работы, название)
            transport?.SetSpeed(_speed);
        }
    }*/

    [Inject]
    public void Constract([Inject(Id = "Effects")]AudioSource audioEffects)
    {
        AudioEffects = audioEffects;
    }

    protected void PlayTapAudio(AudioClip tapClip)
    {
        AudioEffects.PlayOneShot(tapClip);
    }

    public virtual void FixedUpdate() 
    {
       // Mooving();
    }

    public void SetDestinationToPoint(Vector3 pointCoordinate, bool needOvertake)
    {
        NavMeshAgent.SetDestination(pointCoordinate);
        NavMeshAgent.speed = _speed;
       
        _needOvertake = needOvertake;
       if (needOvertake == false)
       {
           
       }

    }

    public void TeleportTonewPosition(Vector3 position)
    {
        NavMeshAgent.Warp(position);
        Debug.Log(NavMeshAgent.isOnNavMesh);
    }

    public virtual void DoDestroy()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator SearchObstacleCoroutine()
    {
        if (_needOvertake == false)
        {

            while (true)
            {
                Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward * 3.5f));

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * 3.5f), Color.red);

                if (Physics.Raycast(ray, out RaycastHit hitinfo, 3.5f, 1 << 6) && GreenLightDetected == false) // 01010011 00000001 0000010 0000100
                {
                    Debug.Log(hitinfo.collider.gameObject.name);
                    NavMeshAgent.speed = 0;
                }
                else if (Physics.Raycast(ray, out RaycastHit hitinfo2, 3.5f, 1 << 6) && GreenLightDetected == true)
                {
                    hitinfo2.collider.TryGetComponent(out NavMeshAgent carAgent);
                    NavMeshAgent.speed = carAgent.speed;
                }
                yield return new WaitForSeconds(0.1f);

            }
        }
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(SearchObstacleCoroutine());
    }

    internal void Initialiaze(TraficLight traficLight)
    {
        _traficLight = traficLight;
        _traficLight.GreenLightEvent += TimeToStopActions;
    }

    private void TimeToStopActions(bool isGreenLightOn)
    {
        if (!isGreenLightOn)
        {

            NavMeshAgent.autoTraverseOffMeshLink = false;
            NavMeshAgent.avoidancePriority = 0;
            NavMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        }

        else 
        {

            NavMeshAgent.autoTraverseOffMeshLink = true;
            NavMeshAgent.avoidancePriority = 3;
            NavMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out ObstacleHandler grandma)) 
        {
            _collisionParticle.Play();

        }
    }

    public void ConvertCarInToYellowCar()
    {
        IsTemporaryYellowCar = true;
        
        _defaultColor = _meshRenderer.material.color;
        _meshRenderer.material.color = Color.yellow;
    }

    public void ConvertCarInToDefault()
    {
        IsTemporaryYellowCar = false;
        
        _meshRenderer.material.color = _defaultColor;
    }
}
