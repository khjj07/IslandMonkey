using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableHole : MonoBehaviour, IPointerClickHandler
{
    public RectTransform holeArea; // 클릭을 통과시킬 영역

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick called.");

        if (IsInsideHole(eventData.position))
        {
            Debug.Log("Click inside the hole.");
            // 클릭을 통과시키려면 아무 것도 하지 않습니다.
            PassEventThrough(eventData);
        }
        else
        {
            Debug.Log("Click outside the hole.");
            // 구멍 바깥을 클릭했을 때의 동작. 예를 들면 아무 반응이 없도록 할 수 있습니다.
            // 이 부분에 원하는 로직을 추가하세요.
        }
    }

    private bool IsInsideHole(Vector2 position)
    {
        bool isInside = RectTransformUtility.RectangleContainsScreenPoint(holeArea, position);
        Debug.Log($"IsInsideHole: {isInside}");
        return isInside;
    }

    private void PassEventThrough(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        Debug.Log($"Raycast results count: {results.Count}");

        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject == gameObject)
            {
                Debug.Log("Skipping self in raycast results.");
                continue;
            }

            Debug.Log($"Passing event to {results[i].gameObject.name}.");
            ExecuteEvents.Execute(results[i].gameObject, eventData, ExecuteEvents.pointerClickHandler);
            break;
        }
    }
}