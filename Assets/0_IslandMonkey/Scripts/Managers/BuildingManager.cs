using System.Collections;
using System.Collections.Generic;
using Assets._0_IslandMonkey.Scripts.Abstract;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    public enum Type
    {
        BananaHouse,
        FireStation,
        IceLink,
        Mongstagram,
        Albamong
    }

    [SerializeField]
    private List<Building> _buildingPrefabs;

    [SerializeField]
    private List<Building> _buildings;

    [SerializeField]
    private Transform origin;

    void Start()
    {
        _buildings = new List<Building>();
    }

    public void CreateBuilding(Type type,Place place)
    {
        var building = Instantiate(_buildingPrefabs[(int)type], origin);
        building.SetPlace(place);
        _buildings.Add(building);
    }

}
