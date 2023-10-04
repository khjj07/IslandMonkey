using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableHole : MonoBehaviour, IPointerClickHandler
{
    public RectTransform holeArea; // Ŭ���� �����ų ����

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsInsideHole(eventData.position))
        {
            // Ŭ���� �����Ű���� �ƹ� �͵� ���� �ʽ��ϴ�.
            PassEventThrough(eventData);
        }
    }

    private bool IsInsideHole(Vector2 position)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(holeArea, position);
    }

    private void PassEventThrough(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject == gameObject) continue;
            ExecuteEvents.Execute(results[i].gameObject, eventData, ExecuteEvents.pointerClickHandler);
            break;
        }
    }
}