using UnityEngine;

public class NoneGravityZone : MonoBehaviour
{
    
    // player�� ������ ������ �۵�
    public void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� targetTag�� ������ Ȯ��
        if ("Player" == other.tag)
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.InNoneGravity();
            }
        }
    }
    
}
