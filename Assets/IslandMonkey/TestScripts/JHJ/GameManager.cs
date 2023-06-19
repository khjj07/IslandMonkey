using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Building[] buildingPrefabs; // 다양한 종류의 빌딩 프리팹을 배열로 선언

    [SerializeField]
    private Monkey[] monkeyPrefabs; // 다양한 종류의 원숭이 프리팹을 배열로 선언

    [SerializeField]
    private Ground[] groundSlots; // 지면 슬롯을 배열로 선언

    [SerializeField]
    private int _totalGold;
    [SerializeField]
    private TextMeshProUGUI totalGoldText; // TextMeshPro 오브젝트를 할당받을 변수

    private ReactiveCollection<Building> _buildings = new ReactiveCollection<Building>();
    private ReactiveCollection<Monkey> _monkeys = new ReactiveCollection<Monkey>();

    private void Start()
    {
        
    }

    public void BuildBuilding()
    {
        var buildingPrefab = buildingPrefabs[0]; // 적절한 인덱스로 프리팹 선택
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();

        var availableGroundSlots = groundSlots.Where(slot => !slot.IsOccupied.Value).ToList();
        if (availableGroundSlots.Count > 0)
        {
            var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position;
            _buildings.Add(building);
            selectedSlot.SetOccupied(true);
        }
        else
        {
            Debug.LogWarning("빌딩을 건설할 자리가 없습니다..");
            Destroy(buildingObject);
        }

        Observable.Interval(TimeSpan.FromSeconds(1))
            .Where(_ => building.Level > 0)
            .Subscribe(_ =>
            {
                var goldIncrease = building.Level * 10;
                _totalGold += goldIncrease;
                UpdateTotalGoldText();
            })
            .AddTo(building);

        building.OnUpgradeAsObservable()
            .Subscribe(_ =>
            {
                building.Upgrade();
                Debug.Log("Building Upgraded. Level: " + building.Level);
            })
            .AddTo(building);
    }

    public void BuildMonkey()
    {
        var monkeyPrefab = monkeyPrefabs[0];
        var monkeyObject = Instantiate(monkeyPrefab.gameObject);
        var monkey = monkeyObject.GetComponent<Monkey>();


        Observable.Interval(TimeSpan.FromSeconds(1))
            .Where(_ => monkey.Level > 0)
            .Subscribe(_ =>
            {
                var goldIncrease = monkey.Level * 5;
                _totalGold += goldIncrease;
                UpdateTotalGoldText();
            })
            .AddTo(monkey);

        monkey.OnUpgradeAsObservable()
            .Subscribe(_ =>
            {
                monkey.Upgrade();
                Debug.Log("Monkey Upgraded. Level: " + monkey.Level);
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
                var goldIncrease = +10;
                _totalGold += goldIncrease;

            });
        if (totalGoldText != null)
        {
            totalGoldText.text = "Total Gold: " + _totalGold;
        }
       
    }
}