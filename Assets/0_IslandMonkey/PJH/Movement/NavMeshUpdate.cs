using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdate : MonoBehaviour
{
    public NavMeshSurface surface;
    private void Start()
    {
        surface = GetComponent<NavMeshSurface>();
    }

    private void Update()
    {
        UpdateNavMesh();
    }

    // 건물의 위치 이동, 생성, 삭제할때 호출해야함(NavMesh 땅 굽는 함수.)
    public void UpdateNavMesh()
    {
        // NavMeshSurface를 기반으로 NavMesh를 런타임에 업데이트합니다.
        surface.BuildNavMesh();
    }
}
