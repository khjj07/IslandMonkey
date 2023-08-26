using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private GameObject Inside;
    public MonkeyContext monkeyContext;

    public bool isInMonkey = false;
    public float moveSpeed = 2.0f;


    private void Start()
    {
        Inside = transform.GetChild(0).gameObject;
    }

    public void goInside(Transform monkeyTransform)
    {
        // �ǹ� ������ġ�� �̵�
        // Point => inside�� �̵�
        monkeyContext.InMonkey(); // ������ ������ �ִϸ��̼� ���
        monkeyTransform.position = Vector3.MoveTowards(monkeyTransform.position, Inside.transform.position, moveSpeed * Time.deltaTime);
        setMonkeyIn();
    }

    public void goOutside() 
    {
        // �ǹ� �ܺ���ġ�� �̵�
        // inside => Point�� �̵�
        monkeyContext.OutMonkey(); // ������ ������ �ִϸ��̼� ���
        //monkey.transform.position = Vector3.MoveTowards(monkey.Point.position, this.transform.position, moveSpeed * Time.deltaTime);
        setMonkeyOut();
    }

    public void setMonkeyIn()
    {
        isInMonkey = true;
    }

    public void setMonkeyOut()
    {
        isInMonkey = false;
    }
}
