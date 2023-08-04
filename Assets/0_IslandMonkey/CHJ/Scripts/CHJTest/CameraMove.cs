using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxY = 11f;
    [SerializeField] private float minY = 5f;
    [SerializeField] private float moveScale = 1f;
    [SerializeField] private float zoomSpeed = 50f;

    private Camera cam;


    [SerializeField] private float panSpeed = 20f;

    private void Start()
    {
        cam = GetComponent<Camera>();

        // 마우스 드래그를 통한 카메라 이동
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ =>
            {
                Vector3 pos = transform.position;

                pos.x -= Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
                pos.y -= Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

                // 카메라 이동 범위 제한
                pos.x = Mathf.Clamp(pos.x, minX, maxX);
                pos.y = Mathf.Clamp(pos.y, minY, maxY);

                transform.position = pos;
            })
            .AddTo(this);

        // 줌
        this.UpdateAsObservable()
            .Where(_ => Input.touchCount > 1 || Input.mouseScrollDelta.y != 0f) // 터치 또는 마우스 휠 입력 확인
            .Subscribe(_ =>
            {
                float zoomInput = Input.mouseScrollDelta.y; // 마우스 휠 입력 값
                if (Input.touchCount > 1)
                {
                    // 터치 입력 처리
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
                    zoomInput = prevTouchDeltaMag - touchDeltaMag;
                }

                // 카메라 사이즈
                float newSize = cam.orthographicSize - (zoomInput * zoomSpeed);
                newSize = Mathf.Max(newSize, 1f);
                newSize = Mathf.Min(newSize, 6f);
                cam.orthographicSize = newSize;
            })
            .AddTo(this);
    }
}
