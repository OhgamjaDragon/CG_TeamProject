// �÷��̾�� �������� �� �÷��̾ ������������ ��������. �߷�(GravityArea)�� ������ ����

using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public GameObject gravityAreaObject;
    private GravityArea gravityArea;
    public float mass = 0.5f;

    public GameObject respawner;

    private void Start()
    {
        // GravityArea ������Ʈ�� ����Ǿ����� Ȯ��
        if (gravityAreaObject == null)
        {
            Debug.LogError("GravityArea ������Ʈ�� ������� �ʾҽ��ϴ�!");
            enabled = false;
            return;
        }

        // GravityArea ������Ʈ���� GravityArea ��ũ��Ʈ ������Ʈ ��������
        gravityArea = gravityAreaObject.GetComponent<GravityArea>();
        if (gravityArea == null)
        {
            Debug.LogError("GravityArea ������Ʈ�� GravityArea ��ũ��Ʈ�� �����ϴ�!");
            enabled = false;
            return;
        }

        UpdatePullDistance();
    }
    private void OnTriggerEnter(Collider other) // trigger �ߵ����� ��
    {
        if (other != null)
        {
            if (other.tag == "Player")
            {
                // Player�� Sample ������Ʈ�� ������ �Լ�(RespawnPlayer()) ����
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
            // ���� ������� pullDistance ���
            float gravityAreaScale = (gravityArea.transform.localScale.x
                + gravityArea.transform.localScale.y
                + gravityArea.transform.localScale.z) / 3;
            float myScale = (transform.localScale.x
                + transform.localScale.y
                + transform.localScale.z) / 3;

            float calculatedPullDistance = mass * gravityAreaScale * myScale;

            // GravityArea ��ũ��Ʈ�� pullDistance �� ����
            gravityArea.SetPullDistance(calculatedPullDistance);

        }
    }
}
