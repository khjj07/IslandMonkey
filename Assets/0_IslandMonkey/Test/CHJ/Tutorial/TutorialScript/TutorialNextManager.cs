using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TutorialNextManager : MonoBehaviour
{
    [SerializeField] private Button Step11Btn;
    [SerializeField] private Button Step12Btn;
    [SerializeField] private Button Step13Btn;
    [SerializeField] private Button Step14Btn;
    [SerializeField] private Button Step15Btn;
    [SerializeField] private Button Step16Btn;
    [SerializeField] private Button Step17Btn;
    [SerializeField] private Button Step18Btn;
    [SerializeField] private Button step20Btn;

    [SerializeField]
    private GameObject VoyageEndPopupPanel;
    [SerializeField]
    private GameObject VoyageCheckPanel;
    [SerializeField]
    private GameObject BuildingUpgradePanel;
    [SerializeField]
    private GameObject BuildingUpgradeicon;
    [SerializeField]
    private GameObject DrawMachinePanel;
    [SerializeField]
    private GameObject DrawMachineCheckPanel;
    [SerializeField]
    private GameObject DrawMachineObject;
    [SerializeField]
    private GameObject SpecialPanel;
    [SerializeField]
    private GameObject pickPresentPanel;




    [SerializeField] private GameObject RadialCircle5;
    // 건설하기 footer UI #17
    [SerializeField] private GameObject RadialCircle11;
    // #11 
    [SerializeField] private GameObject RadialCircle12;
    // #12
    [SerializeField] private GameObject RadialCircle7;
    // #13
    [SerializeField] private GameObject RadialCircle14;
    //#14
    [SerializeField] private GameObject RadialCircle15;
    //#15
    [SerializeField] private GameObject RadialCircle16;
    //#16
    [SerializeField] private GameObject RadialCircle17;
    //#16


    private bool isStep11Completed = false;
    private bool isStep12Completed = false;
    private bool isStep13Completed = false;
    private bool isStep14Completed = false;
    private bool isStep15Completed = false;
    private bool isStep16Completed = false;
    private bool isStep17Completed = false;
    private bool isStep18Completed = false;

    public int BuildingLevel = 1;
    //public TextMeshPro BuildingLevelText;
    public int ExBuildingLevel;
    //public TextMeshPro ExBuildingLevelText;

    private void Start()
    {
        // 버튼에 클릭 이벤트 연결
        Step11Btn.onClick.AddListener(Step11Function);
        Step12Btn.onClick.AddListener(Step12Function);
        Step13Btn.onClick.AddListener(Step13Function);
        Step14Btn.onClick.AddListener(Step14Function);
        Step15Btn.onClick.AddListener(Step15Function);

        Step17Btn.onClick.AddListener(Step17Function);
        Step18Btn.onClick.AddListener(Step18Function);

        // 튜토리얼 플로우 시작
        StartCoroutine(TutorialFlow());

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("iconBuilding"))
                {
                    BuildingUpgradePanel.SetActive(true);
                    Step13Function();
                }
            }
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("iconPresent"))
                {
                    DrawMachinePanel.SetActive(true);
                    Step17Function();
                }
            }
        }
    }
    IEnumerator TutorialFlow()
    {
        yield return StartCoroutine(Step11());
        yield return StartCoroutine(Step12());
        yield return StartCoroutine(Step13());
        yield return StartCoroutine(Step14());
        yield return StartCoroutine(Step15());
        yield return StartCoroutine(Step16());
        yield return StartCoroutine(Step17());
        yield return StartCoroutine(Step18());
        // Step7, Step8도 동일하게 연결...
    }


    IEnumerator Step11()
    {
        VoyageEndPopupPanel.SetActive(true);
        RadialCircle11.SetActive(true);
        while (!isStep11Completed)
        {
            yield return null;
        }
        // 다음 단계로 이동하기 전 추가적인 작업 (예: UI 숨기기 등)
    }

    IEnumerator Step12()
    {
        Camera.main.transform.position = new Vector3(0.52f, 5.78f, -11f);
        VoyageEndPopupPanel.SetActive(false);
        VoyageCheckPanel.SetActive(true);
        RadialCircle11.SetActive(false);
        RadialCircle12.SetActive(true);
        while (!isStep12Completed)
        {
            yield return null;
        }
        // 다음 단계로 이동하기 전 추가적인 작업 (예: UI 숨기기 등)
    }
    // 아이콘 클릭
    IEnumerator Step13()
    {
        Camera.main.transform.position = new Vector3(0f, 5.5f, -11f);
        VoyageCheckPanel.SetActive(false);
        RadialCircle12.SetActive(false);
        
        BuildingUpgradeicon.SetActive(true);
        RadialCircle7.SetActive(true);
        while (!isStep13Completed)
        {
            yield return null;
        }

    }
    // 빌딩 패널 열림
    IEnumerator Step14()
    {
        BuildingUpgradeicon.SetActive(false);
        RadialCircle7.SetActive(false);

        RadialCircle14.SetActive(true);
        while (!isStep14Completed)
        { 
            yield return null;
        }

    }
    //빌딩 꺼지고 건설 푸터 팝업
    IEnumerator Step15()
    {
        RadialCircle14.SetActive(false);
        BuildingUpgradePanel.SetActive(false);

        RadialCircle15.SetActive(true);
        while (!isStep15Completed)
        {
            yield return null;
        }

    }
    // 뽑기창고 건설 버튼
    IEnumerator Step16()
    {
        RadialCircle15.SetActive(false);

        RadialCircle16.SetActive(true);
        while (!isStep16Completed)
        {
            yield return null;
        }

    }
    //뽑기창고 건설 체크
    IEnumerator Step17()
    {
        Camera.main.transform.position = new Vector3(-0.75f, 5.8f, -11f);

        RadialCircle16.SetActive(false);

        SpecialPanel.SetActive(false);
        DrawMachineCheckPanel.SetActive(true);
        RadialCircle17.SetActive(true);
        while (!isStep17Completed)
        {
            yield return null;
        }

    }
    // 뽑기창고 icon 나타남
    IEnumerator Step18()
    {
        RadialCircle17.SetActive(false);
        DrawMachineCheckPanel.SetActive(false);
        
        PopUpDrawMachine();
        Invoke("ActivatePanelMethod", 2f);
        while (!isStep18Completed)
        {
            RadialCircle7.SetActive(false);
            yield return null;
        }

    }
    //뽑기창고 팝업

    void ActivatePanelMethod()
    {
        pickPresentPanel.SetActive(true);
    }

    public void OnConstructionUIClicked()
    {
        isStep17Completed = true;
    }

    public void Step11Function()
    {
        isStep11Completed = true;
    }
    public void Step12Function()
    {
        isStep12Completed = true;
    }
    public void Step13Function()
    {
        isStep13Completed = true;
    }
    public void Step14Function()
    {
        isStep14Completed = true;
    }
    public void Step15Function()
    {
        isStep15Completed = true;
    }

    public void Step16Function()
    {
        isStep16Completed = true;
    }
    public void Step17Function()
    {
        isStep17Completed = true;
    }
    public void Step18Function()
    {
        isStep18Completed = true;
    }

    public void SetUpDrawMachine()
    {
        Camera.main.transform.position = new Vector3(-0.75f, 5.8f, -11f);
        DrawMachineObject.SetActive(true);
        SpecialPanel.SetActive(false);
        Step16Function();
    }
    public void PopUpDrawMachine()
    {
        DrawMachinePanel.SetActive(true);
    }


    //#10을 위한 UI 요소
    public void OnBuildingUpgradeButtonClick()
    {
        BuildingLevel += 1;
        ExBuildingLevel = BuildingLevel - 1;
    }

}
