using UnityEngine;

public class Wall : MonoBehaviour
{
    private float normalForce = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null) {
            if (collision.gameObject.tag == "Player")
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
                if (rb != null) {
                    Vector3 enterVector = player.fixedCurrentSpeed;
                    Vector3 standardVector = transform.up;
                    print(enterVector);
                    print(standardVector);
                    Vector3 outVector = GetReflectedVector(ref enterVector, ref standardVector);

                    rb.linearVelocity = outVector * normalForce;
                    print(outVector);
                }
                
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
