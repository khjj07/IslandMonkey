using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TutorialManager : MonoBehaviour
{
    public CameraMove myScriptComponent; // 인스펙터에서 드래그하여 할당

    public float moveDuration = 2.0f; // 이동에 걸리는 시간 (초 단위)
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float startTime;

    // 시작 시 튜토리얼 플로우 실행
    private void Start()
    {
        StartCoroutine(TutorialFlow());
        CameraMove10();
    }


    IEnumerator TutorialFlow()
    {
        yield return StartCoroutine(Step5());
        yield return StartCoroutine(Step6());
        yield return StartCoroutine(Step7());
        yield return StartCoroutine(Step8());
    }

    public void CameraMove10()
    {
        
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
