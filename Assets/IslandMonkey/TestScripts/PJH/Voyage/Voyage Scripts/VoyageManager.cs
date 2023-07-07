using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;



public class VoyageManager : MonoBehaviour
{
    public float VoyageTime;
    [SerializeField] private GameObject[] Shellfishes;
    [SerializeField] private GameObject[] Helps;
    [SerializeField] private Slider VoyageBar;
    [SerializeField] private GameObject Boat;
    [SerializeField] private GameObject VoyagePopUp;

    private float StartTime;
    private Shellfish ShellfishItem;
    private bool isReturn = false;

    private System.Random randomObj = new System.Random();

    private void Start()
    {
        StartTime = Time.time;
        VoyageBar.maxValue = VoyageTime;
        VoyageBar.value = VoyageTime;
        InvokeRepeating("CreateShellfish", 10.0f, 20.0f);
    }

    private void CreateShellfish()
    {
        int index = randomObj.Next(3);
        ShellfishItem = Shellfishes[index].GetComponent<Shellfish>();
        if (!ShellfishItem.isActiveAndEnabled)
        {
            Debug.Log("Shellfish 생성!");
            ShellfishItem.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (VoyageTime > 0.0f)
        {
            VoyageTime -= (Time.deltaTime);
            VoyageBar.value = VoyageTime;
            if ((VoyageTime <= VoyageTime / 2) && !isReturn)
            {
                Boat.transform.Rotate(0, 180, 0);
                isReturn = true;
            }
        }
        else
        {
            VoyageEnd();
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

        Debug.Log("항해 종료");
        VoyagePopUp.SetActive(true);

    }
}
