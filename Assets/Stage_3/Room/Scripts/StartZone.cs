using UnityEngine;

public class StartZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 targetTag와 같은지 확인
        if ("Player" == other.tag)
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();

            // player가 무중력 공간에서 돌아오면 리스폰 작동
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
