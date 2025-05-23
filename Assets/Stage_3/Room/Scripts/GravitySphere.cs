using UnityEngine;

public class GravitySphere : MonoBehaviour
{
    public GameObject gravityAreaObject; // Inspector â���� GravityArea ������Ʈ�� �����մϴ�.
    public float mass = 1f; // GravitySphere�� ���� (Inspector â���� ���� ����)
    public float scaleFactor = 1f; // Scale ũ�⿡ ���� ����� ���� ���
    private Vector3 myPosition;
    public float bounceLevel = 3f;

    private GravityArea gravityArea;

    void Start()
    {
        myPosition = transform.position;

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

        // pullDistance �� ����
        UpdatePullDistance();
    }

    // �ʿ信 ���� Scale�� ����� �� pullDistance�� ������Ʈ�ϴ� �Լ�
    void Update()
    {
        // GravitySphere�� Scale�� ����Ǿ����� �����ϰ� ������Ʈ�� �� �ֽ��ϴ�.
        // ���� ���, �� �����Ӹ��� ���ϰų�, Scale ���� �̺�Ʈ�� �����ϴ� ��� ��
        if (transform.hasChanged)
        {
            UpdatePullDistance();
            transform.hasChanged = false; // ���� ���� �� �ʱ�ȭ
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

    Vector3 GetReflectedVector(ref Vector3 enter, ref Vector3 standard) // ������ ���� ���� ����, �ݻ�Ǵ� ǥ���� ���� ����
    {
        // �Ի� ���͸� ����ȭ�մϴ�.
        Vector3 normalizedIncident = enter.normalized;

        // ���� ���͸� ����ȭ�մϴ�.
        Vector3 normalizedNormal = standard.normalized;

        // �ݻ� ���͸� ����մϴ�.
        // ����: R = I - 2 * (I dot N) * N
        Vector3 reflected = normalizedIncident - 2 * Vector3.Dot(normalizedIncident, normalizedNormal) * normalizedNormal;

        return reflected.normalized; // �ݻ� ���͸� ����ȭ�Ͽ� ��ȯ�մϴ�.
    }

}

