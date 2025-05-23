using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
public class DistortSphere : MonoBehaviour
{
    [Header("Large Scale Deformation")]
    public float largeDeformationScale = 1.5f;     // 대규모 변형의 노이즈 스케일
    public float largeDeformationStrength = 0.6f;  // 대규모 변형의 강도
    public bool usePseudo3DLarge = true;           // 대규모 변형에 Pseudo-3D 노이즈 사용

    [Header("Small Scale Detail")]
    public float smallDetailScale = 7f;           // 소규모 변형의 노이즈 스케일
    public float smallDetailStrength = 0.15f;     // 소규모 변형의 강도
    public bool usePseudo3DSmall = true;          // 소규모 변형에 Pseudo-3D 노이즈 사용

    // 각 노이즈 샘플링 시 사용할 임의의 시드 오프셋 값들 (결과물의 다양성을 위해 사용)
    // 이 값들을 변경하면 같은 설정에서도 다른 모양의 왜곡을 얻을 수 있습니다.
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
        MeshCollider mc = GetComponent<MeshCollider>(); // MeshCollider도 있다면 가져옵니다.
        Mesh originalMesh = mf.mesh;

        // 원본 메쉬 변경을 방지하기 위해 복사본 생성
        Mesh distortedMesh = Instantiate(originalMesh);
        Vector3[] vertices = distortedMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 originalPos = vertices[i];
            // 구의 중심이 (0,0,0)이라고 가정하고, 정점 위치를 정규화하여 변위 방향으로 사용
            Vector3 normalDirection = originalPos.normalized;

            Vector3 totalDisplacement = Vector3.zero;

            // --- 1. 큰 규모의 변형 ---
            if (largeDeformationStrength > 0)
            {
                float noiseValueLarge;
                if (usePseudo3DLarge)
                {
                    // Pseudo-3D: 서로 다른 평면에서 2D Perlin 노이즈를 3번 샘플링하여 평균
                    // PerlinNoise 결과(0~1)를 (-0.5~0.5)로 바꾸고 2를 곱해 (-1~1) 범위로 만듦
                    float pNoiseL1 = (Mathf.PerlinNoise(originalPos.x * largeDeformationScale + seedOffsetLarge1, originalPos.y * largeDeformationScale + seedOffsetLarge1 + 5.0f) - 0.5f) * 2f;
                    float pNoiseL2 = (Mathf.PerlinNoise(originalPos.y * largeDeformationScale + seedOffsetLarge2, originalPos.z * largeDeformationScale + seedOffsetLarge2 + 5.0f) - 0.5f) * 2f;
                    float pNoiseL3 = (Mathf.PerlinNoise(originalPos.z * largeDeformationScale + seedOffsetLarge3, originalPos.x * largeDeformationScale + seedOffsetLarge3 + 5.0f) - 0.5f) * 2f;
                    noiseValueLarge = (pNoiseL1 + pNoiseL2 + pNoiseL3) / 3f;
                }
                else
                {
                    // 기본 2D Perlin 노이즈 사용
                    noiseValueLarge = (Mathf.PerlinNoise(originalPos.x * largeDeformationScale + seedOffsetLarge1, originalPos.y * largeDeformationScale + seedOffsetLarge1 + 5.0f) - 0.5f) * 2f;
                }
                totalDisplacement += normalDirection * noiseValueLarge * largeDeformationStrength;
            }

            // --- 2. 작은 규모의 표면 디테일 ---
            if (smallDetailStrength > 0)
            {
                float noiseValueSmall;
                if (usePseudo3DSmall)
                {
                    // Pseudo-3D 노이즈 사용 (큰 규모와 다른 시드 오프셋 사용)
                    float pNoiseS1 = (Mathf.PerlinNoise(originalPos.x * smallDetailScale + seedOffsetSmall1, originalPos.y * smallDetailScale + seedOffsetSmall1 + 5.0f) - 0.5f) * 2f;
                    float pNoiseS2 = (Mathf.PerlinNoise(originalPos.y * smallDetailScale + seedOffsetSmall2, originalPos.z * smallDetailScale + seedOffsetSmall2 + 5.0f) - 0.5f) * 2f;
                    float pNoiseS3 = (Mathf.PerlinNoise(originalPos.z * smallDetailScale + seedOffsetSmall3, originalPos.x * smallDetailScale + seedOffsetSmall3 + 5.0f) - 0.5f) * 2f;
                    noiseValueSmall = (pNoiseS1 + pNoiseS2 + pNoiseS3) / 3f;
                }
                else
                {
                    // 기본 2D Perlin 노이즈 사용
                    noiseValueSmall = (Mathf.PerlinNoise(originalPos.x * smallDetailScale + seedOffsetSmall1, originalPos.y * smallDetailScale + seedOffsetSmall1 + 5.0f) - 0.5f) * 2f;
                }
                totalDisplacement += normalDirection * noiseValueSmall * smallDetailStrength;
            }

            vertices[i] = originalPos + totalDisplacement;
        }

        distortedMesh.vertices = vertices;
        distortedMesh.RecalculateNormals(); // 법선 벡터 재계산 (조명에 중요)
        distortedMesh.RecalculateBounds();  // 경계 상자 재계산 (컬링 등 최적화에 중요)

        mf.mesh = distortedMesh;

        // MeshCollider가 있다면, 충돌 메쉬도 업데이트
        if (mc != null)
        {
            mc.sharedMesh = distortedMesh;
        }

        rotationState = (float)(Math.Pow(gameObject.transform.position.magnitude, 2) % 180);

        gameObject.transform.Rotate(Vector3.up, rotationState);
        gameObject.transform.Rotate(Vector3.right, rotationState);
    }
}