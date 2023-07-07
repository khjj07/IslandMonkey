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
        // 마우스 왼쪽 버튼 클릭 시 Raycast를 수행합니다.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Building 태그를 가진 오브젝트를 감지하면 패널을 활성화합니다.
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Building"))
            {
                UpgradeBuildingPanel.SetActive(true);
                UpgradebulidingPanelOpened = true;
            }
        }
    }

    public void OnClickBuildingBtn()
    {
        //BuildingPanel.transform.localPosition = Vector3.zero;   // recttransform 쓰기
        BuildingPanel.SetActive(true);
        bulidingPanelOpened = true;
    }
    public void OnClickUpgradeBuildingBtn()
    {
        //BuildingPanel.transform.localPosition = Vector3.zero;   // recttransform 쓰기
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
