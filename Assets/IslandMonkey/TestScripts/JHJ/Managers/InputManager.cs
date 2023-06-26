using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject BuildingPanel;

    public static bool bulidingPanelOpened;
    public Vector3 scrollRectStart;

    private void Start()
    {
        bulidingPanelOpened = false;
    }

    public void OnClickBuildingBtn()
    {
        BuildingPanel.transform.localPosition = Vector3.zero;   // recttransform ¾²±â
        //BuildingPanel.SetActive(true);
        bulidingPanelOpened = true;
    }
    public void OnClickStoreBtn()
    {

    }
    public void OnClickVoyageBtn()
    {

    }
    public void OnClickAbroadBtn()
    {

    }

    public void BuildingPanelBackBtn()
    {
        //BuildingPanel.SetActive(false);
        BuildingPanel.transform.localPosition = Vector3.left * 1500;
        bulidingPanelOpened = false;
    }
}
