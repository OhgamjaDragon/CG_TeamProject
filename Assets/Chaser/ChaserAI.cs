using UnityEngine;
using UnityEngine.AI; // NavMesh Agent를 사용하기 위해 필수

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
