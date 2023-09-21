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
    public enum State
    {
        Play,
        Place,
        Menu
    }

    public int banana;
    public int gold;
    public int clam;
    public State currentState = State.Play;
    public Place selectedPlace;

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

        var currentStateStream = this.UpdateAsObservable().Select(_ => currentState);

        currentStateStream.Where(x => x == State.Place)
            .Where(_ => selectedPlace);
        //.Subscribe(_ => { BuildingManager.instance.CreateBuilding();});
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