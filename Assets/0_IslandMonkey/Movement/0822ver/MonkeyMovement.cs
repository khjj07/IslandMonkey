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
    // �������� ���� ��ġ
    public enum MonkeyLocation
    {
        InOwnBuilding,
        Moving,
        InTargetBuilding,
    }

    [SerializeField]
    public ReactiveProperty<MonkeyLocation> currentMonkeyLocation;
    [SerializeField]
    private List<Building> _functionalBuildings; // �Ÿ��� ª�� ������ ����, �ǹ� �̵��� �ٽ� ����
   
    public Building OwnBuilding; // ������ instanciate�� �� �Բ� �����Ǵ� �ǹ��� �Ҵ����־�� ��.
    [SerializeField]
    private Building TargetBuilding; 
    private NavMeshAgent AgentMonkey;
    private static float OffsetFromBuildingCenter = -2.5f; // building �߾����κ��� 3.5��ŭ ������ ������ stop, 3.5f �ٲٸ� �ȵ�.(�ִϸ��̼ǰ� ����)
    private static Vector3 OffsetFromBuildingCenterVector; // ���߿� z ������ �ٲٱ�

    /// <summary>
    /// ������ health ���� ����) ����: 0~5, 1�ʿ� 1�� ü�°��� or ȸ��
    /// </summary>
    [SerializeField]
    private static float maxHealth = 5.0f;
    [SerializeField]
    private static float minHealth = 0.0f;
    [SerializeField]
    private static float healthUpdatePerSecond = 1.0f;
    [SerializeField]
    private float currentMonkeyHealth;

    private MonkeyAnimationController _monkeyAnimationController;

    private void Awake()
    {
        currentMonkeyLocation = new ReactiveProperty<MonkeyLocation>(MonkeyLocation.Moving);
        OffsetFromBuildingCenterVector = new Vector3(0, 0, OffsetFromBuildingCenter);

        AgentMonkey = GetComponent<NavMeshAgent>();
        currentMonkeyHealth = maxHealth; // �ʱ� health�� max
        _monkeyAnimationController = GetComponent<MonkeyAnimationController>();

        _functionalBuildings = new List<Building> { };

    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        FindclosestBuilding();

        // �����̰� �ڽ� �ǹ� �߾����κ��� z����� offset��ŭ ������ ���� ����
        transform.position = OwnBuilding.transform.position + OffsetFromBuildingCenterVector;

        // currentMonkeyLocation ����� ���� 
        currentMonkeyLocation
            .DistinctUntilChanged();

        // 1�ʸ��� health update
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

    // targetBuilding�� �����̰� �ִ��� üũ => ���� �����̰� ���ο� ���� �ǹ� �߿� ���� ����� �ǹ��� target���� ����
    private bool SetTargetBuilding()
    {
        SortBuildings();
        foreach (Building functionalBuilding in _functionalBuildings)
        {
            if (!functionalBuilding.IsInMonkey)
            {
                TargetBuilding = functionalBuilding;
                //Debug.Log("����ִ� ������ ã�ҽ��ϴ�.");
                return true;
            }
        }
        Debug.Log("����ִ� ������ �����ϴ�.");
        return false;
    }


    private void changeBuildingsIsInMonkey()
    {
        OwnBuilding.changeIsInMonkey();
        TargetBuilding.changeIsInMonkey();
    }

    // ������ ü�������Ϸ� TargetBuilding���� �̵�
    private void GoToTargetBuilding()
    {
        // Target Building Entracne ã�� => ����ִ� �ǹ��� ���� ��� ����ִ� ������ ���涧���� ��� ã��
        bool isThereEmptyBuilding = FindclosestBuilding();
        if (isThereEmptyBuilding)
        {
            if (currentMonkeyLocation.Value != MonkeyLocation.Moving)
            {
                currentMonkeyLocation.Value = MonkeyLocation.Moving;
                changeBuildingsIsInMonkey();
                _monkeyAnimationController.ChangeMonkeyState(MonkeyState.Walk);

                AgentMonkey.SetDestination(TargetBuilding.transform.position + OffsetFromBuildingCenterVector);
                //_monkeyAnimationController.MonkeyInBuildingAnimatorController = TargetBuilding.MonkeyWithBuildingAnimatorController;
            }
        }
    }

    // ������ OwnBuilding���� �̵�
    public void ComeBackHome()
    {
        if (currentMonkeyLocation.Value != MonkeyLocation.Moving)
        {
            currentMonkeyLocation.Value = MonkeyLocation.Moving;
            changeBuildingsIsInMonkey();
            _monkeyAnimationController.ChangeMonkeyState(MonkeyState.Walk);

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
                    //Debug.Log("���� ������ ����");
                    //_monkeyAnimationController.ChangeMonkeyState(MonkeyState.Walk);
                    GoToTargetBuilding();
                }
                break;
            case (MonkeyLocation.Moving):
                if (AgentMonkey.remainingDistance <= AgentMonkey.stoppingDistance) // ���������� ����������
                {
                    if (currentMonkeyHealth >= maxHealth)  // OwnBuilding�� �������� ���
                    {
                        currentMonkeyLocation.Value = MonkeyLocation.InOwnBuilding;
                        _monkeyAnimationController.ChangeMonkeyState(MonkeyState.Stand);
                    }
                    else if (currentMonkeyHealth <= minHealth)  // TargetBuilding�� �������� ���
                    {
                        currentMonkeyLocation.Value = MonkeyLocation.InTargetBuilding;
                        _monkeyAnimationController.ChangeMonkeyState(MonkeyState.Stand);
                    }
                }
                break;
            case (MonkeyLocation.InTargetBuilding):
                if (currentMonkeyHealth >= maxHealth)
                {
                    //Debug.Log("������ ���� ����");
                    //_monkeyAnimationController.ChangeMonkeyState(MonkeyState.Walk);
                    ComeBackHome();
                }
                break;
        }
    }

    // �����̷κ��� �Ÿ��� ����� ������ BuildingEntrances ����
    private void SortBuildings()
    {
        // ����Ʈ�� �� ��Ҹ� ���� �����Ͽ� ���ο� ����Ʈ ����
        _functionalBuildings = BuildingManager.instance.FunctionalBuildings.Select(_ => new Building()).ToList();


        _functionalBuildings.Sort((a, b) =>
        {
            float distanceToA = Vector3.Distance(OwnBuilding.transform.position + OffsetFromBuildingCenterVector, a.transform.position);
            float distanceToB = Vector3.Distance(OwnBuilding.transform.position + OffsetFromBuildingCenterVector, b.transform.position);
            return distanceToA.CompareTo(distanceToB);
        });
        TargetBuilding = _functionalBuildings[0]; // ���� targetBuilding�� ���� ����� Building
    }

    private bool FindclosestBuilding()
    {
        float minDistance = Mathf.Infinity;
        Building closestBuilding = null;
        Vector3 monkeyCurrentPosition = transform.position; // ���� ������Ʈ�� ��ġ

        if (BuildingManager.instance.FunctionalBuildings != null)
        {
            foreach (Building  _building in BuildingManager.instance.FunctionalBuildings)
            {
                if (_building == null) continue;
                if (_building.IsInMonkey == false)
                {
                    Vector3 buildingPosition = _building.transform.position;
                    float distance = Vector3.Distance(monkeyCurrentPosition, buildingPosition);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestBuilding = new Building();
                        closestBuilding = _building;
                    }
                }
            }

            if (closestBuilding != null)
            {
                TargetBuilding = closestBuilding;
                Debug.Log("����ִ� ���� ã�ҽ��ϴ�.");
                return true;
            }
            Debug.Log("����ִ� ������ �����ϴ�.");
        }
        return false;
    }
}
