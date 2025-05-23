using UnityEngine;

public class EndZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� targetTag�� ������ Ȯ��
        if ("Player" == other.tag)
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();

            // player�� ���߷� �������� �����ϸ� ��ŸƮ���� �۵�
            if (pm != null && !pm.canUseKeyInput)
            {
                pm.QuitNoneGravityRoom();
            }
        }
    }
}
