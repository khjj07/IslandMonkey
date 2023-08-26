using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityEntracne : MonoBehaviour
{
    public bool IsInMonkey { get { return isInMonkey; } set { isInMonkey = value; } }

    [SerializeField]
    private bool isInMonkey = false;

    public RuntimeAnimatorController MonkeyInBuildingAnimatorController; // 건물에 있을때 원숭이 Controller

    private void Awake()
    {
        isInMonkey = true;
    }

    public void changeIsInMonkey()
    {
        if (IsInMonkey) { isInMonkey = false; }
        else { isInMonkey = true; }
    }
}
