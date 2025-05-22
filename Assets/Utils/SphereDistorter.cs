using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class SphereDistorter : MonoBehaviour
{
    public float noiseStrength = 0.3f;
    public float noiseScale = 3f;

    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshCollider mc = GetComponent<MeshCollider>();
        Mesh originalMesh = mf.mesh;

        // ���纻 ���� (���� ���� ����)
        Mesh distortedMesh = Instantiate(originalMesh);
        Vector3[] vertices = distortedMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = vertices[i];
            Vector3 normal = v.normalized;
            float noise = Mathf.PerlinNoise(v.x * noiseScale, v.y * noiseScale);
            vertices[i] = v + normal * noise * noiseStrength;
        }

        distortedMesh.vertices = vertices;
        distortedMesh.RecalculateNormals();
        distortedMesh.RecalculateBounds();

        mf.mesh = distortedMesh;
        mc.sharedMesh = distortedMesh; // �浹�� �ٲ� �޽ÿ� ����
    }
}