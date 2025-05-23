// 움직이지 않는 소행성들, 중력 없고 부딪혔을 때 플레이어를 튕겨내기만 하는 장애물

using UnityEngine;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Vector3 myPosition;
    public float bounceLevel = 1f;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("player collision");
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (rb != null)
            {
                Vector3 enterVector = player.fixedCurrentSpeed;
                Vector3 enterPosition = collision.transform.position;
                Vector3 objectsPositionVector = myPosition - enterPosition;
                Vector3 normalizedOutVector = GetReflectedVector(ref enterVector, ref objectsPositionVector);

                float enterSpeed = enterVector.magnitude;

                print("bomb!");
                rb.linearVelocity = enterSpeed * normalizedOutVector * bounceLevel;
            }
        }
    }

    Vector3 GetReflectedVector(ref Vector3 enter, ref Vector3 standard) // 들어오는 빛의 방향 벡터, 반사되는 표면의 법선 벡터
    {
        // 입사 벡터를 정규화합니다.
        Vector3 normalizedIncident = enter.normalized;

        // 법선 벡터를 정규화합니다.
        Vector3 normalizedNormal = standard.normalized;

        // 반사 벡터를 계산합니다.
        // 공식: R = I - 2 * (I dot N) * N
        Vector3 reflected = normalizedIncident - 2 * Vector3.Dot(normalizedIncident, normalizedNormal) * normalizedNormal;

        return reflected.normalized; // 반사 벡터를 정규화하여 반환합니다.
    }
}
