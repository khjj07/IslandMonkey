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
    }

    void Update()
    {
        if (VoyageBar.value > 0.0f)
        {
            VoyageBar.value -= (Time.time );

        }
        if ((VoyageBar.value <= VoyageBar.maxValue / 2) && !isReturn)
        {
            Boat.transform.rotation = Quaternion.Euler(0, 180, 0);
            isReturn = true;
        }

        if (VoyageBar.value % 60.0f == 0.0f)
        {
            int index = randomObj.Next(3);
            ShellfishItem = Shellfishes[index].GetComponent<Shellfish>();
            if (!ShellfishItem.isActiveAndEnabled)
            {
                ShellfishItem.gameObject.SetActive(true);
            }
        }
    }
}
