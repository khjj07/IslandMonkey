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
        // 건물 내부위치로 이동
        // Point => inside로 이동
        monkeyContext.InMonkey(); // 안으로 들어오는 애니메이션 재생
        monkeyTransform.position = Vector3.MoveTowards(monkeyTransform.position, Inside.transform.position, moveSpeed * Time.deltaTime);
        setMonkeyIn();
    }

    public void goOutside() 
    {
        // 건물 외부위치로 이동
        // inside => Point로 이동
        monkeyContext.OutMonkey(); // 밖으로 나가는 애니메이션 재생
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
