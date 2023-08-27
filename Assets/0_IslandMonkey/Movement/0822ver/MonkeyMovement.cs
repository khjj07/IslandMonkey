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
    public ReactiveProperty<MonkeyLocation> currentMonkeyLocation = new ReactiveProperty<MonkeyLocation>(MonkeyLocation.Moving);
    private List<Building> _functionalBuildings = new List<Building>(); // �Ÿ��� ª�� ������ ����, �ǹ� �̵��� �ٽ� ����
   
    public Building OwnBuilding; // ������ instanciate�� �� �Բ� �����Ǵ� �ǹ��� �Ҵ����־�� ��.
    private Building TargetBuilding; 
    private NavMeshAgent AgentMonkey;
    private static float OffsetFromBuildingCenter = -3.5f; // building �߾����κ��� 3.5��ŭ ������ ������ stop, 3.5f �ٲٸ� �ȵ�.(�ִϸ��̼ǰ� ����)
    private static Vector3 OffsetFromBuildingCenterVector = new Vector3(0, 0, OffsetFromBuildingCenter); // ���߿� z ������ �ٲٱ�

    /// <summary>
    /// ������ health ���� ����) ����: 0~5, 1�ʿ� 1�� ü�°��� or ȸ��
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
        _functionalBuildings = BuildingManager.instance.FunctionalBuildings.ToList(); // FunctionalBuildings����Ʈ ��������
        AgentMonkey = GetComponent<NavMeshAgent>();
        currentMonkeyHealth = maxHealth; // �ʱ� health�� max
        _monkeyAnimationController = GetComponent<MonkeyAnimationController>();
    }

    private void Start()
    {
        SortBuildings(); // FunctionalBuilding�� ���� ������ ��ġ�� ����� �Ÿ������� ����

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
        foreach (Building functionalBuilding in _functionalBuildings)
        {
            if (!functionalBuilding.IsInMonkey)
            {
                TargetBuilding = functionalBuilding;
                Debug.Log("����ִ� ������ ã�ҽ��ϴ�.");
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

    // ������ OwnBuilding���� �̵�
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
                    Debug.Log("���� ������ ����");
                    _monkeyAnimationController.ChangeMonkeyState(MonkeyState.Walk);
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
                    Debug.Log("������ ���� ����");
                    _monkeyAnimationController.ChangeMonkeyState(MonkeyState.Walk);
                    ComeBackHome();
                }
                break;
        }
    }

    // �����̷κ��� �Ÿ��� ����� ������ BuildingEntrances ����
    private void SortBuildings()
    {
        _functionalBuildings.Sort((a, b) =>
        {
            float distanceToA = Vector3.Distance(OwnBuilding.transform.position + OffsetFromBuildingCenterVector, a.transform.position);
            float distanceToB = Vector3.Distance(OwnBuilding.transform.position + OffsetFromBuildingCenterVector, b.transform.position);
            return distanceToA.CompareTo(distanceToB);
        });
        TargetBuilding = _functionalBuildings[0]; // ���� targetBuilding�� ���� ����� Building
    }
}
