using Assets._0_IslandMonkey.Scripts.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monkey : MonoBehaviour
{
    // 원숭이의 현재 위치
    public enum State
    {
        Entering,
        InBuilding,
        Exiting,
        Moving,
    }

    [SerializeField] private ReactiveProperty<State> _currentState;

    public Building_PJH ownBuilding; // 원숭이 instanciate될 때 함께 생성되는 건물을 할당해주어야 함.
    private Building_PJH _currentBuilding;
    public Building_PJH _targetBuilding;
    private NavMeshAgent _agentMonkey;
    [SerializeField] private Animator _animator;

    const float MAX_COUNT = 8.0f;
    const float UPDATE_PER_SECOND_COUNT = 1.0f;
    [SerializeField] private float _currentCount;

    private void Awake()
    {
        _currentState = new ReactiveProperty<State>(State.Entering); // 초기상태
        _agentMonkey = GetComponent<NavMeshAgent>();
        _currentCount = MAX_COUNT; // 초기 health는 max
        //ownBuilding.isOccupied = true;
        //ownBuilding.isInMonkey = true;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        // 원숭이 초기위치 및 방향 설정
        transform.position = ownBuilding.transform.position;
        transform.rotation = ownBuilding.entrance.rotation;

        _currentBuilding = ownBuilding;

        // 1초마다 health update
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ => HealthUpdate())
            .AddTo(this);

        //_currentState
        //    .DistinctUntilChanged()
        //    .Subscribe(_ => ChangeState())
        //    .AddTo(gameObject);

    }

    private void Update()
    {
        ChangeState();
        CheckState();
    }

    private void HealthUpdate()
    {
        switch (_currentState.Value)
        {
            case (State.Entering):
                _currentCount = MAX_COUNT;
                break;
            case (State.InBuilding):
                _currentCount -= UPDATE_PER_SECOND_COUNT;
                break;
        }
    }

    private void ChangeState()
    {
        switch (_currentState.Value)
        {
            case (State.Entering):
                Debug.Log("Entering");
                _currentState.Value = State.InBuilding;
                _currentCount = MAX_COUNT;
                _animator.SetTrigger("Cafe"); 
                _animator.SetBool("inBuilding", true);
                break;
            case (State.InBuilding):
                if(_currentCount <= 0)
                {
                    Debug.Log("InBuilding");
                    _currentState.Value = State.Exiting;
                }
                break;
            case (State.Exiting):
                Debug.Log("Exiting");   
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    _animator.SetBool("inBuilding", false);
                    _currentState.Value = State.Moving;
                    
                }
                
                _animator.SetTrigger("Exit");

                break;
            case (State.Moving):
                if (Vector3.Distance(this.transform.position, _targetBuilding.entrance.transform.position) <= 0.1)
                {
                    Debug.Log("Moving");
                    _currentState.Value = State.Entering;
                }
                break;
        }
    }

    private void CheckState()
    {
        switch (_currentState.Value)
        {
            case (State.Entering):
                _agentMonkey.updatePosition = false;
                _agentMonkey.updateRotation = false;
                _currentBuilding = _targetBuilding;
                this.transform.position = _currentBuilding.transform.position;
                

                break;
            case (State.InBuilding):
                break;
            case (State.Exiting):
                _agentMonkey.updatePosition = true;
                _agentMonkey.updateRotation = true;
                
                break;
            case (State.Moving):
                Move();
                break;


        }
    }

    private void Move()
    {
        // 기능 시설로 이동
        if (_currentBuilding == ownBuilding)
        {
            GoToTargetBuilding();
        }
        // ownbuilding으로 이동
        else
        {
            GoToOwnBuilding();
        }
    }

    private void GoToOwnBuilding()
    {
        _agentMonkey.SetDestination(ownBuilding.entrance.transform.position);
        _targetBuilding = ownBuilding;
    }

    // 원숭이 체력충전하러 targetBuilding으로 이동
    private void GoToTargetBuilding()
    {
        // targetBuilding 찾기 => 비어있는 건물이 없을 경우 비어있는 빌딩이 생길때까지 계속 찾음
        bool isThereEmptyBuilding = FindClosestBuilding();
        if (isThereEmptyBuilding)
        {
            _agentMonkey.SetDestination(_targetBuilding.entrance.transform.position);
        }
    }

    private bool FindClosestBuilding()
    {
        float minDistance = Mathf.Infinity;
        Building_PJH closestBuilding = null;
        Vector3 monkeyCurrentPosition = transform.position; // 현재 Monkey 위치

        if (BuildingManager_PJH.instance.functionalBuildings != null)
        {
            foreach (Building_PJH _building in BuildingManager_PJH.instance.functionalBuildings)
            {
                if (_building == null) break;
                if (_building.isOccupied == false)
                {
                    Vector3 buildingPosition = _building.transform.position;
                    float distance = Vector3.Distance(monkeyCurrentPosition, buildingPosition);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestBuilding = _building;
                    }
                }
            }

            if (closestBuilding != null)
            {
                _targetBuilding = closestBuilding;
                //Debug.Log("비어있는 빌딩 찾았습니다.");
                return true;
            }
            Debug.Log("비어있는 빌딩이 없습니다.");
        }
        return false;
    }
}

