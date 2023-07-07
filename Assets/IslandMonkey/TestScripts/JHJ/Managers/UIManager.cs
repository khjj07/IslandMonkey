using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject BuildingPanel;
    [SerializeField]
    private GameObject UpgradeBuildingPanel;

    public static bool bulidingPanelOpened;
    public static bool UpgradebulidingPanelOpened;

    public float delayTime = 3;

    public Vector3 scrollRectStart;

    private void Start()
    {
        bulidingPanelOpened = false;
    }

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� �� Raycast�� �����մϴ�.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Building �±׸� ���� ������Ʈ�� �����ϸ� �г��� Ȱ��ȭ�մϴ�.
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Building"))
            {
                UpgradeBuildingPanel.SetActive(true);
                UpgradebulidingPanelOpened = true;
            }
        }
    }

    public void OnClickBuildingBtn()
    {
        //BuildingPanel.transform.localPosition = Vector3.zero;   // recttransform ����
        BuildingPanel.SetActive(true);
        bulidingPanelOpened = true;
    }
    public void OnClickUpgradeBuildingBtn()
    {
        //BuildingPanel.transform.localPosition = Vector3.zero;   // recttransform ����
        //UpgradeBuildingPanel.SetActive(true);
        //UpgradebulidingPanelOpened = true;
    }
    public void OnClickStoreBtn()
    {

    }
    public void OnClickVoyageBtn()
    {
        GameManager.instance.CreateBuilding();
        GameManager.instance.SaveGameManagerData();
        SceneManager.LoadScene("Voyage");
    }
    public void OnClickAbroadBtn()
    {

    }
    
    public void BuildingPanelBackBtn()
    {
        BuildingPanel.SetActive(false);
        //BuildingPanel.transform.localPosition = Vector3.left * 1500;
        bulidingPanelOpened = false;
    }
    public void UpgradeBuildingPanelBackBtn()
    {
        UpgradeBuildingPanel.SetActive(false);
        //BuildingPanel.transform.localPosition = Vector3.left * 1500;
        UpgradebulidingPanelOpened = false;
    }
    /*public void LoadVoyageScene()
    {
        SceneManager.LoadScene("Voyage");
    }*/
}
