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
        yield return new WaitForSeconds(30.0f); // 30�� ���
        Debug.Log("Shellfish �����!");
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Obtain Shellfish!"); // ���߿� gamamanager shellfish ��ɰ� ���� �ʿ�
        gameObject.SetActive(false);
    }


}
