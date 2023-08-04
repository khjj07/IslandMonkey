using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using System.Collections.Generic;
using Assets._0_IslandMonkey.CHJ.Scripts.Upgrade;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private List<Building> _buildingPrefabs; // 다양한 종류의 빌딩 프리팹을 리스트로 선언

    [SerializeField]
    private List<Monkey> _monkeyPrefabs; // 다양한 종류의 원숭이 프리팹을 리스트로 선언

    [SerializeField]
    private List<InstallablePlace> _InstallablePlaceSlots; // 지면 슬롯을 리스트로 선언


    public static int _totalGold;
    public static int _totalShell;
    public static int _totalMonkey;


    [SerializeField]
    private TextMeshProUGUI _totalGoldText; // TextMeshPro 오브젝트를 할당받을 변수

    [SerializeField]
    private TextMeshProUGUI _totalShellText; // TextMeshPro 오브젝트를 할당받을 변수

    [SerializeField]
    private TextMeshProUGUI _totalMonkeyText; // TextMeshPro 오브젝트를 할당받을 변수

    private ReactiveCollection<Building> _buildings = new ReactiveCollection<Building>();
    private ReactiveCollection<Monkey> _monkeys = new ReactiveCollection<Monkey>();


    private void Start()
    {
        // 씬 전환이 일어났을 때 GameManager 인스턴스가 유지되도록 설정
        // DontDestroyOnLoad(gameObject);
        UpdateTotalMonkeyText();
        UpdateTotalGoldText();
        UpdateTotalShellText();
    }
    public void CreateBuilding()
    {
        var buildingPrefab = _buildingPrefabs[0]; // 프리팹 선택
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();
        // 유학 시: 원숭이 생성
        var monkeyPrefab = _monkeyPrefabs[0];
        var monkeyObject = Instantiate(monkeyPrefab.gameObject, building.transform);
        var monkey = monkeyObject.GetComponent<Monkey>();
        _totalMonkey = _totalMonkey + 1;

        monkey.transform.localPosition = new Vector3(-0.25f, 0f, 0f); // 건물에 상대적인 위치 설정
        monkey.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        var availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();
        if (availableGroundSlots.Count > 0)
        {
            var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position + new Vector3(-0.5f, 0.01f, 0.5f);
            _buildings.Add(building);
            selectedSlot.SetOccupied(false);
        }
        else
        {
            Debug.LogWarning("빌딩을 건설할 자리가 없습니다..");
            Destroy(buildingObject);
        }

        Observable.Interval(TimeSpan.FromSeconds(1))
            .Where(_ => monkey.monkeyLevel > 0)
            .Subscribe(_ =>
            {
                var goldIncrease = monkey.monkeyLevel * 5;
                _totalGold += goldIncrease;
                UpdateTotalGoldText();
            })
            .AddTo(monkey);

        monkey.OnUpgradeAsObservable()
            .Subscribe(_ =>
            {
                monkey.MonkeyUpgrade();
                Debug.Log(" 원숭이 레벨 : " + monkey.monkeyLevel);
            })
            .AddTo(monkey);

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

        // 빌딩이 지어진 후에 availableGroundSlots를 업데이트
        availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();
    }
    public void CreateBuildingEx1()
    {
        var buildingPrefab = _buildingPrefabs[1]; // 프리팹 선택
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();

        var availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();
        if (availableGroundSlots.Count > 0)
        {
            var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position + new Vector3(-0.5f, 0.01f, 0.5f);
            _buildings.Add(building);
            selectedSlot.SetOccupied(false);

            // 유학 시: 원숭이 생성
            var monkeyPrefab = _monkeyPrefabs[0];
            var monkeyObject = Instantiate(monkeyPrefab.gameObject, building.transform);
            var monkey = monkeyObject.GetComponent<Monkey>();
            _totalMonkey = _totalMonkey + 1;

            monkey.transform.localPosition = new Vector3(3f, 0f, 0f); // 건물에 상대적인 위치 설정
            monkey.transform.localScale = new Vector3(1f, 1f, 1f);

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Where(_ => monkey.monkeyLevel > 0)
                .Subscribe(_ =>
                {
                    var goldIncrease = monkey.monkeyLevel * 5;
                    _totalGold += goldIncrease;
                    UpdateTotalGoldText();
                })
                .AddTo(monkey);

            monkey.OnUpgradeAsObservable()
                .Subscribe(_ =>
                {
                    monkey.MonkeyUpgrade();
                    Debug.Log(" 원숭이 레벨 : " + monkey.monkeyLevel);
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

        // 빌딩이 지어진 후에 availableGroundSlots를 업데이트
        availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();
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
                _totalGoldText.text = FormatGoldText(_totalGold);
            }
        })
        .AddTo(this); // 옵저버블에 GameManager를 연결하여 OnDestroy() 시 옵저버블 구독 해지
    }
    private string FormatGoldText(int gold)
    {
        string[] suffixes = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
            "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        if (gold <= 0)
        {
            return "0";
        }

        int suffixIndex = 0;
        decimal goldValue = gold;

        while (goldValue >= 1000)
        {
            goldValue /= 1000;
            suffixIndex++;
        }

        return $"{goldValue:F1}{suffixes[suffixIndex]}";
    }
    // gold 표시

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
    private void UpdateTotalMonkeyText()
    {
        Observable.Interval(TimeSpan.FromSeconds(1))
        .Subscribe(_ =>
        {
            if (_totalMonkeyText != null)
            {
                _totalMonkeyText.text = " " + _totalMonkey;
            }
        })
        .AddTo(this); // 옵저버블에 GameManager를 연결하여 OnDestroy() 시 옵저버블 구독 해지

    }




    // 게임 매니저 데이터 저장
    public void SaveGameManagerData()
    {
        // 빌딩과 지면 슬롯의 상태를 저장
        foreach (var building in _buildings)
        {
            PlayerPrefs.SetInt($"BuildingOccupied_{building.name}", 1); // 빌딩이 점유된 상태를 1로 저장
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

        foreach (var slot in _InstallablePlaceSlots)
        {
            var isOccupied = PlayerPrefs.GetInt($"GroundOccupied_{slot.name}", 0) == 1;
            slot.SetOccupied(isOccupied); // 저장된 상태에 따라 슬롯의 점유 여부를 설정
        }

        _totalGold = PlayerPrefs.GetInt("TotalGold", 0);
        _totalShell = PlayerPrefs.GetInt("TotalShell", 0);
        
        UpdateTotalGoldText();
        UpdateTotalShellText();

    }
}