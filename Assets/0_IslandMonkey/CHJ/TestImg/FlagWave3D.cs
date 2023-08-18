using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FlagWave3D : MonoBehaviour
{
    public float waveSpeed = 1.0f;
    public float waveHeight = 0.5f;
    public float waveLength = 2.0f;

    private Mesh mesh;
    private Vector3[] initialVertices;
    private Vector3[] currentVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        initialVertices = mesh.vertices;
        currentVertices = new Vector3[initialVertices.Length];
    }

    void Update()
    {
        for (int i = 0; i < initialVertices.Length; i++)
        {
            Vector3 vertex = initialVertices[i];
            vertex.y += Mathf.Sin(Time.time * waveSpeed + initialVertices[i].x * waveLength) * waveHeight;
            currentVertices[i] = vertex;
        }

        mesh.vertices = currentVertices;
        mesh.RecalculateNormals();
    }
}