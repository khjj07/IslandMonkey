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

    private Dictionary<int, float> distances = new Dictionary<int, float>(); // destinations�� <index, distance>
    private NavMeshAgent agent;
    private GameObject targetBuilding;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        // agent �ʱ�ȭ
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
        // distance ����� ������ ����
        distances = distances.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value); ;

        index = 0;
        // ���ĵ� distance �������� destinations ����
        foreach (var dist in distances)
        {
            int key = dist.Key;
            destinations[key] = buildings[index];

            index++;
        }
        targetBuilding = destinations[distances.Keys.First()]; // ���� ����� building�� targetBuilding���� ����
    }

    private int FindTargetBuilding()
    {
        int index = 0;
        bool FindBuilding = false;
        foreach (GameObject building in destinations)
        {
            // targetBuilding�� �����̰� �ִ��� üũ => ���� �����̰� ���� �ǹ� �߿� ���� ����� �ǹ��� target���� ����
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
            Debug.Log("����ִ� ������ ã�ҽ��ϴ�.");
            return 0;
        }
        else
        {
            Debug.Log("����ִ� ������ �����ϴ�.");
            return -1;
        }
    }

    // ������ ������ ����
    public void goHome()
    {
        agent.SetDestination(HomeBuilding.transform.position);
    }

    // ������ ü�������Ϸ� ����
    public void goDestination()
    {
        
        // ����ִ� ������ ���涧���� ��� �ݺ�(���߿� �߰�)
        int isFind = FindTargetBuilding();
        Destination destination = targetBuilding.GetComponent<Destination>();
        destination.setMonkeyIn(); // ������ ������ setMonkeyIn
        //agent.SetDestination(targetBuilding.transform.position);
    }
}