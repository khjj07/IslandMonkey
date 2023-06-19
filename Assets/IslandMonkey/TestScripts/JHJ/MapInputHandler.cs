using UnityEngine;


    public class MapInputHandler : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        [SerializeField] private SpriteRenderer mapRenderer;

        private float mapMinY, mapMaxY;

        private Vector3 dragOrigin;

        private void Awake()
        {
            mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
            mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
        }

        void Update()
        {
            
                PanCamera();
        }

        private void PanCamera()
        {
            if (Input.GetMouseButtonDown(0))
                dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButton(0))
            {
                Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
                cam.transform.position = ClampCamera(cam.transform.position + difference);
            }
        }

        private Vector3 ClampCamera(Vector3 targetPosition)
        {
            float camHeight = cam.orthographicSize;

            float minY = mapMinY + camHeight;
            float maxY = mapMaxY - camHeight;

            float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

            return new Vector3(0, newY, targetPosition.z);
        }
    }
