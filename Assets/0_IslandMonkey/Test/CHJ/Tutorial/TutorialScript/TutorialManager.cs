using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public CameraMove myScriptComponent; // 인스펙터에서 드래그하여 할당

    // 시작 시 튜토리얼 플로우 실행
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
        // MyScript를 비활성화
        //myScriptComponent.enabled = false;
        //Debug.Log("이게 되네");
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
