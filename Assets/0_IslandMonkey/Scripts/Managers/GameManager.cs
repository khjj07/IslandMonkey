using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using System.Collections.Generic;
using Assets._0_IslandMonkey.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int banana;
    public int gold;
    public int clam;
    public Transform placeOrigin;
    public Place placePrefab;

    public void Start()
    {
        //아마도 여기서 firebase 연동
        this.UpdateAsObservable().Select(_ => banana).DistinctUntilChanged().Subscribe(_ =>
        {
            UIManager.instance.SetBanana(banana);
        });
        this.UpdateAsObservable().Select(_ => gold).DistinctUntilChanged().Subscribe(_ =>
        {
            UIManager.instance.SetGold(gold);
        });
        this.UpdateAsObservable().Select(_ => clam).DistinctUntilChanged().Subscribe(_ =>
        {
            UIManager.instance.SetClam(clam);
        });
    }

    public void ShowPlacableArea()
    {
        List<Vector2Int> indexList = new List<Vector2Int>();
        foreach (var b in BuildingManager.instance._buildings)
        {
            Vector2Int[] increasementList = { new Vector2Int(2, 0), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-2, 0), new Vector2Int(-1, 1), new Vector2Int(1, 1) };
            foreach (var increasement in increasementList)
            {
                var newIndex = b.index + increasement;
                var isPlaced = BuildingManager.instance._buildings.Find(_ =>
                {
                    return newIndex == _.index;
                });
                if (isPlaced !=null)
                {
                    indexList.Add(newIndex);
                }
            }
        }

    }

    public void EarnGold(int income)
    {
        gold += income;
    }

    public void EarnBanana(int income)
    {
        banana += income;
    }

    public void EarnClam(int income)
    {
        clam += income;
    }
}