using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using Assets.IslandMonkey.TestScripts.JHJ.Upgrade;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Building[] buildingPrefabs; // �پ��� ������ ���� �������� �迭�� ����

    [SerializeField]
    private Monkey[] monkeyPrefabs; // �پ��� ������ ������ �������� �迭�� ����

    [SerializeField]
    private Ground[] groundSlots; // ���� ������ �迭�� ����

    [SerializeField]
    private int _totalGold;
    [SerializeField]
    private TextMeshProUGUI totalGoldText; // TextMeshPro ������Ʈ�� �Ҵ���� ����

    private ReactiveCollection<Building> _buildings = new ReactiveCollection<Building>();
    private ReactiveCollection<Monkey> _monkeys = new ReactiveCollection<Monkey>();

    private void Start()
    {
        UpdateTotalGoldText();
    }

    public void BuildBuilding()
    {
        var buildingPrefab = buildingPrefabs[0]; // ������ �ε����� ������ ����
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();

        var availableGroundSlots = groundSlots.Where(slot => !slot.IsOccupied.Value).ToList();
        if (availableGroundSlots.Count > 0)
        {
            var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position + new Vector3(0f, 0.5f, 0f); // Ground�� ��ġ���� y������ 1��ŭ �ø�
            _buildings.Add(building);
            selectedSlot.SetOccupied(true);

            // ������ ����
            var monkeyPrefab = monkeyPrefabs[0];
            var monkeyObject = Instantiate(monkeyPrefab.gameObject, building.transform);
            var monkey = monkeyObject.GetComponent<Monkey>();
            monkey.transform.localPosition = new Vector3(1f, 0f, 0f); // �ǹ��� ������� ��ġ ����

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Where(_ => monkey.MonkeyLevel > 0)
                .Subscribe(_ =>
                {
                    var goldIncrease = monkey.MonkeyLevel * 5;
                    _totalGold += goldIncrease;
                    UpdateTotalGoldText();
                })
                .AddTo(monkey);

            monkey.OnUpgradeAsObservable()
                .Subscribe(_ =>
                {
                    monkey.MonkeyUpgrade();
                    Debug.Log(" ������ ���� : " + monkey.MonkeyLevel);
                })
                .AddTo(monkey);
        }
        else
        {
            Debug.LogWarning("������ �Ǽ��� �ڸ��� �����ϴ�..");
            Destroy(buildingObject);
        }

        Observable.Interval(TimeSpan.FromSeconds(1))
            .Where(_ => building.buildingLevel > 0)
            .Subscribe(_ =>
            {
                var goldIncrease = building.buildingLevel * 10;
                _totalGold += goldIncrease;
                UpdateTotalGoldText();
            })
            .AddTo(building);

        building.OnUpgradeAsObservable()
            .Subscribe(_ =>
            {
                building.BuildingUpgrade();
                Debug.Log(" ���� ���� : " + building.buildingLevel);
            })
            .AddTo(building);
    }

    public void BuildMonkey()
    {
        var monkeyPrefab = monkeyPrefabs[0];
        var monkeyObject = Instantiate(monkeyPrefab.gameObject);
        var monkey = monkeyObject.GetComponent<Monkey>();


        Observable.Interval(TimeSpan.FromSeconds(1))
            .Where(_ => monkey.MonkeyLevel > 0)
            .Subscribe(_ =>
            {
                var goldIncrease = monkey.MonkeyLevel * 5;
                _totalGold += goldIncrease;
                UpdateTotalGoldText();
            })
            .AddTo(monkey);

        monkey.OnUpgradeAsObservable()
            .Subscribe(_ =>
            {
                monkey.MonkeyUpgrade();
                Debug.Log("Monkey Upgraded. MonkeyLevel: " + monkey.MonkeyLevel);
            })
            .AddTo(monkey);
    }

    private Vector3 GetCameraLookDirection()
    {
        Camera mainCamera = Camera.main;
        return mainCamera.transform.forward;
    }

    private void UpdateTotalGoldText()
    {
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                if (totalGoldText != null)
                {
                    totalGoldText.text = " " + _totalGold;
                }

            });
        
       
    }
}