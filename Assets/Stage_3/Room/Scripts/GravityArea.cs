using UnityEngine;

public class GravityArea : MonoBehaviour
{
    public float pullForce = 20f; // 끌어당기는 힘의 세기
    public float pullDistance; // 끌어당기는 최대 거리
    public string targetTag = "Player"; // 끌어당길 오브젝트의 태그

    private void OnTriggerStay(Collider other)
    {
        // 충돌한 오브젝트의 태그가 targetTag와 같은지 확인
        if (other.tag == targetTag)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            // Collider가 Rigidbody를 가지고 있고, 거리가 pullDistance 이하인 경우
            if (rb != null)
            {
                Vector3 directionToPull = transform.position - other.transform.position;
                float distance = directionToPull.magnitude;

                if (distance <= pullDistance && distance > 0f) // 거리가 pullDistance 이하이고 자기 자신은 제외
                {
                    // 힘의 크기는 거리에 따라 조절할 수 있습니다 (선택 사항)
                    float realPullForce = pullForce * (1f - (distance / pullDistance));
                    rb.AddForce(directionToPull.normalized * realPullForce, ForceMode.Force);
                }
            }
        }
    }

    public void SetPullDistance (float distance)
    {
        pullDistance = distance;
    }

    public void SetPullForce (float force) 
    {
        pullForce = force;
    }
}