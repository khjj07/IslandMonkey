using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Assets._0_IslandMonkey.CHJ.Scripts.Upgrade;
using UniRx;
using Unity.VisualScripting;
using static MonkeyAnimationController;
using System;

public class MonkeyMovement : MonoBehaviour
{
    // 원숭이의 현재 위치
    public enum MonkeyLocation
    {
        InOwnBuilding,
        Moving,
        InTargetBuilding,
    }

    [SerializeField]
    public ReactiveProperty<MonkeyLocation> currentMonkeyLocation = new ReactiveProperty<MonkeyLocation>(MonkeyLocation.Moving);
    private List<Building> _functionalBuildings = new List<Building>(); // 거리가 짧은 순으로 정렬, 건물 이동시 다시 정렬
   
    public Building OwnBuilding; // 원숭이 instanciate될 때 함께 생성되는 건물을 할당해주어야 함.
    private Building TargetBuilding; 
    private NavMeshAgent AgentMonkey;
    private static float OffsetFromBuildingCenter = -3.5f; // building 중앙으로부터 3.5만큼 떨어진 곳에서 stop, 3.5f 바꾸면 안됨.(애니메이션과 관련)
    private static Vector3 OffsetFromBuildingCenterVector = new Vector3(0, 0, OffsetFromBuildingCenter); // 나중에 z 축으로 바꾸기

    /// <summary>
    /// 원숭이 health 임의 설정) 범위: 0~5, 1초에 1씩 체력감소 or 회복
    /// </summary>
    [SerializeField]
    private static float maxHealth = 7.0f;
    [SerializeField]
    private static float minHealth = 0.0f;
    [SerializeField]
    private static float healthUpdatePerSecond = 1.0f;
    [SerializeField]
    private float currentMonkeyHealth;

    private MonkeyAnimationController _monkeyAnimationController;

    private void Awake()
    {
        _functionalBuildings = BuildingManager.instance.FunctionalBuildings.ToList(); // FunctionalBuildings리스트 가져오기
        AgentMonkey = GetComponent<NavMeshAgent>();
        currentMonkeyHealth = maxHealth; // 초기 health는 max
        _monkeyAnimationController = GetComponent<MonkeyAnimationController>();
    }

    private void Start()
    {
        SortBuildings(); // FunctionalBuilding을 현재 원숭이 위치와 가까운 거리순으로 정렬

        // 원숭이가 자신 건물 중앙으로부터 z축방향 offset만큼 떨어진 곳에 생성
        transform.position = OwnBuilding.transform.position + OffsetFromBuildingCenterVector;

        // currentMonkeyLocation 변경될 때만 
        currentMonkeyLocation
            .DistinctUntilChanged();

        // 1초마다 health update
        Observable.Interval(TimeSpan.FromSeconds(1))
                        .Subscribe(_ => HealthUpdate())
                        .AddTo(this);
    }

    private void Update()
    {
        SetMonkeyLocation();
    }

    private void HealthUpdate()
    {
        switch (currentMonkeyLocation.Value)
        {
            case (MonkeyLocation.InOwnBuilding):
                currentMonkeyHealth -= healthUpdatePerSecond;
                break;
            case (MonkeyLocation.InTargetBuilding):
                currentMonkeyHealth += healthUpdatePerSecond;
                break;
        }
    }

    // targetBuilding에 원숭이가 있는지 체크 => 현재 원숭이가 내부에 없는 건물 중에 가장 가까운 건물을 target으로 설정
    private bool SetTargetBuilding()
    {
        foreach (Building functionalBuilding in _functionalBuildings)
        {
            if (!functionalBuilding.IsInMonkey)
            {
                TargetBuilding = functionalBuilding;
                Debug.Log("비어있는 빌딩을 찾았습니다.");
                return true;
            }
        }
        Debug.Log("비어있는 빌딩이 없습니다.");
        return false;
    }


    private void changeBuildingsIsInMonkey()
    {
        OwnBuilding.changeIsInMonkey();
        TargetBuilding.changeIsInMonkey();
    }

    // 원숭이 체력충전하러 TargetBuilding으로 이동
    private void GoToTargetBuilding()
    {
        // Target Building Entracne 찾기 => 비어있는 건물이 없을 경우 비어있는 빌딩이 생길때까지 계속 찾음
        bool isThereEmptyBuilding = SetTargetBuilding();
        if (isThereEmptyBuilding)
        {
            if (currentMonkeyLocation.Value != MonkeyLocation.Moving)
            {
                currentMonkeyLocation.Value = MonkeyLocation.Moving;
                changeBuildingsIsInMonkey();
                AgentMonkey.SetDestination(TargetBuilding.transform.position + OffsetFromBuildingCenterVector);
                //_monkeyAnimationController.MonkeyInBuildingAnimatorController = TargetBuilding.MonkeyWithBuildingAnimatorController;
            }
        }
    }

    // 원숭이 OwnBuilding으로 이동
    public void ComeBackHome()
    {
        if (currentMonkeyLocation.Value != MonkeyLocation.Moving)
        {
            currentMonkeyLocation.Value = MonkeyLocation.Moving;
            changeBuildingsIsInMonkey();
            AgentMonkey.SetDestination(OwnBuilding.transform.position + OffsetFromBuildingCenterVector);
            //_monkeyAnimationController.MonkeyInBuildingAnimatorController = OwnBuilding.MonkeyWithBuildingAnimatorController;
        }
    }

    private void SetMonkeyLocation()
    {
        switch (currentMonkeyLocation.Value)
        {
            case (MonkeyLocation.InOwnBuilding):
                if (currentMonkeyHealth <= minHealth)
                {
                    Debug.Log("집을 떠나자 숭숭");
                    _monkeyAnimationController.ChangeMonkeyState(MonkeyState.Walk);
                    GoToTargetBuilding();
                }
                break;
            case (MonkeyLocation.Moving):
                if (AgentMonkey.remainingDistance <= AgentMonkey.stoppingDistance) // 목적지까지 도달했을때
                {
                    if (currentMonkeyHealth >= maxHealth)  // OwnBuilding에 도달했을 경우
                    {
                        currentMonkeyLocation.Value = MonkeyLocation.InOwnBuilding;
                        _monkeyAnimationController.ChangeMonkeyState(MonkeyState.Stand);
                    }
                    else if (currentMonkeyHealth <= minHealth)  // TargetBuilding에 도달했을 경우
                    {
                        currentMonkeyLocation.Value = MonkeyLocation.InTargetBuilding;
                        _monkeyAnimationController.ChangeMonkeyState(MonkeyState.Stand);
                    }
                }
                break;
            case (MonkeyLocation.InTargetBuilding):
                if (currentMonkeyHealth >= maxHealth)
                {
                    Debug.Log("집으로 가자 숭숭");
                    _monkeyAnimationController.ChangeMonkeyState(MonkeyState.Walk);
                    ComeBackHome();
                }
                break;
        }
    }

    // 원숭이로부터 거리가 가까운 순으로 BuildingEntrances 정렬
    private void SortBuildings()
    {
        _functionalBuildings.Sort((a, b) =>
        {
            float distanceToA = Vector3.Distance(OwnBuilding.transform.position + OffsetFromBuildingCenterVector, a.transform.position);
            float distanceToB = Vector3.Distance(OwnBuilding.transform.position + OffsetFromBuildingCenterVector, b.transform.position);
            return distanceToA.CompareTo(distanceToB);
        });
        TargetBuilding = _functionalBuildings[0]; // 최초 targetBuilding는 가장 가까운 Building
    }
}
