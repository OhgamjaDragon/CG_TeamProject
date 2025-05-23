// �������� �ʴ� ���༺��, �߷� ���� �ε����� �� �÷��̾ ƨ�ܳ��⸸ �ϴ� ��ֹ�

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
