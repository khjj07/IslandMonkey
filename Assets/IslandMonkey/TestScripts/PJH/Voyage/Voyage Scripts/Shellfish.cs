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
        yield return new WaitForSeconds(5.0f); // 10초 대기
        Debug.Log("Shellfish 사라짐!");
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Obtain Shellfish!"); // 나중에 gamamanager shellfish 기능과 연결 필요
        GameManager._totalShell += 50;
        Debug.Log(GameManager._totalShell);
        gameObject.SetActive(false);
    }


}
