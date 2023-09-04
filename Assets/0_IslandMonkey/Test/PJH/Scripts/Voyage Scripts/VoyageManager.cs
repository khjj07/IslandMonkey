using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class VoyageManager : MonoBehaviour
{
    [SerializeField] public float VoyageTime;
    [SerializeField] public float ShellfishTime;

    [SerializeField] private GameObject[] Shellfishes;
    [SerializeField] private GameObject[] Helps;
    [SerializeField] private Slider VoyageBar;
    [SerializeField] private GameObject Boat;
    [SerializeField] private GameObject VoyagePopUp;

    //private float StartTime;
    private Shellfish ShellfishItem;
    private bool isReturn = false;
    private bool isEnd = false;

    private System.Random randomObj = new System.Random();

    private void Start()
    {
        //StartTime = Time.time;
        VoyageBar.maxValue = VoyageTime;
        VoyageBar.value = VoyageTime;
        InvokeRepeating("CreateShellfish", ShellfishTime, ShellfishTime);
    }

    private void CreateShellfish()
    {
        int index = randomObj.Next(7);
        ShellfishItem = Shellfishes[index].GetComponent<Shellfish>();
        if (!ShellfishItem.isActiveAndEnabled)
        {
            //Debug.Log("Shellfish ����!");
            ShellfishItem.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (VoyageTime > 0.0f)
        {
            VoyageTime -= (Time.deltaTime);
            VoyageBar.value = VoyageTime;
            if ((VoyageTime < VoyageBar.maxValue / 2) && !isReturn)
            {
                //Boat.transform.Rotate(0, 180, 0);
                Debug.Log("���Ƶ���");
                isReturn = true;
            }
        }
        else if (!isEnd)
        {
            VoyageEnd();
            isEnd = true;
        }
    }

    private void VoyageEnd()
    {
        CancelInvoke("CreateShellfish");
        foreach (GameObject shellfish in Shellfishes)
        {
            ShellfishItem = shellfish.GetComponent<Shellfish>();
            if (ShellfishItem.isActiveAndEnabled)
            {
                ShellfishItem.gameObject.SetActive(false);
            }
        }

        Debug.Log("���� ����");
        VoyagePopUp.SetActive(true);

    }
}