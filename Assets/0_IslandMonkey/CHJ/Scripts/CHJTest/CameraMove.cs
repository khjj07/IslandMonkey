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

        // ���콺 �巡�׸� ���� ī�޶� �̵�
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ =>
            {
                Vector3 pos = transform.position;

                pos.x -= Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
                pos.y -= Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

                // ī�޶� �̵� ���� ����
                pos.x = Mathf.Clamp(pos.x, minX, maxX);
                pos.y = Mathf.Clamp(pos.y, minY, maxY);

                transform.position = pos;
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
                float newSize = cam.orthographicSize - (zoomInput * zoomSpeed);
                newSize = Mathf.Max(newSize, 1f);
                newSize = Mathf.Min(newSize, 6f);
                cam.orthographicSize = newSize;
            })
            .AddTo(this);
    }
}
