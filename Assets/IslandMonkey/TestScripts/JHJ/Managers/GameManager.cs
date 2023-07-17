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
        // DontDestroyOnLoad(gameObject);

        UpdateTotalGoldText();
        UpdateTotalShellText();
    }
    public void CreateBuilding()
    {
        var buildingPrefab = _buildingPrefabs[0]; // 프리팹 선택
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();

        var availableGroundSlots = _builddSlots.Where(slot => !slot.IsOccupied).ToList();
        if (availableGroundSlots.Count > 0)
        {
            var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position + new Vector3(0f, 0.4f, 0f);
            _buildings.Add(building);
            selectedSlot.SetOccupied(true); // 슬롯을 점유로 설정

            // 유학 시: 원숭이 생성
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

            // 빌딩이 지어진 후에 availableGroundSlots를 업데이트해줍니다.
            availableGroundSlots = _builddSlots.Where(slot => !slot.IsOccupied).ToList();
        }
        else
        {
            Debug.LogWarning("빌딩을 건설할 자리가 없습니다..");
            Destroy(buildingObject);
        }
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
        // 빌딩과 지면 슬롯의 상태를 저장
        foreach (var building in _buildings)
        {
            PlayerPrefs.SetInt($"BuildingOccupied_{building.name}", 1); // 빌딩이 점유된 상태를 1로 저장
        }

        foreach (var slot in _builddSlots)
        {
            PlayerPrefs.SetInt($"GroundOccupied_{slot.name}", slot.IsOccupied ? 1 : 0); // 슬롯의 점유 여부를 1 또는 0으로 저장
        }

        PlayerPrefs.SetInt("TotalGold", _totalGold);
        PlayerPrefs.SetInt("TotalShell", _totalShell);

        // 저장된 데이터를 디스크에 기록
        PlayerPrefs.Save();

    }

    // 게임 매니저 데이터 복원
    public void LoadGameManagerData()
    {
        // 저장된 빌딩과 지면 슬롯의 상태를 복원
        foreach (var building in _buildings)
        {
            var isOccupied = PlayerPrefs.GetInt($"BuildingOccupied_{building.name}", 0) == 1;
            building.gameObject.SetActive(isOccupied); // 저장된 상태에 따라 빌딩을 활성화 또는 비활성화
        }

        foreach (var slot in _builddSlots)
        {
            var isOccupied = PlayerPrefs.GetInt($"GroundOccupied_{slot.name}", 0) == 1;
            slot.SetOccupied(isOccupied); // 저장된 상태에 따라 슬롯의 점유 여부를 설정
        }

        _totalGold = PlayerPrefs.GetInt("TotalGold", 0);
        _totalShell = PlayerPrefs.GetInt("TotalShell", 0);
        
        UpdateTotalGoldText();

    }
}