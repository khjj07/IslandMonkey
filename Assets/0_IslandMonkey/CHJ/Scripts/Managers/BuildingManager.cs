using System.Collections;
using System.Collections.Generic;
using Assets._0_IslandMonkey.CHJ.Scripts.Upgrade;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    // 기능시설만 담긴 Building들 (building instansiate할 때 기능시설이면 여기에 추가해주어야 함)
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
