using UnityEngine;

public class Attack : MonoBehaviour
{
    public string playerTag = "Player";

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        if (collision.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Chaser�� Player�� �浹�߽��ϴ�!");

            Heart playerHeart = collision.gameObject.GetComponent<Heart>();

            if (playerHeart != null)
            {
                playerHeart.TakeDamage(1);
            }

        }
    }
}
