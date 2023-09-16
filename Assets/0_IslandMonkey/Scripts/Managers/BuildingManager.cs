using System;
using System.Collections;
using System.Collections.Generic;
using Assets._0_IslandMonkey.Scripts.Abstract;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    [Serializable]
    public class InstalledBuilding
    {
        public Vector2Int index;
        public Building building;
        public InstalledBuilding(Vector2Int index, Building building)
        {
            this.index = index;
            this.building = building;
        }
    }

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
    public List<InstalledBuilding> _buildings;

    [SerializeField]
    private Transform origin;

    public float offset = 1.0f;

    void Start()
    {
        _buildingPrefabs = new List<Building>();
        _buildings = new List<InstalledBuilding>();
    }

    public void CreateBuilding(Type type, Vector2Int index)
    {
        var building = Instantiate(_buildingPrefabs[(int)type], origin);
        building.transform.position = GetIndexPosition(index);
        _buildings.Add(new InstalledBuilding(index, building));
    }

    public Vector3 GetIndexPosition(Vector2Int index)
    {
        return new Vector3(offset / 2 * index.x, 0, offset * index.y);
    }

}
