using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableHole : MonoBehaviour, IPointerClickHandler
{
    public RectTransform holeArea; // Ŭ���� �����ų ����

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick called.");

        if (IsInsideHole(eventData.position))
        {
            Debug.Log("Click inside the hole.");
            // Ŭ���� �����Ű���� �ƹ� �͵� ���� �ʽ��ϴ�.
            PassEventThrough(eventData);
        }
        else
        {
            Debug.Log("Click outside the hole.");
            // ���� �ٱ��� Ŭ������ ���� ����. ���� ��� �ƹ� ������ ������ �� �� �ֽ��ϴ�.
            // �� �κп� ���ϴ� ������ �߰��ϼ���.
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