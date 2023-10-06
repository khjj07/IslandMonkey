using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public int BuildingLevel = 1;
    //public TextMeshPro BuildingLevelText;
    public int ExBuildingLevel;
    //public TextMeshPro ExBuildingLevelText;

    private void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ ����
        constructionUIBtn.onClick.AddListener(OnConstructionUIClicked);
        facility0BuildBtn.onClick.AddListener(OnPopupUIClicked);

        // Ʃ�丮�� �÷ο� ����
        StartCoroutine(TutorialFlow());

    }
    IEnumerator TutorialFlow()
    {
        yield return StartCoroutine(Step5());
        yield return StartCoroutine(Step6());
        // Step7, Step8�� �����ϰ� ����...
    }
    IEnumerator Step5()
    {
        RadialCircle5.SetActive(true);
        while (!isStep5Completed)
        {
            yield return null;
        }
        // ���� �ܰ�� �̵��ϱ� �� �߰����� �۾� (��: UI ����� ��)
    }

    IEnumerator Step6()
    {
        RadialCircle5.SetActive(false);
        RadialCircle6.SetActive(true);
        while (!isStep6Completed)
        {
            yield return null;
        }
        // ���� �ܰ�� �̵��ϱ� �� �߰����� �۾� (��: UI ����� ��)
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

        //#8~#9
        PopUpVoyageStartPanel.SetActive(true);
        StartCoroutine(HideAfterDelay());
        StartCoroutine(DelayedLoad());
    }
    
    /*public void GoToTutorialVoyageScene()
    {
        PopUpVoyageStartPanel.SetActive(false);
        
        // Ʃ�丮�� ���� ������ ����
    }*/

    private IEnumerator DelayedLoad()
    {
        yield return new WaitForSeconds(2f);  // 2�� ���
        SceneManager.LoadScene("Voyage");
    }
    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(2f);  // 2�� ���
        PopUpVoyageStartPanel.SetActive(false);
    }



    //#10�� ���� UI ���
    public void OnBuildingUpgradeButtonClick()
    {
        BuildingLevel += 1;
        ExBuildingLevel = BuildingLevel - 1;
    }
}
