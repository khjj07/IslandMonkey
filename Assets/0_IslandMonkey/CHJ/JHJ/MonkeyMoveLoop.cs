using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyMoveLoop : MonoBehaviour
{
    [SerializeField]
    private Transform point1;
    [SerializeField]
    private Transform point2;
    [SerializeField]
    private Transform point3;
    [SerializeField]
    private Transform point4;

    [SerializeField]
    private float moveSpeed = 5f;

    private Transform currentTarget;

    private void Start()
    {
        currentTarget = point1;
    }

    private void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        if (transform.position == currentTarget.position)
        {
            SetNextTarget();
        }
    }

    private void SetNextTarget()
    {
        if (currentTarget == point1)
        {
            currentTarget = point2;
        }
        else if (currentTarget == point2)
        {
            currentTarget = point3;
        }
        else if (currentTarget == point3)
        {
            currentTarget = point4;
        }
        else if (currentTarget == point4)
        {
            currentTarget = point1;
        }
    }
}
