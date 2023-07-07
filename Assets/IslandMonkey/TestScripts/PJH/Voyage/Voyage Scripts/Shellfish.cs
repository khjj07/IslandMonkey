using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shellfish : MonoBehaviour, IPointerClickHandler
{
    private void Start()
    {
        StartCoroutine("DestroyShellfish");
    }

    IEnumerator DestroyShellfish()
    {
        yield return new WaitForSeconds(5.0f); // 10�� ���
        Debug.Log("Shellfish �����!");
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Obtain Shellfish!"); // ���߿� gamamanager shellfish ��ɰ� ���� �ʿ�
        GameManager._totalShell += 50;
        Debug.Log(GameManager._totalShell);
        gameObject.SetActive(false);
    }


}
