using UnityEngine;

public class Attack : MonoBehaviour
{
    public string playerTag = "Player";

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Chaser가 Player와 충돌했습니다!");

            Heart playerHeart = collision.gameObject.GetComponent<Heart>();

            if (playerHeart != null)
            {
                playerHeart.TakeDamage(1);
            }

        }
    }
}
