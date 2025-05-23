using UnityEngine;

public class NoneGravityZone : MonoBehaviour
{
    
    // player가 영역에 들어오면 작동
    public void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 targetTag와 같은지 확인
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
