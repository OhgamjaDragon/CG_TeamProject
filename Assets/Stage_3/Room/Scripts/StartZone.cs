using UnityEngine;

public class StartZone : MonoBehaviour
{
    public GameObject respawner;

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 targetTag와 같은지 확인
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
