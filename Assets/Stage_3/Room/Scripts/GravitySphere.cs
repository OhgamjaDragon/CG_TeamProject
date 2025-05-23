using UnityEngine;

public class GravitySphere : MonoBehaviour
{
    public GameObject gravityAreaObject; // Inspector 창에서 GravityArea 오브젝트를 연결합니다.
    public float mass = 1f; // GravitySphere의 질량 (Inspector 창에서 조절 가능)
    public float scaleFactor = 1f; // Scale 크기에 대한 영향력 조절 계수
    private Vector3 myPosition;
    public float bounceLevel = 3f;

    private GravityArea gravityArea;

    void Start()
    {
        myPosition = transform.position;

        // GravityArea 오브젝트가 연결되었는지 확인
        if (gravityAreaObject == null)
        {
            Debug.LogError("GravityArea 오브젝트가 연결되지 않았습니다!");
            enabled = false;
            return;
        }

        // GravityArea 오브젝트에서 GravityArea 스크립트 컴포넌트 가져오기
        gravityArea = gravityAreaObject.GetComponent<GravityArea>();
        if (gravityArea == null)
        {
            Debug.LogError("GravityArea 오브젝트에 GravityArea 스크립트가 없습니다!");
            enabled = false;
            return;
        }

        // pullDistance 값 설정
        UpdatePullDistance();
    }

    // 필요에 따라 Scale이 변경될 때 pullDistance를 업데이트하는 함수
    void Update()
    {
        // GravitySphere의 Scale이 변경되었는지 감지하고 업데이트할 수 있습니다.
        // 예를 들어, 매 프레임마다 비교하거나, Scale 변경 이벤트를 구독하는 방식 등
        if (transform.hasChanged)
        {
            UpdatePullDistance();
            transform.hasChanged = false; // 변경 감지 후 초기화
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (rb != null) {
                Vector3 enterVector = player.fixedCurrentSpeed;
                Vector3 enterPosition = collision.transform.position;
                Vector3 objectsPositionVector = myPosition - enterPosition;
                Vector3 normalizedOutVector = GetReflectedVector(ref enterVector, ref objectsPositionVector);

                float enterSpeed = enterVector.magnitude;

                rb.linearVelocity = enterSpeed * normalizedOutVector * bounceLevel;
            }
        }
    }

    void UpdatePullDistance()
    {
        if (gravityArea != null)
        {
            // 질량 기반으로 pullDistance 계산
            float gravityAreaScale = (gravityArea.transform.localScale.x 
                + gravityArea.transform.localScale.y 
                + gravityArea.transform.localScale.z) / 3;
            float myScale = (transform.localScale.x 
                + transform.localScale.y 
                + transform.localScale.z) / 3;
            float calculatedPullDistance = mass * gravityAreaScale * myScale;

            // GravityArea 스크립트에 pullDistance 값 설정
            gravityArea.SetPullDistance(calculatedPullDistance);

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

