using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;



public class VoyageManager : MonoBehaviour
{
    [SerializeField] private float VoyageTime;
    [SerializeField] private GameObject[] Shellfishes;
    [SerializeField] private GameObject[] Helps;
    [SerializeField] private Slider VoyageBar;
    [SerializeField] private GameObject Boat;

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
            Debug.Log("Shellfish »ý¼º!");
            ShellfishItem.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (VoyageBar.value > 0.0f)
        {
            VoyageBar.value -= (Time.deltaTime );
            if ((VoyageBar.value <= VoyageBar.maxValue / 2) && !isReturn)
            {
                Boat.transform.Rotate(0, 180, 0);
                isReturn = true;
            }
        }   
    }
}
