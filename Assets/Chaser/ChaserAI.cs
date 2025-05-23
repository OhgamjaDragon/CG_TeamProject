using UnityEngine;
using UnityEngine.AI; // NavMesh Agent�� ����ϱ� ���� �ʼ�

public class ChaserAI : MonoBehaviour
{
    public string playerTag = "Player";
    private Transform playerTransform;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("ChaserAI: NavMeshAgent component not found!");
            enabled = false;
            return;
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError($"ChaserAI: Player with tag '{playerTag}' not found!");
            enabled = false;
        }
    }

    void Update()
    {
        if (playerTransform != null && agent.isOnNavMesh)
        {
            agent.SetDestination(playerTransform.position);
        }
    }
}
