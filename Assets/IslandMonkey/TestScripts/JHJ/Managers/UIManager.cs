using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ScreenState
{
    VoyageFacilityScreen,
    FunctialFacilityScreen,
    SpecialFacilityScreen
}

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject SettingPanel;


    [SerializeField]
    private GameObject BuildingPanel;
    [SerializeField]
    private GameObject UpgradeBuildingPanel;
    [SerializeField]
    private GameObject voyageSplash;

    public static bool bulidingPanelOpened;
    public static bool UpgradebulidingPanelOpened;

    [SerializeField]
    private GameObject buildingBtn; // Building 버튼 오브젝트
    [SerializeField]
    private GameObject storeBtn; // Store 버튼 오브젝트
    [SerializeField]
    private GameObject voyageBtn; // Voyage 버튼 오브젝트

    [SerializeField]
    private GameObject voyageFacilityScreen;
    [SerializeField]
    private GameObject functialFacilityScreen;
    [SerializeField]
    private GameObject specialFacilityScreen;

    private ScreenState currentScreenState;

    private bool isRolledUp = false; // 버튼들이 숨겨진 상태인지 여부를 나타내는 변수


    //나중에 수정
    public static int buildingLevel=1;
    [SerializeField]
    private TextMeshProUGUI _buildingLevelText; // TextMeshPro 오브젝트를 할당받을 변수


    private void Start()
    {
        bulidingPanelOpened = false;
       
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 1f); // Draw the ray for 1 second.

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Ray hit: " + hit.collider.gameObject.name); // Print the name of the hit object.

                if (hit.collider.CompareTag("Building"))
                {
                    UpgradeBuildingPanel.SetActive(true);
                    UpgradebulidingPanelOpened = true;
                }
            }
        }
    }
    public void OnClickSettingBtn()
    {
        SettingPanel.SetActive(true);
        
    }
    public void OnClickSettingBackBtn()
    {
        SettingPanel.SetActive(false);

    }
    public void OnClickBuildingBtn()
    {
         BuildingPanel.SetActive(true);
         bulidingPanelOpened = true;
         SetVoyageFacilityScreen();
         //SetScreenState(ScreenState.VoyageFacilityScreen);
    }

    public void OnClickUpgradeBuildingBtn()
    {
        UpgradeBuildingPanel.SetActive(true);
        UpgradebulidingPanelOpened = true;
    }

    public void OnClickStoreBtn()
    {
    }

    public void OnClickVoyageBtn()
    {
        StartCoroutine(TransitionToVoyageScene());
    }

    public void OnClickAbroadBtn()
    {
    }

    public void BuildingPanelBackBtn()
    {
        BuildingPanel.SetActive(false);
        bulidingPanelOpened = false;
    }

    public void UpgradeBuildingPanelBackBtn()
    {
        UpgradeBuildingPanel.SetActive(false);
        UpgradebulidingPanelOpened = false;
    }
    public void BuildingUpgradeBtn()
    {
        buildingLevel++;
        _buildingLevelText.text = " " + buildingLevel;
        
    }


    /* public void SetScreenState(ScreenState newState)
     {
         currentScreenState = newState;

         voyageFacilityScreen.SetActive(false);
         functialFacilityScreen.SetActive(false);
         specialFacilityScreen.SetActive(false);

         switch (currentScreenState)
         {
             case ScreenState.VoyageFacilityScreen:
                 voyageFacilityScreen.SetActive(true);
                 break;
             case ScreenState.FunctialFacilityScreen:
                 functialFacilityScreen.SetActive(true);
                 break;
             case ScreenState.SpecialFacilityScreen:
                 specialFacilityScreen.SetActive(true);
                 break;
             default:
                 Debug.LogError("Unrecognized screen state: " + currentScreenState);
                 break;
         }
     }*/

    public void SetVoyageFacilityScreen()
    {
        voyageFacilityScreen.SetActive(true);
        functialFacilityScreen.SetActive(false);
        specialFacilityScreen.SetActive(false);
    }
    public void SetFunctialFacilityScreen()
    {
        voyageFacilityScreen.SetActive(false);
        functialFacilityScreen.SetActive(true);
        specialFacilityScreen.SetActive(false);
    }
    public void SetSpecialFacilityScreen()
    {
        voyageFacilityScreen.SetActive(false);
        functialFacilityScreen.SetActive(false);
        specialFacilityScreen.SetActive(true);
    }


    public void RollupBtn()
    {
        isRolledUp = !isRolledUp; // 상태를 반전시킴

        // 버튼들을 활성화 또는 비활성화
        buildingBtn.SetActive(!isRolledUp);
        storeBtn.SetActive(!isRolledUp);
        voyageBtn.SetActive(!isRolledUp);
    }

    private IEnumerator TransitionToVoyageScene()
    {

        // Show the VoyageSplash game object.
        voyageSplash.SetActive(true);
        GameManager.instance.CreateBuilding();
        GameManager.instance.SaveGameManagerData();
        // Wait for 2 seconds.
        yield return new WaitForSeconds(2);

        // Load the Voyage scene.
        SceneManager.LoadScene("Voyage");
    }
}