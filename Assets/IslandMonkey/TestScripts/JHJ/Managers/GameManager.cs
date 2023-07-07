using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using Assets.IslandMonkey.TestScripts.JHJ.Upgrade;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<Building> _buildingPrefabs; // 다양한 종류의 빌딩 프리팹을 리스트로 선언

    [SerializeField]
    private List<Monkey> _monkeyPrefabs; // 다양한 종류의 원숭이 프리팹을 리스트로 선언

    [SerializeField]
    private List<Ground> _builddSlots; // 지면 슬롯을 리스트로 선언

    
    public static int _totalGold;

    public static int _totalShell;

    [SerializeField]
    private TextMeshProUGUI _totalGoldText; // TextMeshPro 오브젝트를 할당받을 변수

    [SerializeField]
    private TextMeshProUGUI _totalShellText; // TextMeshPro 오브젝트를 할당받을 변수

    private ReactiveCollection<Building> _buildings = new ReactiveCollection<Building>();
    private ReactiveCollection<Monkey> _monkeys = new ReactiveCollection<Monkey>();

    private void Start()
    {
        // 씬 전환이 일어났을 때 GameManager 인스턴스가 유지되도록 설정
        DontDestroyOnLoad(gameObject);

        UpdateTotalGoldText();
        UpdateTotalShellText();
    }
    public void CreateBuilding()
    {
        var buildingPrefab = _buildingPrefabs[0]; // 프리팹 선택
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();

        var availableGroundSlots = _builddSlots.Where(slot => !slot.IsOccupied.Value).ToList();
        if (availableGroundSlots.Count > 0)
        {
            var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position + new Vector3(0f, 0.4f, 0f);
            // Ground의 위치에서 y축으로 1만큼 올림 Grid로 하면 달라질듯
            _buildings.Add(building);
            selectedSlot.SetOccupied(true);

            // 유학 시 : 원숭이 생성
            var monkeyPrefab = _monkeyPrefabs[0];
            var monkeyObject = Instantiate(monkeyPrefab.gameObject, building.transform);
            var monkey = monkeyObject.GetComponent<Monkey>();
            monkey.transform.localPosition = new Vector3(1f, 0.1f, 0f); // 건물에 상대적인 위치 설정

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
                    Debug.Log(" 원숭이 레벨 : " + monkey.MonkeyLevel);
                })
                .AddTo(monkey);
        }
        else
        {
            Debug.LogWarning("빌딩을 건설할 자리가 없습니다..");
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
                Debug.Log(" 빌딩 레벨 : " + building.buildingLevel);
            })
            .AddTo(building);
    }

    public void CreateMonkey()
    {
        var monkeyPrefab = _monkeyPrefabs[0];
        var monkeyObject = Instantiate(monkeyPrefab.gameObject);
        var monkey = monkeyObject.GetComponent<Monkey>();

        /*
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
                        //monkey.MonkeyUpgrade();
                        //monkey 업그레이드 없음
                        Debug.Log(" 원숭이 레벨 " + monkey.MonkeyLevel);
                    })
                    .AddTo(monkey);*/
    }

    private void UpdateTotalGoldText()
    {
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                if (_totalGoldText != null)
                {
                    _totalGoldText.text = " " + _totalGold;
                }

            });


    }
    private void UpdateTotalShellText()
    {
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                if (_totalShellText != null)

                {
                    _totalShellText.text = " " + GameManager._totalShell;
                }
            });


    }
    // 게임 매니저 데이터 저장
    public void SaveGameManagerData()
    {
        PlayerPrefs.SetInt("TotalGold", _totalGold);
        PlayerPrefs.SetInt("TotalShell", _totalShell);
    }

    // 게임 매니저 데이터 복원
    public void LoadGameManagerData()
    {
        _totalGold = PlayerPrefs.GetInt("TotalGold");
        _totalShell = PlayerPrefs.GetInt("TotalShell");
        UpdateTotalGoldText();
    }
}