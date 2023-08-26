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

    // �ǹ��� ��ġ �̵�, ����, �����Ҷ� ȣ���ؾ���(NavMesh �� ���� �Լ�.)
    public void UpdateNavMesh()
    {
        // NavMeshSurface�� ������� NavMesh�� ��Ÿ�ӿ� ������Ʈ�մϴ�.
        surface.BuildNavMesh();
    }
}
