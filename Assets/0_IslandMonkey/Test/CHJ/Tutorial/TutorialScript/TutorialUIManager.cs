using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIManager : MonoBehaviour
{
    [SerializeField] private Button constructionUIBtn;
    [SerializeField] private Button facility0BuildBtn;

    [SerializeField]
    private GameObject MyRoomPanel;
    [SerializeField]
    private GameObject PopUpVoyageStartPanel;

    [SerializeField] private GameObject RadialCircle5;
    [SerializeField] private GameObject RadialCircle6;
    [SerializeField] private GameObject RadialCircle7;

    private bool isStep5Completed = false;
    private bool isStep6Completed = false;

    private void Start()
    {
        // 버튼에 클릭 이벤트 연결
        constructionUIBtn.onClick.AddListener(OnConstructionUIClicked);
        facility0BuildBtn.onClick.AddListener(OnPopupUIClicked);

        // 튜토리얼 플로우 시작
        StartCoroutine(TutorialFlow());

    }
    IEnumerator TutorialFlow()
    {
        yield return StartCoroutine(Step5());
        yield return StartCoroutine(Step6());
        // Step7, Step8도 동일하게 연결...
    }
    IEnumerator Step5()
    {
        RadialCircle5.SetActive(true);
        while (!isStep5Completed)
        {
            yield return null;
        }
        // 다음 단계로 이동하기 전 추가적인 작업 (예: UI 숨기기 등)
    }

    IEnumerator Step6()
    {
        RadialCircle5.SetActive(false);
        RadialCircle6.SetActive(true);
        while (!isStep6Completed)
        {
            yield return null;
        }
        // 다음 단계로 이동하기 전 추가적인 작업 (예: UI 숨기기 등)
    }

    public void Step6Function()
    {
        RadialCircle5.SetActive(false);
        RadialCircle6.SetActive(true);
    }

    public void OnConstructionUIClicked()
    {
        isStep5Completed = true;
    }

    public void OnPopupUIClicked()
    {
        isStep6Completed = true;
    }

    //#4~#5

    //#5~#6

    //#6~#7
    public void PopUpMyRoom()
    {
        RadialCircle6.SetActive(false);
        RadialCircle7.SetActive(true);
        MyRoomPanel.SetActive(true);
    }

    //#7~#8
    public void PopUpVoyageStart()
    {
        RadialCircle7.SetActive(false);
        MyRoomPanel.SetActive(false);

        PopUpVoyageStartPanel.SetActive(true);
    }
    //#8~#9
    public void GoToTutorialVoyageScene()
    {
        PopUpVoyageStartPanel.SetActive(false);
        // 튜토리얼 항해 씬으로 가기
    }

   
}
