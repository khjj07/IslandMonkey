using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject BuildingPanel;
    public void OnClickBuildingBtn()
    {
        BuildingPanel.SetActive(true);
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

    public void BackBtn()
    {
        BuildingPanel.SetActive(false);
    }
}
