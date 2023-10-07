using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TutorialManager : MonoBehaviour
{
    public CameraMove myScriptComponent; // �ν����Ϳ��� �巡���Ͽ� �Ҵ�

    public float moveDuration = 2.0f; // �̵��� �ɸ��� �ð� (�� ����)
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float startTime;

    // ���� �� Ʃ�丮�� �÷ο� ����
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
