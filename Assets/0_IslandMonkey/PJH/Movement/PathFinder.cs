using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Assertions.Must;
using System;
using Assets._0_IslandMonkey.CHJ.Scripts.Upgrade;
using System.ComponentModel;

public class PathFinder : MonoBehaviour
{
    public GameObject HomeBuilding;

    private List<GameObject> destinations = new List<GameObject>();

    private Dictionary<int, float> distances = new Dictionary<int, float>(); // destinations의 <index, distance>
    private NavMeshAgent agent;
    private GameObject targetBuilding;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        // agent 초기화
        agent = this.GetComponent<NavMeshAgent>();
        setDestinations();

        goDestination();

        //if (agent.remainingDistance < 0.1f)
        //{
        //    agent.speed = 0.1f;
        //}
    }

    private void setDestinations()
    {
        int index = 0;
        GameObject[] buildings = PathFinderManager.instance.originDestinations;
        foreach (GameObject building in buildings)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, building.transform.position);
            distances.Add(index, Distance);
            destinations.Add(building);
            index++;
        }
        // distance 가까운 순서로 정렬
        distances = distances.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value); ;

        index = 0;
        // 정렬된 distance 기준으로 destinations 정렬
        foreach (var dist in distances)
        {
            int key = dist.Key;
            destinations[key] = buildings[index];

            index++;
        }
        targetBuilding = destinations[distances.Keys.First()]; // 가장 가까운 building을 targetBuilding으로 지정
    }

    private int FindTargetBuilding()
    {
        int index = 0;
        bool FindBuilding = false;
        foreach (GameObject building in destinations)
        {
            // targetBuilding에 원숭이가 있는지 체크 => 현재 원숭이가 없는 건물 중에 가장 가까운 건물을 target으로 설정
            Destination destination = building.GetComponent<Destination>();
            if (!destination.isInMonkey)
            {
                targetBuilding = building;
                FindBuilding = true;
                break;
            }
            index++;
        }

        if (FindBuilding)
        {
            Debug.Log("비어있는 빌딩을 찾았습니다.");
            return 0;
        }
        else
        {
            Debug.Log("비어있는 빌딩이 없습니다.");
            return -1;
        }
    }

    // 원숭이 집으로 가기
    public void goHome()
    {
        agent.SetDestination(HomeBuilding.transform.position);
    }

    // 원숭이 체력충전하러 가기
    public void goDestination()
    {
        
        // 비어있는 빌딩이 생길때까지 계속 반복(나중에 추가)
        int isFind = FindTargetBuilding();
        Destination destination = targetBuilding.GetComponent<Destination>();
        destination.setMonkeyIn(); // 가려는 목적지 setMonkeyIn
        //agent.SetDestination(targetBuilding.transform.position);
    }
}