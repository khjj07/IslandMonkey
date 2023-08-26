using System.Collections;
using System.Collections.Generic;
using Assets._0_IslandMonkey.CHJ.Scripts.Upgrade;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    // ��ɽü��� ��� Building�� (building instansiate�� �� ��ɽü��̸� ���⿡ �߰����־�� ��)
    public List<Building> FunctionalBuildings;

    public void AddBuildingToList(Building building)
    {
        FunctionalBuildings.Add(building);
    }
}
