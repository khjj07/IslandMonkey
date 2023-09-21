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
    private List<Building> _buildingPrefabs = new List<Building>();

    [SerializeField]
    public List<InstalledBuilding> _buildings = new List<InstalledBuilding>();

    [SerializeField]
    private Transform _origin;

    [SerializeField]
    private Place _placePrefab;

    public int expandedLevel = 2;

    public float offset = 1.0f;

    public void Start()
    {
        //ShowPlacableArea();
    }

    public void CreateBuilding(Type type, Vector2Int index)
    {
        var building = Instantiate(_buildingPrefabs[(int)type], _origin);
        building.transform.position = GetIndexPosition(index);
        _buildings.Add(new InstalledBuilding(index, building));
    }

    public void ShowPlacableArea()
    {
        List<Vector2Int> indexList = new List<Vector2Int>();
        foreach (var b in _buildings)
        {
            Vector2Int[] increasementList = { new Vector2Int(2, 0), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-2, 0), new Vector2Int(-1, 1), new Vector2Int(1, 1) };
            foreach (var increasement in increasementList)
            {
                var newIndex = b.index + increasement;
                var placedBuilding = _buildings.Find(_ =>
                {
                    return newIndex == _.index;
                });
                if (placedBuilding == null && GetIndexLevel(newIndex) <= expandedLevel)
                {
                    indexList.Add(newIndex);
                }
            }
        }

        foreach (var index in indexList)
        {
            var instance = Instantiate(_placePrefab,_origin);
            instance.transform.localPosition = GetIndexPosition(index);
        }

    }

    public  Vector3 GetIndexPosition(Vector2Int index)
    {
        return new Vector3(offset / 2 * index.x, 0, offset * index.y);
    }

    public int GetIndexLevel(Vector2Int index)
    {
        return Math.Abs((index.x+index.y)/2);
    }
}
