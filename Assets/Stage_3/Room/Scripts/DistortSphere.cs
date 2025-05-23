using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
public class DistortSphere : MonoBehaviour
{
    [Header("Large Scale Deformation")]
    public float largeDeformationScale = 1.5f;     // ��Ը� ������ ������ ������
    public float largeDeformationStrength = 0.6f;  // ��Ը� ������ ����
    public bool usePseudo3DLarge = true;           // ��Ը� ������ Pseudo-3D ������ ���

    [Header("Small Scale Detail")]
    public float smallDetailScale = 7f;           // �ұԸ� ������ ������ ������
    public float smallDetailStrength = 0.15f;     // �ұԸ� ������ ����
    public bool usePseudo3DSmall = true;          // �ұԸ� ������ Pseudo-3D ������ ���

    // �� ������ ���ø� �� ����� ������ �õ� ������ ���� (������� �پ缺�� ���� ���)
    // �� ������ �����ϸ� ���� ���������� �ٸ� ����� �ְ��� ���� �� �ֽ��ϴ�.
    private const float seedOffsetLarge1 = 10.0f;
    private const float seedOffsetLarge2 = 30.0f;
    private const float seedOffsetLarge3 = 50.0f;
    private const float seedOffsetSmall1 = 110.0f;
    private const float seedOffsetSmall2 = 130.0f;
    private const float seedOffsetSmall3 = 150.0f;

    //
    private float rotationState;

    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshCollider mc = GetComponent<MeshCollider>(); // MeshCollider�� �ִٸ� �����ɴϴ�.
        Mesh originalMesh = mf.mesh;

        // ���� �޽� ������ �����ϱ� ���� ���纻 ����
        Mesh distortedMesh = Instantiate(originalMesh);
        Vector3[] vertices = distortedMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 originalPos = vertices[i];
            // ���� �߽��� (0,0,0)�̶�� �����ϰ�, ���� ��ġ�� ����ȭ�Ͽ� ���� �������� ���
            Vector3 normalDirection = originalPos.normalized;

            Vector3 totalDisplacement = Vector3.zero;

            // --- 1. ū �Ը��� ���� ---
            if (largeDeformationStrength > 0)
            {
                float noiseValueLarge;
                if (usePseudo3DLarge)
                {
                    // Pseudo-3D: ���� �ٸ� ��鿡�� 2D Perlin ����� 3�� ���ø��Ͽ� ���
                    // PerlinNoise ���(0~1)�� (-0.5~0.5)�� �ٲٰ� 2�� ���� (-1~1) ������ ����
                    float pNoiseL1 = (Mathf.PerlinNoise(originalPos.x * largeDeformationScale + seedOffsetLarge1, originalPos.y * largeDeformationScale + seedOffsetLarge1 + 5.0f) - 0.5f) * 2f;
                    float pNoiseL2 = (Mathf.PerlinNoise(originalPos.y * largeDeformationScale + seedOffsetLarge2, originalPos.z * largeDeformationScale + seedOffsetLarge2 + 5.0f) - 0.5f) * 2f;
                    float pNoiseL3 = (Mathf.PerlinNoise(originalPos.z * largeDeformationScale + seedOffsetLarge3, originalPos.x * largeDeformationScale + seedOffsetLarge3 + 5.0f) - 0.5f) * 2f;
                    noiseValueLarge = (pNoiseL1 + pNoiseL2 + pNoiseL3) / 3f;
                }
                else
                {
                    // �⺻ 2D Perlin ������ ���
                    noiseValueLarge = (Mathf.PerlinNoise(originalPos.x * largeDeformationScale + seedOffsetLarge1, originalPos.y * largeDeformationScale + seedOffsetLarge1 + 5.0f) - 0.5f) * 2f;
                }
                totalDisplacement += normalDirection * noiseValueLarge * largeDeformationStrength;
            }

            // --- 2. ���� �Ը��� ǥ�� ������ ---
            if (smallDetailStrength > 0)
            {
                float noiseValueSmall;
                if (usePseudo3DSmall)
                {
                    // Pseudo-3D ������ ��� (ū �Ը�� �ٸ� �õ� ������ ���)
                    float pNoiseS1 = (Mathf.PerlinNoise(originalPos.x * smallDetailScale + seedOffsetSmall1, originalPos.y * smallDetailScale + seedOffsetSmall1 + 5.0f) - 0.5f) * 2f;
                    float pNoiseS2 = (Mathf.PerlinNoise(originalPos.y * smallDetailScale + seedOffsetSmall2, originalPos.z * smallDetailScale + seedOffsetSmall2 + 5.0f) - 0.5f) * 2f;
                    float pNoiseS3 = (Mathf.PerlinNoise(originalPos.z * smallDetailScale + seedOffsetSmall3, originalPos.x * smallDetailScale + seedOffsetSmall3 + 5.0f) - 0.5f) * 2f;
                    noiseValueSmall = (pNoiseS1 + pNoiseS2 + pNoiseS3) / 3f;
                }
                else
                {
                    // �⺻ 2D Perlin ������ ���
                    noiseValueSmall = (Mathf.PerlinNoise(originalPos.x * smallDetailScale + seedOffsetSmall1, originalPos.y * smallDetailScale + seedOffsetSmall1 + 5.0f) - 0.5f) * 2f;
                }
                totalDisplacement += normalDirection * noiseValueSmall * smallDetailStrength;
            }

            vertices[i] = originalPos + totalDisplacement;
        }

        distortedMesh.vertices = vertices;
        distortedMesh.RecalculateNormals(); // ���� ���� ���� (���� �߿�)
        distortedMesh.RecalculateBounds();  // ��� ���� ���� (�ø� �� ����ȭ�� �߿�)

        mf.mesh = distortedMesh;

        // MeshCollider�� �ִٸ�, �浹 �޽��� ������Ʈ
        if (mc != null)
        {
            mc.sharedMesh = distortedMesh;
        }

        rotationState = (float)(Math.Pow(gameObject.transform.position.magnitude, 2) % 180);

        gameObject.transform.Rotate(Vector3.up, rotationState);
        gameObject.transform.Rotate(Vector3.right, rotationState);
    }
}