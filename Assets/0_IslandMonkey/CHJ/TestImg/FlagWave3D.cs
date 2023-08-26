using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FlagWave3D : MonoBehaviour
{
    Mesh mesh;
    Vector3[] originalVertices;
    Vector3[] displacedVertices;

    public float waveSpeed = 1f;
    public float waveHeight = 0.1f;
    public float waveFrequency = 1f;
    public float rotationSpeed = 0.5f;

    private float accumulatedRotation = 0f; // Track the accumulated rotation

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        // Vertex movement along the z-axis based on the x position
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            vertex.z += waveHeight * Mathf.Sin(vertex.x * waveFrequency + Time.time * waveSpeed);
            displacedVertices[i] = vertex;
        }

        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();

        // Rotation around the y-axis using accumulated rotation
        accumulatedRotation += rotationSpeed * Time.deltaTime;
        float rotationAmount = 20f * Mathf.Sin(accumulatedRotation); // Rotates between -20 and 20

        transform.localEulerAngles = new Vector3(0, rotationAmount, 0);
    }

}