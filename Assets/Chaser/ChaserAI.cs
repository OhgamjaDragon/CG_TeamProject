using UnityEngine;
using UnityEngine.AI; // NavMesh Agent�� ����ϱ� ���� �ʼ�

public class ChaserAI : MonoBehaviour
{
    public string playerTag = "Player";
    private Camera playerCamera;

    private Transform playerTransform;
    private NavMeshAgent agent;
    private Renderer chaserRenderer; // �������� ������

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("ChaserAI: NavMeshAgent component not found on Chaser!", this);
            enabled = false;
            return;
        }

        chaserRenderer = GetComponent<Renderer>();
        if (chaserRenderer == null)
        {
            chaserRenderer = GetComponentInChildren<Renderer>();
            if (chaserRenderer == null)
            {
                Debug.LogError("ChaserAI: Renderer component not found on Chaser or its children!", this);
                enabled = false;
                return;
            }
        }

        // "MainCamera" �±׸� ���� ī�޶� ã��
        playerCamera = Camera.main;

        if (playerCamera == null)
        {
            Debug.LogError("ChaserAI: No Camera with the 'MainCamera' tag was found in the scene! Please tag your main camera.", this);
            enabled = false;
            return;
        }


        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else // playerCamera�� ã������ �÷��̾ ���� ���
        {
            Debug.LogError($"ChaserAI: Player with tag '{playerTag}' not found!", this);
            enabled = false;
            
        }
    }

    void Update()
    {
        if (playerTransform == null || !agent.isOnNavMesh || playerCamera == null || chaserRenderer == null)
        {
            if (agent != null && agent.isOnNavMesh && agent.isActiveAndEnabled)
            {
                agent.isStopped = true;
            }
            return;
        }

        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        bool isVisibleToPlayerCam = GeometryUtility.TestPlanesAABB(frustumPlanes, chaserRenderer.bounds);

        if (isVisibleToPlayerCam)
        {
            if (agent.isActiveAndEnabled) agent.isStopped = true;
        }
        else
        {
            if (agent.isActiveAndEnabled)
            {
                agent.isStopped = false;
                agent.SetDestination(playerTransform.position);
            }
        }
    }
}