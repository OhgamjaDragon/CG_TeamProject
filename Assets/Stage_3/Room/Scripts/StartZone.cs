using UnityEngine;

public class StartZone : MonoBehaviour
{
    public GameObject respawner;

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� targetTag�� ������ Ȯ��
        if ("Player" == other.tag)
        {
            TimerRespawn timerRespawn = respawner.GetComponent<TimerRespawn>();

            if (timerRespawn != null)
            {
                PlayerMovement pm = other.GetComponent<PlayerMovement>();
                if (pm != null)
                {
                    pm.NoneGravityRoomStartSettings();
                }
                timerRespawn.RespawnPlayer();
            }
        }
    }
}
