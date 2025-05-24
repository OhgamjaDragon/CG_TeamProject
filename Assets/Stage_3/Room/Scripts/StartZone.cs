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
            PlayerMovement pm = other.GetComponent<PlayerMovement>();

            if (timerRespawn != null && !pm.canUseKeyInput)
            {
                if (pm != null)
                {
                    pm.NoneGravityRoomStartSettings();
                }
                timerRespawn.RespawnPlayer();
            }
        }
    }
}
