using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Help : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Help succeeded!"); // ���߿� gamamanager shellfish ��ɰ� ���� �ʿ�
    }
}
