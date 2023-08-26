using System.Collections;
using Assets._0_IslandMonkey.CHJ.Scripts.Upgrade;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using Assets.IslandMonkey.Scripts.Managers;


public class UIManager : MonoBehaviour
{

    public enum UIState
    {
        basic,
        Voyage,
        Functional,
        Special

    }
        


    [SerializeField]
    private Image _settingPanel;

    [SerializeField]
    private GameObject _buildingGroup;


    [SerializeField] 
    private Image _upgradePanel;
    [SerializeField]
    private Image _upgradeBuildingFacility0Panel;
    [SerializeField]
    private Image _upgradeBuildingVoyage0Panel;
    [SerializeField]
    private Image _upgradeBuildingVoyage1Panel;
    [SerializeField]
    private Image _upgradeMonkeyPanel;

    [SerializeField]
    private Image _voyageSplash;

    public bool bulidingPanelOpened;
    public bool upgradebulidingPanelOpened;

    [SerializeField]
    private Button _buildingBtn; // Building 버튼 오브젝트
    [SerializeField]
    private Button _storeBtn; // Store 버튼 오브젝트
    [SerializeField]
    private Button _voyageBtn; // Voyage 버튼 오브젝트
    [SerializeField]
    private Button _manageBtn; // Voyage 버튼 오브젝트
    [SerializeField]
    private Button _rollupBtn; // Voyage 버튼 오브젝트
    private Quaternion initialRotation;



    [SerializeField]
    private Image _basicScreen;
    [SerializeField]
    private Image _voyageFacilityScreen;
    [SerializeField]
    private Image _functialFacilityScreen;
    [SerializeField]
    private Image _specialFacilityScreen;



    [SerializeField]
    private Image _manageScreen;



    private bool isRolledUp = false; // 버튼들이 숨겨진 상태인지 여부를 나타내는 변수


    //나중에 수정
    public static int buildingLevel=1;
    [SerializeField]
    private TextMeshProUGUI _buildingLevelText; // TextMeshPro 오브젝트를 할당받을 변수


    private void Start()
    {
        bulidingPanelOpened = false;
        initialRotation = _rollupBtn.transform.rotation;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 1f); 

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Ray hit: " + hit.collider.gameObject.name); 

                if (hit.collider.CompareTag("BuildingFacility0"))
                {
                    OnClickUpgradeBuildingBtn();
                    _upgradeBuildingFacility0Panel.gameObject.SetActive(true);
                    upgradebulidingPanelOpened = true;
                }
                if (hit.collider.CompareTag("BuildingVoyage0"))
                {
                    
                    _upgradeBuildingVoyage0Panel.gameObject.SetActive(true);
                    _upgradeMonkeyPanel.gameObject.SetActive(false);
                    upgradebulidingPanelOpened = true;
                }
                if (hit.collider.CompareTag("BuildingVoyage1"))
                {
                    
                    _upgradeBuildingVoyage1Panel.gameObject.SetActive(true);
                    _upgradeMonkeyPanel.gameObject.SetActive(false);
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
        SetFacilityScreen(UIState.Voyage);
    }

    public void OnClickUpgradeBuildingBtn()
    {
        _upgradeBuildingFacility0Panel.gameObject.SetActive(true);
        _upgradeMonkeyPanel.gameObject.SetActive(false);
    }

    public void OnClickStoreBtn()
    {
    }



    public void OnClickVoyageBtn()
    {
        StartCoroutine(TransitionToVoyageScene());
    }
    public void OnClickVoyageEx1Btn()
    {
        StartCoroutine(TransitionToVoyageSceneEx1());
    }
    public void OnClickVoyageEx2Btn()
    {
        StartCoroutine(TransitionToVoyageSceneEx2());
    }
    public void OnClickVoyageEx3Btn()
    {
        StartCoroutine(TransitionToVoyageSceneEx3());
    }






    public void OnClickAbroadBtn()
    {
    }


    public void OnClickBuildingUpgrade()
    {
        _upgradeBuildingFacility0Panel.gameObject.SetActive(true);
        _upgradeMonkeyPanel.gameObject.SetActive(false);
    }
    public void OnClickMonkeyUpgrade()
    {
        _upgradeBuildingFacility0Panel.gameObject.SetActive(false);
        _upgradeMonkeyPanel.gameObject.SetActive(true);
    }




    public void SetFacilityScreen(UIState screen)
    {
        _basicScreen.gameObject.SetActive(screen == UIState.basic);
        _voyageFacilityScreen.gameObject.SetActive(screen == UIState.Voyage);
        _functialFacilityScreen.gameObject.SetActive(screen == UIState.Functional);
        _specialFacilityScreen.gameObject.SetActive(screen == UIState.Special);
    }
    public void SetVoyageFacilityScreen()
    {
        SetFacilityScreen(UIState.Voyage);
    }
    public void SetFunctialFacilityScreen()
    {
        SetFacilityScreen(UIState.Functional);
    }
    public void SetSpecialFacilityScreen()
    {
        SetFacilityScreen(UIState.Special);
    }



    public void BuildingPanelBackBtn()
    {
        _buildingGroup.gameObject.SetActive(false);
        bulidingPanelOpened = false;
    }
    public void ManagePanelBackBtn()
    {
        _manageScreen.gameObject.SetActive(false);
        bulidingPanelOpened = false;
    }

    public void UpgradeBuildingPanelBackBtn()
    {
        //_upgradePanel.gameObject.SetActive(false);
        _upgradeBuildingFacility0Panel.gameObject.SetActive(false);
        _upgradeBuildingVoyage0Panel.gameObject.SetActive(false);
        _upgradeBuildingVoyage1Panel.gameObject.SetActive(false);
    }




    public void BuildingUpgradeBtn()
    {
        Building.instance.BuildingUpgrade();

    }
    public void MonkeyUpgradeBtn()
    {
        Monkey.instance.MonkeyUpgrade();

    }


    public void OnClickCookingScreen()
    {
        _manageScreen.gameObject.SetActive(true);
    }






    public void RollupBtn()
    {
        isRolledUp = !isRolledUp; // 상태를 반전시킴

        // 버튼들을 활성화 또는 비활성화
        _buildingBtn.gameObject.SetActive(!isRolledUp);
        _storeBtn.gameObject.SetActive(!isRolledUp);
        _voyageBtn.gameObject.SetActive(!isRolledUp);
        _manageBtn.gameObject.SetActive(!isRolledUp);

        RectTransform btnTransform = _rollupBtn.GetComponent<RectTransform>();
        // Y 위치 조절
        Vector2 currentPos = btnTransform.anchoredPosition;
        if (isRolledUp)
        {
            btnTransform.anchoredPosition = new Vector2(currentPos.x, currentPos.y - 250);
            btnTransform.localEulerAngles = new Vector3(0, 0, 180);
        }
        else
        {
            btnTransform.anchoredPosition = new Vector2(currentPos.x, currentPos.y + 250);
            btnTransform.localEulerAngles = Vector3.zero;
        }
    }
    private IEnumerator TransitionToVoyageScene()
    {

        _voyageSplash.gameObject.SetActive(true);
        GameManager.instance.CreateBuilding();
        GameManager.instance.SaveGameManagerData();
        // 2초 기다리기
        yield return new WaitForSeconds(2);

        // Voyage 씬 로드하기
        SceneManager.LoadScene("Voyage");
    }
    private IEnumerator TransitionToVoyageSceneEx1()
    {

        _voyageSplash.gameObject.SetActive(true);
        GameManager.instance.CreateBuildingEx1();
        GameManager.instance.SaveGameManagerData();
        // 2초 기다리기
        yield return new WaitForSeconds(2);

        // Voyage 씬 로드하기
        SceneManager.LoadScene("Voyage");
    }
    private IEnumerator TransitionToVoyageSceneEx2()
    {

        _voyageSplash.gameObject.SetActive(true);
        GameManager.instance.CreateBuildingEx2();
        GameManager.instance.SaveGameManagerData();
        // 2초 기다리기
        yield return new WaitForSeconds(2);

        // Voyage 씬 로드하기
        SceneManager.LoadScene("Voyage");
    }
    private IEnumerator TransitionToVoyageSceneEx3()
    {

        _voyageSplash.gameObject.SetActive(true);
        GameManager.instance.CreateBuildingEx3();
        GameManager.instance.SaveGameManagerData();
        // 2초 기다리기
        yield return new WaitForSeconds(2);

        // Voyage 씬 로드하기
        SceneManager.LoadScene("Voyage");
    }
}