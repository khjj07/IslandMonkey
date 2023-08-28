using System.Collections;
using System.Collections.Generic;
using Assets._0_IslandMonkey.CHJ.Scripts.Upgrade;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    // ��ɽü��� ��� Building�� (building instansiate�� �� ��ɽü��̸� ���⿡ �߰����־�� ��)
    public List<Building> FunctionalBuildings;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AddBuildingToList(Building building)
    {
        Building _building = new Building();
        _building = building;
        FunctionalBuildings.Add(_building);
    }
}
