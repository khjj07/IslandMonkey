using Assets._0_IslandMonkey.Scripts.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

public class Monkey : MonoBehaviour
{
    // �������� ���� ��ġ
    public enum State
    {
        InOwnBuilding,
        Moving,
        InTargetBuilding,
    }

    [SerializeField]
    private ReactiveProperty<State> _currentState;

    public Building_PJH ownBuilding; // ������ instanciate�� �� �Բ� �����Ǵ� �ǹ��� �Ҵ����־�� ��.
    private Building_PJH _targetBuilding;
    public Building_PJH _currentBuilding;
    private NavMeshAgent _agentMonkey;

    /// <summary>
    /// ������ health ���� ����) ����: 0~8, 1�ʿ� 1�� ü�°��� or ȸ��
    /// </summary>
    const float MAX_HEALTH = 8.0f;
    const float MIN_HEALTH = 0.0f;
    const float UPDATE_PER_SECOND_HEALTH = 1.0f;
    [SerializeField]
    private float _currentHealth;

    private Animator _animator;

    public float remain_dist;

    private void Awake()
    {
        _currentState = new ReactiveProperty<State>(State.InOwnBuilding); // �ʱ���� Moving
        _agentMonkey = GetComponent<NavMeshAgent>();
        _currentHealth = MAX_HEALTH; // �ʱ� health�� max
        ownBuilding.isOccupied = true;
        ownBuilding.isInMonkey = true;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        // ������ �ʱ���ġ �� ���� ����
        transform.position = ownBuilding.transform.position;
        transform.rotation = ownBuilding.entrance.rotation;

        // 1�ʸ��� health update
        Observable.Interval(TimeSpan.FromSeconds(1))
                        .Subscribe(_ => HealthUpdate())
                        .AddTo(this);

        _currentState
            .DistinctUntilChanged()
            .Subscribe(newState => HandleAnimation(newState))
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => _currentState.Value == State.InOwnBuilding)
            .Subscribe(_ =>
            {
                _currentBuilding = ownBuilding;
                if (_currentHealth <= MIN_HEALTH)
                {
                    GoToTargetBuilding();
                }
            });

        this.UpdateAsObservable()
            .Where(_ => _currentState.Value == State.Moving)
            .Subscribe(_ =>
            {
                if (_currentHealth >= MAX_HEALTH)
                {
                    if (_agentMonkey.remainingDistance <= _agentMonkey.stoppingDistance) // OwnBuilding�� �������� ���
                    {
                        InOwnBuilding();
                    }
                }
                else if (_currentHealth <= MIN_HEALTH)
                {
                    if (_agentMonkey.remainingDistance <= _agentMonkey.stoppingDistance) // TargetBuilding�� �������� ���
                    {
                        InTargetBuilding();

                    }
                }
            });

        this.UpdateAsObservable()
            .Where(_ => _currentState.Value == State.InTargetBuilding)
            .Subscribe(_ =>
            {
                _currentBuilding = _targetBuilding;
                if (_currentHealth >= MAX_HEALTH)
                {
                    ComeBackHome();
                }
            });

    }
    private void Update()
    { 
        remain_dist = _agentMonkey.remainingDistance;
    }

    private void HealthUpdate()
    {
        switch (_currentState.Value)
        {
            case (State.InOwnBuilding):
                _currentHealth -= UPDATE_PER_SECOND_HEALTH;
                break;
            case (State.InTargetBuilding):
                _currentHealth += UPDATE_PER_SECOND_HEALTH;
                break;
        }
    }

    private void ChangePosition(Building_PJH building)
    {
        if (this.transform.position == building.transform.position)
        {
            this.transform.position = building.entrance.position;
        }
        else
        {
            this.transform.position = building.transform.position;
        }
    }

    private void ChangePosition()
    {
        if (this.transform.position == ownBuilding.transform.position)
        {
            ChangePosition(ownBuilding);
        }
        else
        {
            ChangePosition(_targetBuilding);
        }
    }

    private void HandleAnimation(State newState)
    {
        switch (newState)
        {
            case State.Moving:
                //ChangePosition();
                _animator.SetTrigger("OutBuilding");
                _animator.SetBool("building", false);
                break;
            case State.InOwnBuilding:
                //ChangePosition();
                _animator.SetBool("building", true);
                _animator.SetTrigger("InBuilding");
                break;
            case State.InTargetBuilding:
                //ChangePosition();
                //_animator.SetTrigger("InBuilding");
                //_animator.SetBool("building", true);
                break;
        }
    }

    private void ChangeBuildingsIsOccupied()
    {
        ownBuilding.ChangeIsOccupied();
        _targetBuilding.ChangeIsOccupied();
    }

    private void Move(Transform targetTramsform)
    {
        _agentMonkey.SetDestination(targetTramsform.position);
    }

    private void SetRotation(Transform targetTramsform)
    {
        this.transform.rotation = targetTramsform.rotation;
    }

    // ������ ü�������Ϸ� targetBuilding���� �̵�
    private void GoToTargetBuilding()
    {
        // targetBuilding ã�� => ����ִ� �ǹ��� ���� ��� ����ִ� ������ ���涧���� ��� ã��
        bool isThereEmptyBuilding = FindClosestBuilding();
        if (isThereEmptyBuilding)
        {
            if (_currentState.Value != State.Moving)
            {
                _currentState.Value = State.Moving;
                ChangeBuildingsIsOccupied();
                ownBuilding.ChangeIsInMonkey();
                Move(_targetBuilding.entrance);
            }
        }
    }

    // ������ ownBuilding���� �̵�
    public void ComeBackHome()
    {
        if (_currentState.Value != State.Moving)
        {
            _currentState.Value = State.Moving;
            ChangeBuildingsIsOccupied();
            _targetBuilding.ChangeIsInMonkey();
            Move(ownBuilding.entrance);
        }
    }

    public void InOwnBuilding()
    {
        SetRotation(ownBuilding.entrance.transform);
        ownBuilding.ChangeIsInMonkey();
        _currentState.Value = State.InOwnBuilding;
    }

    public void InTargetBuilding()
    {
        SetRotation(_targetBuilding.entrance.transform);
        _targetBuilding.ChangeIsInMonkey();
        _currentState.Value = State.InTargetBuilding;
    }

    private bool FindClosestBuilding()
    {
        float minDistance = Mathf.Infinity;
        Building_PJH closestBuilding = null;
        Vector3 monkeyCurrentPosition = transform.position; // ���� Monkey ��ġ

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
                        closestBuilding = new Building_PJH();
                        closestBuilding = _building;
                    }
                }
            }

            if (closestBuilding != null)
            {
                _targetBuilding = closestBuilding;
                Debug.Log("����ִ� ���� ã�ҽ��ϴ�.");
                return true;
            }
            Debug.Log("����ִ� ������ �����ϴ�.");
        }
        return false;
    }
}

