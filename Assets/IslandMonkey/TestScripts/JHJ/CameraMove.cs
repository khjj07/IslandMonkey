using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CameraMove : MonoBehaviour
{

    [SerializeField] private float maxX = 5f;
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxY = 5f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float moveScale = 0.01f;
    [SerializeField] private float zoomSpeed = 0.01f;

    private Camera cam;
    private Vector2 moveScaleVector;
    private Vector2 startPos;
    private Vector2 curPos;
    private bool hold = false;

    private void Start()
    {
        cam = GetComponent<Camera>();
        moveScaleVector = new Vector2(moveScale, moveScale);

        // ��������
        this.UpdateAsObservable()
            .Where(_ => Input.touchCount == 1)
            .Subscribe(_ =>
            {
                if (Input.GetMouseButtonDown(0))
                {
                    this.hold = true;
                    this.startPos = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    this.hold = false;
                }
                if (this.hold)
                {
                    this.curPos = Input.mousePosition;
                    transform.Translate((cam.orthographicSize / 5) * moveScaleVector * (startPos - curPos));
                    if (Outside())
                    {
                        transform.Translate((cam.orthographicSize / 5) * moveScaleVector * (curPos - startPos));
                    }
                    startPos = curPos;
                }
            })
            .AddTo(this);

        // ��
        this.UpdateAsObservable()
            .Where(_ => Input.touchCount > 1)
            .Subscribe(_ =>
            {
                // ��ġ ����
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // ��ġ ����
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // �Ÿ� ���
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // �� �����
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // ī�޶� ������
                float newSize = cam.orthographicSize + (deltaMagnitudeDiff * zoomSpeed);
                newSize = Mathf.Max(newSize, 1f);
                newSize = Mathf.Min(newSize, 6f);
                cam.orthographicSize = newSize;
            })
            .AddTo(this);
    }

    // �ƿ����� üũ
    private bool Outside()
    {
        if (transform.position.x > maxX || transform.position.x < minX ||
            transform.position.y > maxY || transform.position.y < minY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}