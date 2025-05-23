using UnityEngine;

public class JetPack : MonoBehaviour
{
    public int basicFuel = 4;
    public int fuel;
    public GameObject playerObject;
    public float jetPower = 30f;

    public Camera firstPersonCam;
    public Camera thirdPersonCam;

    /*private float fovChange = 15f;
    private float fovIncreaseTime = 0.15f;
    private float fovHoldTime = 0.4f;
    private float fovDecreaseTime = 0.25f;

    private float originalFirstPersonFov;
    private float originalThirdPersonFov;
    private bool isShowingEffect;*/

    private void Start()
    {
        if (playerObject == null)
        {
            Debug.LogError("JetPack�� Object�� ������� �ʾҽ��ϴ�!");
            enabled = false;
            return;
        }
        if (firstPersonCam == null || thirdPersonCam == null)
        {
            Debug.LogError("JetPack�� Camera�� ������� �ʾҽ��ϴ�!");
            enabled = false;
            return;
        }
        fuel = 0;
        /*originalFirstPersonFov = firstPersonCam.fieldOfView;
        originalThirdPersonFov = thirdPersonCam.fieldOfView;*/
    }

    public void AddFuel()
    {
        fuel = basicFuel;
    }

    public void OperateJetPack()
    {
        Rigidbody rb = playerObject.GetComponent<Rigidbody>();
        CameraSwitcher cameraSwitcher = playerObject.GetComponent<CameraSwitcher>();

        // rigidbody �������� �� ����, cameraSwitchr �������� �� ����, fuel 1 �̻�
        if (rb != null && cameraSwitcher != null && fuel > 0)
        {
            //�÷��̾ ���� ����(���� ����ǰ� �ִ� ī�޶� ����)
            Vector3 direction;

            // ���� ����ǰ� �ִ� ī�޶� ���� ����
            if (cameraSwitcher.IsFirstPersonCamera())
            {
                direction = firstPersonCam.transform.forward.normalized;
                /*ShowJetPackEffect(firstPersonCam);*/
            } else
            {
                direction = thirdPersonCam.transform.forward.normalized;
                /*ShowJetPackEffect(thirdPersonCam);*/
            }

            // �÷��̾ ���� ���⿡ �� ����
            rb.AddForce(direction * jetPower, ForceMode.Impulse);

            fuel--; // ���� ����

            print("��Ʈ�� �۵�");

        }
        else if (rb == null)
        {
            Debug.LogError("PlayerObject�� Rigidbody ������Ʈ�� �����ϴ�!");
            enabled = false;
        }
    }

    /*public void ShowJetPackEffect(Camera currentCam)
    {
    // ��Ʈ�� �۵� ȿ�� �þ߰� ��ȯ���� ������. 
    // �ٵ� �þ߰� ��ȯ�ϴٰ� �� ��û ������ �ϴ� �� ����ϰ� ������.
    }*/
}
