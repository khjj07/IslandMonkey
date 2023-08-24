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
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private List<Building> _buildingPrefabs; // �پ��� ������ ���� �������� ����Ʈ�� ����

    [SerializeField]
    private List<Monkey> _monkeyPrefabs; // �پ��� ������ ������ �������� ����Ʈ�� ����

    [SerializeField]
    private List<InstallablePlace> _InstallablePlaceSlots; // ���� ������ ����Ʈ�� ����


    public static int _totalGold;
    public static int _totalBanana;
    public static int _totalShell;
    public static int _totalMonkey;


    [SerializeField]
    private TextMeshProUGUI _totalGoldText; // TextMeshPro ������Ʈ�� �Ҵ���� ����

    [SerializeField]
    private TextMeshProUGUI _totalBananaText; // TextMeshPro ������Ʈ�� �Ҵ���� ����

    [SerializeField]
    private TextMeshProUGUI _totalShellText; // TextMeshPro ������Ʈ�� �Ҵ���� ����

    [SerializeField]
    private TextMeshProUGUI _totalMonkeyText; // TextMeshPro ������Ʈ�� �Ҵ���� ����

    private ReactiveCollection<Building> _buildings = new ReactiveCollection<Building>();
    private ReactiveCollection<Monkey> _monkeys = new ReactiveCollection<Monkey>();


    private void Start()
    {
        // �� ��ȯ�� �Ͼ�� �� GameManager �ν��Ͻ��� �����ǵ��� ����
        DontDestroyOnLoad(gameObject);
        UpdateTotalMonkeyText();
        UpdateTotalGoldText();
        UpdateTotalBananaText();
        UpdateTotalShellText();

        CreateBuildingEx2();


    }
    public void CreateBuilding()
    {
        // ���� �ü� ���� 1
        var buildingPrefab = _buildingPrefabs[0]; // ������ ����
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();

        var availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();
        if (availableGroundSlots.Count > 0)
        {
            //var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var randomSlotIndex = 2;
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position + new Vector3(0f, -0.15f, -0.5f);
            _buildings.Add(building);
            selectedSlot.SetOccupied(false);

            // ���� ��: ������ ����
            var monkeyPrefab = _monkeyPrefabs[0];
            var monkeyObject = Instantiate(monkeyPrefab.gameObject, building.transform);
            var monkey = monkeyObject.GetComponent<Monkey>();
            _totalMonkey = _totalMonkey + 1;

            // ������ �ִϸ��̼� �۵� �� ����
            monkey.transform.localPosition = new Vector3(-1.5f, 0f, 0f); // �ǹ��� ������� ��ġ ����
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
                    Debug.Log(" ������ ���� : " + monkey.monkeyLevel);
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

        // ������ ������ �Ŀ� availableGroundSlots�� ������Ʈ
        availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();

        
    }
    public void CreateBuildingEx1()
    {
        // ���� �ü� ���� 2
        var buildingPrefab = _buildingPrefabs[1]; // ������ ����
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();

        var availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();
        if (availableGroundSlots.Count > 0)
        {
            //var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var randomSlotIndex =1;
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position + new Vector3(0f, 0.01f, 0.0f);
            _buildings.Add(building);
            selectedSlot.SetOccupied(false);

            // ���� ��: ������ ����
            var monkeyPrefab = _monkeyPrefabs[0];
            var monkeyObject = Instantiate(monkeyPrefab.gameObject, building.transform);
            var monkey = monkeyObject.GetComponent<Monkey>();
            _totalMonkey = _totalMonkey + 1;

            // ������ �ִϸ��̼� �۵� �� ����
            monkey.transform.localPosition = new Vector3(2f, 0f, 0f); // �ǹ��� ������� ��ġ ����
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
                    Debug.Log(" ������ ���� : " + monkey.monkeyLevel);
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

        // ������ ������ �Ŀ� availableGroundSlots�� ������Ʈ
        availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();

        
    }
    public void CreateBuildingEx2()
    {
        // ��� �ü� ���� 1
        var buildingPrefab = _buildingPrefabs[2]; // ������ ����
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();

        var availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();
        if (availableGroundSlots.Count > 0)
        {
            //var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var randomSlotIndex = 0;
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position + new Vector3(0f, 0.01f, 0.0f);
            _buildings.Add(building);
            selectedSlot.SetOccupied(false);

            // ���� ��: ������ ����
            var monkeyPrefab = _monkeyPrefabs[1];
            var monkeyObject = Instantiate(monkeyPrefab.gameObject, building.transform);
            var monkey = monkeyObject.GetComponent<Monkey>();
            _totalMonkey = _totalMonkey + 1;

            // ������ �ִϸ��̼� �۵� �� ����
            monkey.transform.localPosition = new Vector3(2f, 0f, 0f); // �ǹ��� ������� ��ġ ����
            monkey.transform.localScale = new Vector3(1f, 1f, 1f);

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Where(_ => monkey.monkeyLevel > 0)
                .Subscribe(_ =>
                {
                    var bananaIncrease = monkey.monkeyLevel * 5;
                    _totalBanana += bananaIncrease;
                    UpdateTotalBananaText();
                })
                .AddTo(monkey);

            monkey.OnUpgradeAsObservable()
                .Subscribe(_ =>
                {
                    monkey.MonkeyUpgrade();
                    Debug.Log(" ������ ���� : " + monkey.monkeyLevel);
                })
                .AddTo(monkey);
            building.OnUpgradeAsObservable()
                .Subscribe(_ =>
                {
                    building.BuildingUpgrade();
                    Debug.Log(" ���� ���� : " + building.buildingLevel);
                })
                .AddTo(building);
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
                var bananaIncrease = building.buildingLevel * 10;
                _totalBanana += bananaIncrease;
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

        // ������ ������ �Ŀ� availableGroundSlots�� ������Ʈ
        availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();


    }
    public void CreateBuildingEx3()
    {
        // Ư�� �ü� ���� 1

        var buildingPrefab = _buildingPrefabs[3]; // ������ ����
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();

        var availableGroundSlots = _InstallablePlaceSlots.Where(slot => !slot.IsOccupied.Value).ToList();
        if (availableGroundSlots.Count > 0)
        {
            //var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var randomSlotIndex = 3;
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position + new Vector3(0f, 0.01f, 0.0f);
            _buildings.Add(building);
            selectedSlot.SetOccupied(false);

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

        // ������ ������ �Ŀ� availableGroundSlots�� ������Ʈ
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
                        //monkey ���׷��̵� ����
                        Debug.Log(" ������ ���� " + monkey.MonkeyLevel);
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
        .AddTo(this); // �������� GameManager�� �����Ͽ� OnDestroy() �� �������� ���� ����
    }

    private void UpdateTotalBananaText()
    {
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
        {
            if (_totalBananaText != null)
            {
                _totalBananaText.text = FormatBananaText(_totalBanana);
            }
        })
        .AddTo(this);
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
    // gold ǥ��
    private string FormatBananaText(int banana)
    {
        string[] suffixes = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
            "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        if (banana <= 0)
        {
            return "0";
        }

        int suffixIndex = 0;
        decimal bananaValue = banana;

        while (bananaValue >= 1000)
        {
            bananaValue /= 1000;
            suffixIndex++;
        }

        return $"{bananaValue:F1}{suffixes[suffixIndex]}";
    }
    //�ٳ��� ǥ��

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
        .AddTo(this); // �������� GameManager�� �����Ͽ� OnDestroy() �� �������� ���� ����

    }




    // ���� �Ŵ��� ������ ����
    public void SaveGameManagerData()
    {
        // ������ ���� ������ ���¸� ����
        foreach (var building in _buildings)
        {
            PlayerPrefs.SetInt($"BuildingOccupied_{building.name}", 1); // ������ ������ ���¸� 1�� ����
        }

        PlayerPrefs.SetInt("TotalGold", _totalGold);
        PlayerPrefs.SetInt("TotalShell", _totalShell);

        // ����� �����͸� ��ũ�� ���
        PlayerPrefs.Save();

    }

    // ���� �Ŵ��� ������ ����
    public void LoadGameManagerData()
    {
        // ����� ������ ���� ������ ���¸� ����
        foreach (var building in _buildings)
        {
            var isOccupied = PlayerPrefs.GetInt($"BuildingOccupied_{building.name}", 0) == 1;
            building.gameObject.SetActive(isOccupied); // ����� ���¿� ���� ������ Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
        }

        foreach (var slot in _InstallablePlaceSlots)
        {
            var isOccupied = PlayerPrefs.GetInt($"GroundOccupied_{slot.name}", 0) == 1;
            slot.SetOccupied(isOccupied); // ����� ���¿� ���� ������ ���� ���θ� ����
        }

        _totalGold = PlayerPrefs.GetInt("TotalGold", 0);
        _totalShell = PlayerPrefs.GetInt("TotalShell", 0);
        
        UpdateTotalGoldText();
        UpdateTotalShellText();

    }
}