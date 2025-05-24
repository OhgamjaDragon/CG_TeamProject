using UnityEngine;

public class SaveZone : MonoBehaviour
{
    public TimerRespawn respawnManager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���𰡰� ����: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ ������");

            if (respawnManager != null)
            {
                string objectName = gameObject.name;
                char lastCharacter = objectName[objectName.Length - 1];
                respawnManager.UpdateRespawnPoint(transform, CharNumChangeIntNum(lastCharacter));
                Debug.Log("�� ������ ��ġ �����!");
            }
            else
            {
                Debug.LogWarning("respawnManager�� ������� �ʾҾ��!");
            }
        }

    }

    int CharNumChangeIntNum(char lastCharacter)
    {
        int num = lastCharacter - '0';
        return num;
    }
}