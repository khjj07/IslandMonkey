using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPath : MonoBehaviour
{
    public Transform nodeA;
    public Transform nodeB;
    public float curveAngle = 30f; // 곡선 경로의 회전 각도
    public float curveDistanceMultiplier = 0.5f; // 곡선 경로의 거리 비율

    private Transform train;
    private int pathType = 0; // 0: 직선, 1: 오른쪽 곡선, 2: 왼쪽 곡선

    private void Start()
    {
        train = transform;
    }

    private void Update()
    {
        switch (pathType)
        {
            case 0: // 직선 경로
                train.position = Vector3.Lerp(nodeA.position, nodeB.position, 0.5f);
                break;

            case 1: // 오른쪽으로 휜 곡선
                Vector3 middlePointR = (nodeA.position + nodeB.position) / 2;
                Vector3 directionR = Quaternion.Euler(0, curveAngle, 0) * (nodeA.position - nodeB.position).normalized;
                train.position = middlePointR + directionR * curveDistanceMultiplier;
                break;

            case 2: // 왼쪽으로 휜 곡선
                Vector3 middlePointL = (nodeA.position + nodeB.position) / 2;
                Vector3 directionL = Quaternion.Euler(0, -curveAngle, 0) * (nodeA.position - nodeB.position).normalized;
                train.position = middlePointL + directionL * curveDistanceMultiplier;
                break;
        }
    }

    public void SetPathType(int newPathType)
    {
        pathType = newPathType;
    }
}
