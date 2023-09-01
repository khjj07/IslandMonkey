using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float maxX = 5f;
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxZ = 11f;
    [SerializeField] private float minZ = 5f;
    [SerializeField] private float zoomSpeed = 10000000f;
    [SerializeField] private float panSpeed = 20f;

    private Camera camera;

    void OnDrawGizmos()
    {
        float height = transform.position.y;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(minX, height, maxZ), new Vector3(maxX, height, maxZ));
        Gizmos.DrawLine(new Vector3(maxX, height, maxZ), new Vector3(maxX, height, minZ));
        Gizmos.DrawLine(new Vector3(maxX, height, minZ), new Vector3(minX, height, minZ));
        Gizmos.DrawLine(new Vector3(minX, height, maxZ), new Vector3(minX, height,minZ));
  
    }

    private void Start()
    {
        camera = GetComponent<Camera>();

        // ���콺 �巡�׸� ���� ī�޶� �̵�
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ =>
            {
                Vector3 pos = transform.position;

                pos.x -= Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
                pos.z -= Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

                // ī�޶� �̵� ���� ����
                pos.x = Mathf.Clamp(pos.x, minX, maxX);
                pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

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

                // zoomInput ũ�⸦ ���� (��: 2�� ����)
                zoomInput = zoomInput * 2.0f;

                // ī�޶� ������
                float newSize = camera.orthographicSize - (zoomInput * zoomSpeed);
                newSize = Mathf.Max(newSize, 1f);
                newSize = Mathf.Min(newSize, 6f);
                camera.orthographicSize = newSize;
            })
            .AddTo(this);
    }
}
