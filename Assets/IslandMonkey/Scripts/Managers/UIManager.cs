using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
    //�����..������..
    //�׸��� ���⿡�� UI�� FSM�� ���°� ������.. A���� B���� C���¿� ���� UI ��ü ���°� �ٲ��
    //���� ���¸� ��ư ������ �ٲٴ°� ����Ұ� ����

    [SerializeField]
    private Image _settingPanel;

    [SerializeField]
    private GameObject _buildingGroup;

    [SerializeField]
    private Image _upgradeBuildingPanel;

    [SerializeField]
    private Image _voyageSplash;

    public bool bulidingPanelOpened;
    public bool upgradebulidingPanelOpened;

    [SerializeField]
    private Button _buildingBtn; // Building ��ư ������Ʈ
    [SerializeField]
    private Button _storeBtn; // Store ��ư ������Ʈ
    [SerializeField]
    private Button _voyageBtn; // Voyage ��ư ������Ʈ
    //��� Button ��ü�� �޾���..

    [SerializeField]
    private Image _voyageFacilityScreen;
    [SerializeField]
    private Image _functialFacilityScreen;
    [SerializeField]
    private Image _specialFacilityScreen;
    //�̰͵� ��� Panel ��ü��

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
                    _upgradeBuildingPanel.gameObject.SetActive(true);
                    upgradebulidingPanelOpened = true;
                }
            }
        }
    }
    public void OnClickSettingBtn()
    {
        _settingPanel.gameObject.SetActive(true);
        
    }
    public void OnClickSettingBackBtn()
    {
        _settingPanel.gameObject.SetActive(false);

    }
    public void OnClickBuildingBtn()
    {
         _buildingGroup.gameObject.SetActive(true);
         bulidingPanelOpened = true;
         SetVoyageFacilityScreen();
         //SetScreenState(ScreenState.VoyageFacilityScreen);
    }

    public void OnClickUpgradeBuildingBtn()
    {
        _upgradeBuildingPanel.gameObject.SetActive(true);
        upgradebulidingPanelOpened = true;
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
        _buildingGroup.gameObject.SetActive(false);
        bulidingPanelOpened = false;
    }

    public void UpgradeBuildingPanelBackBtn()
    {
        _upgradeBuildingPanel.gameObject.SetActive(false);
        upgradebulidingPanelOpened = false;
    }
    public void BuildingUpgradeBtn()
    {
        buildingLevel++;
        _buildingLevelText.text = " " + buildingLevel;
        
    }


    /* public void SetScreenState(ScreenState newState)
     {
         currentScreenState = newState;

         _voyageFacilityScreen.SetActive(false);
         _functialFacilityScreen.SetActive(false);
         _specialFacilityScreen.SetActive(false);

         switch (currentScreenState)
         {
             case ScreenState.VoyageFacilityScreen:
                 _voyageFacilityScreen.SetActive(true);
                 break;
             case ScreenState.FunctialFacilityScreen:
                 _functialFacilityScreen.SetActive(true);
                 break;
             case ScreenState.SpecialFacilityScreen:
                 _specialFacilityScreen.SetActive(true);
                 break;
             default:
                 Debug.LogError("Unrecognized screen state: " + currentScreenState);
                 break;
         }
     }*/

    public void SetVoyageFacilityScreen()
    {
        _voyageFacilityScreen.gameObject.SetActive(true);
        _functialFacilityScreen.gameObject.SetActive(false);
        _specialFacilityScreen.gameObject.SetActive(false);
    }
    public void SetFunctialFacilityScreen()
    {
        _voyageFacilityScreen.gameObject.SetActive(false);
        _functialFacilityScreen.gameObject.SetActive(true);
        _specialFacilityScreen.gameObject.SetActive(false);
    }
    public void SetSpecialFacilityScreen()
    {
        _voyageFacilityScreen.gameObject.SetActive(false);
        _functialFacilityScreen.gameObject.SetActive(false);
        _specialFacilityScreen.gameObject.SetActive(true);
    }


    public void RollupBtn()
    {
        isRolledUp = !isRolledUp; // ���¸� ������Ŵ

        // ��ư���� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
        _buildingBtn.gameObject.SetActive(!isRolledUp);
        _storeBtn.gameObject.SetActive(!isRolledUp);
        _voyageBtn.gameObject.SetActive(!isRolledUp);
    }

    private IEnumerator TransitionToVoyageScene()
    {

        // Show the VoyageSplash game object.
        _voyageSplash.gameObject.SetActive(true);
        GameManager.instance.CreateBuilding();
        GameManager.instance.SaveGameManagerData();
        // Wait for 2 seconds.
        yield return new WaitForSeconds(2);

        // Load the Voyage scene.
        SceneManager.LoadScene("Voyage");
    }
}