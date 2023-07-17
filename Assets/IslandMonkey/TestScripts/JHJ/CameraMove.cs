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
            .Where(_ => Input.touchCount > 0)
            .Subscribe(_ =>
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    this.hold = true;
                    this.startPos = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    this.hold = false;
                }
                if (this.hold)
                {
                    this.curPos = touch.position;
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
            .Where(_ => Input.touchCount > 1 || Input.mouseScrollDelta.y != 0f) // ��ġ �Ǵ� ���콺 �� �Է� Ȯ��
            .Subscribe(_ =>
            {
                float zoomInput = Input.mouseScrollDelta.y; // ���콺 �� �Է� ��
                if (Input.touchCount > 1)
                {
                    // ��ġ �Է� ó��
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
                    zoomInput = prevTouchDeltaMag - touchDeltaMag;
                }

                // ī�޶� ������
                float newSize = cam.orthographicSize + (zoomInput * zoomSpeed);
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