using UnityEngine;

public class StartZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� targetTag�� ������ Ȯ��
        if ("Player" == other.tag)
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();

            // player�� ���߷� �������� ���ƿ��� ������ �۵�
            if (pm != null && !pm.canUseKeyInput)
            {
                pm.RespawnPlayer();
            } else if (pm != null)
            {
                pm.blackHoleRespawnPosition = gameObject.transform.position;
            }
        }
    }
}
