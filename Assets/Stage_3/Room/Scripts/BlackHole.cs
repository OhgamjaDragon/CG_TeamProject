// 플레이어와 접촉했을 때 플레이어를 시작지점으로 리스폰함. 중력(GravityArea)를 가지고 있음

using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public GameObject gravityAreaObject;
    private GravityArea gravityArea;
    public float mass = 0.5f;

    public GameObject respawner;

    private void Start()
    {
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

        UpdatePullDistance();
    }
    private void OnTriggerEnter(Collider other) // trigger 발동했을 때
    {
        if (other != null)
        {
            if (other.tag == "Player")
            {
                // Player의 Sample 컴포넌트에 리스폰 함수(RespawnPlayer()) 존재
                TimerRespawn timerRespawn = respawner.GetComponent<TimerRespawn>();

                if (timerRespawn != null)
                {
                    Rigidbody rb = other.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = Vector3.zero; // Fix: Set velocity to Vector3.zero instead of assigning an int
                    }
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
}
