using UnityEngine;

public class SaveZone : MonoBehaviour
{
    public TimerRespawn respawnManager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("무언가가 들어옴: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 들어왔음");

            if (respawnManager != null)
            {
                string objectName = gameObject.name;
                char lastCharacter = objectName[objectName.Length - 1];
                respawnManager.UpdateRespawnPoint(transform, CharNumChangeIntNum(lastCharacter));
                Debug.Log("새 리스폰 위치 저장됨!");
            }
            else
            {
                Debug.LogWarning("respawnManager가 연결되지 않았어요!");
            }
        }

    }

    int CharNumChangeIntNum(char lastCharacter)
    {
        int num = lastCharacter - '0';
        return num;
    }
}