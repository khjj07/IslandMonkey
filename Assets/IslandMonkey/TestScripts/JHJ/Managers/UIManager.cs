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
    private GameObject buildingBtn; // Building ��ư ������Ʈ
    [SerializeField]
    private GameObject storeBtn; // Store ��ư ������Ʈ
    [SerializeField]
    private GameObject voyageBtn; // Voyage ��ư ������Ʈ

    [SerializeField]
    private GameObject voyageFacilityScreen;
    [SerializeField]
    private GameObject functialFacilityScreen;
    [SerializeField]
    private GameObject specialFacilityScreen;

    private ScreenState currentScreenState;

    private bool isRolledUp = false; // ��ư���� ������ �������� ���θ� ��Ÿ���� ����


    //���߿� ����
    public static int buildingLevel=1;
    [SerializeField]
    private TextMeshProUGUI _buildingLevelText; // TextMeshPro ������Ʈ�� �Ҵ���� ����


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
        isRolledUp = !isRolledUp; // ���¸� ������Ŵ

        // ��ư���� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
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