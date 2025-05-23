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
                respawnManager.UpdateRespawnPoint(transform);
                Debug.Log("�� ������ ��ġ �����!");
            }
            else
            {
                Debug.LogWarning("respawnManager�� ������� �ʾҾ��!");
            }
        }

    }
}