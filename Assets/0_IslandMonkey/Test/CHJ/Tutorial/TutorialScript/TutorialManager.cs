using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public CameraMove myScriptComponent; // �ν����Ϳ��� �巡���Ͽ� �Ҵ�

    // ���� �� Ʃ�丮�� �÷ο� ����
    private void Start()
    {
        StartCoroutine(TutorialFlow());
    }

    IEnumerator TutorialFlow()
    {
        yield return StartCoroutine(Step5());
        yield return StartCoroutine(Step6());
        yield return StartCoroutine(Step7());
        yield return StartCoroutine(Step8());
    }

    void ActiveRadialWave()
    {
        // MyScript�� ��Ȱ��ȭ
        //myScriptComponent.enabled = false;
        //Debug.Log("�̰� �ǳ�");
    }

    IEnumerator Step5()
    {
        yield return null;
    }

    IEnumerator Step6()
    {
        yield return null;
    }

    IEnumerator Step7()
    {
        yield return null;
    }

    IEnumerator Step8()
    {
        yield return null;
    }


}
