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

    [SerializeField]
    private TextMeshProUGUI _totalGoldText; // TextMeshPro ������Ʈ�� �Ҵ���� ����

    [SerializeField]
    private TextMeshProUGUI _totalShellText; // TextMeshPro ������Ʈ�� �Ҵ���� ����

    private ReactiveCollection<Building> _buildings = new ReactiveCollection<Building>();
    private ReactiveCollection<Monkey> _monkeys = new ReactiveCollection<Monkey>();

    private void Start()
    {
        // �� ��ȯ�� �Ͼ�� �� GameManager �ν��Ͻ��� �����ǵ��� ����
        DontDestroyOnLoad(gameObject);

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
            building.transform.position = selectedSlot.transform.position + new Vector3(0f, 0.4f, 0f);
            // Ground�� ��ġ���� y������ 1��ŭ �ø� Grid�� �ϸ� �޶�����
            _buildings.Add(building);
            selectedSlot.SetOccupied(true);

            // ���� �� : ������ ����
            var monkeyPrefab = _monkeyPrefabs[0];
            var monkeyObject = Instantiate(monkeyPrefab.gameObject, building.transform);
            var monkey = monkeyObject.GetComponent<Monkey>();
            monkey.transform.localPosition = new Vector3(1f, 0.1f, 0f); // �ǹ��� ������� ��ġ ����

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
    // ���� �Ŵ��� ������ ����
    public void SaveGameManagerData()
    {
        PlayerPrefs.SetInt("TotalGold", _totalGold);
        PlayerPrefs.SetInt("TotalShell", _totalShell);
    }

    // ���� �Ŵ��� ������ ����
    public void LoadGameManagerData()
    {
        _totalGold = PlayerPrefs.GetInt("TotalGold");
        _totalShell = PlayerPrefs.GetInt("TotalShell");
        UpdateTotalGoldText();
    }
}