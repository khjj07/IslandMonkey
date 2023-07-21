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
    private List<Building> _buildingPrefabs; // �پ��� ������ ���� �������� ����Ʈ�� ����

    [SerializeField]
    private List<Monkey> _monkeyPrefabs; // �پ��� ������ ������ �������� ����Ʈ�� ����

    [SerializeField]
    private List<Ground> _builddSlots; // ���� ������ ����Ʈ�� ����


    public static int _totalGold;

    public static int _totalShell;

    public static int _totalMonkey;


    [SerializeField]
    private TextMeshProUGUI _totalGoldText; // TextMeshPro ������Ʈ�� �Ҵ���� ����

    [SerializeField]
    private TextMeshProUGUI _totalShellText; // TextMeshPro ������Ʈ�� �Ҵ���� ����

    [SerializeField]
    private TextMeshProUGUI _totalMonkeyText; // TextMeshPro ������Ʈ�� �Ҵ���� ����

    private ReactiveCollection<Building> _buildings = new ReactiveCollection<Building>();
    private ReactiveCollection<Monkey> _monkeys = new ReactiveCollection<Monkey>();


    private void Start()
    {
        // �� ��ȯ�� �Ͼ�� �� GameManager �ν��Ͻ��� �����ǵ��� ����
        // DontDestroyOnLoad(gameObject);
        UpdateTotalMonkeyText();
        UpdateTotalGoldText();
        UpdateTotalShellText();
    }
    public void CreateBuilding()
    {
        var buildingPrefab = _buildingPrefabs[0]; // ������ ����
        var buildingObject = Instantiate(buildingPrefab.gameObject);
        var building = buildingObject.GetComponent<Building>();

        var availableGroundSlots = _builddSlots.Where(slot => !slot.IsOccupied.Value).ToList();
        if (availableGroundSlots.Count > 0)
        {
            var randomSlotIndex = UnityEngine.Random.Range(0, availableGroundSlots.Count);
            var selectedSlot = availableGroundSlots[randomSlotIndex];
            building.transform.position = selectedSlot.transform.position + new Vector3(0f, 0.2f, -0.8f);
            _buildings.Add(building);
            selectedSlot.SetOccupied(false);

            // ���� ��: ������ ����
            var monkeyPrefab = _monkeyPrefabs[0];
            var monkeyObject = Instantiate(monkeyPrefab.gameObject, building.transform);
            var monkey = monkeyObject.GetComponent<Monkey>();
            _totalMonkey = _totalMonkey + 1;
            
            monkey.transform.localPosition = new Vector3(-0.5f, 0.5f, 0f); // �ǹ��� ������� ��ġ ����

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
                _totalGold = _totalGold - 100;
                Debug.Log(" ���� ���� : " + building.buildingLevel);
            })
            .AddTo(building);

        // ������ ������ �Ŀ� availableGroundSlots�� ������Ʈ
        availableGroundSlots = _builddSlots.Where(slot => !slot.IsOccupied.Value).ToList();
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
        .AddTo(this); // ���������� GameManager�� �����Ͽ� OnDestroy() �� �������� ���� ����
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
        .AddTo(this); // ���������� GameManager�� �����Ͽ� OnDestroy() �� �������� ���� ����

    }




    // ���� �Ŵ��� ������ ����
    public void SaveGameManagerData()
    {
        // ������ ���� ������ ���¸� ����
        foreach (var building in _buildings)
        {
            PlayerPrefs.SetInt($"BuildingOccupied_{building.name}", 1); // ������ ������ ���¸� 1�� ����
        }

       /* foreach (var slot in _builddSlots)
        {
            PlayerPrefs.SetInt($"GroundOccupied_{slot.name}", slot.IsOccupied ? 1 : 0); // ������ ���� ���θ� 1 �Ǵ� 0���� ����
        }*/

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

        foreach (var slot in _builddSlots)
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