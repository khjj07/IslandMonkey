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

        // 스와이프
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

        // 줌
        this.UpdateAsObservable()
            .Where(_ => Input.touchCount > 1)
            .Subscribe(_ =>
            {
                // 터치 저장
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // 위치 저장
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // 거리 계산
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // 줌 땡기기
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // 카메라 사이즈
                float newSize = cam.orthographicSize + (deltaMagnitudeDiff * zoomSpeed);
                newSize = Mathf.Max(newSize, 1f);
                newSize = Mathf.Min(newSize, 6f);
                cam.orthographicSize = newSize;
            })
            .AddTo(this);
    }

    // 아웃라인 체크
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