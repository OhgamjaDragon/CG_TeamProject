using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private Camera playerCam;
    private float xRotation = 0f;
    private bool isGrounded;

    // ThirdRoom ����
    public bool canUseKeyInput = true;
    public Vector3 fixedCurrentSpeed;
    public GameObject jetPackObject;
    private JetPack jetPack;
    //

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookAround();
        if (canUseKeyInput) // �Է� Ű ��� ������ ��(�߷� ����� ��)
        {
            Move();
            Jump();
        }
        else // �Է� Ű ��� �Ұ����� ��(���߷��� ��)
        {
            UseJetPack();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;

        Vector3 moveDir = camForward.normalized * moveZ + camRight.normalized * moveX;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 velocity = moveDir * currentSpeed;
        Vector3 yVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        rb.linearVelocity = velocity + yVelocity;
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Item"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Item"))
        {
            isGrounded = false;
        }
    }

    // �� �����δ� FourthRoom ����
    public void ReadyNoneGravityRoom()
    {
        
        if (jetPackObject == null)
        {
            Debug.LogError("JetPack ������Ʈ�� ������� �ʾҽ��ϴ�!");
            enabled = false;
            return;
        }
        jetPack = jetPackObject.GetComponent<JetPack>();
        NoneGravityRoomStartSettings();
    }

    public void QuitNoneGravityRoom()
    {
        jetPack = null;
        canUseKeyInput = true;
        rb.useGravity = true;
    }

    void UseJetPack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            jetPack.OperateJetPack();
        }
    }

    // ���� ������ �ӵ� ���
    private void FixedUpdate()
    {
        fixedCurrentSpeed = rb.linearVelocity;
    }

    // ó�� ������ ��, �������� �� ���õ�
    public void NoneGravityRoomStartSettings()
    {
        canUseKeyInput = true;   // input ��� �������� ����
        rb.useGravity = true;
        jetPack.AddFuel();
    }


    // ���߷� �������� �� �� �ߵ�
    public void InNoneGravity()
    {
        KeyInputControl(false);
        GravityControl(false);
    }

    // �÷��̾�� ����Ǵ� �߷� ����
    private void GravityControl(bool is_gravity)
    {
        if (is_gravity)
        {
            rb.useGravity = true;
        }
        else
        {
            rb.useGravity = false;
        }
    }

    // �÷��̾��� Ű �Է� �ɷ� ����
    private void KeyInputControl(bool can_use)
    {
        if (can_use)
        {
            canUseKeyInput = true;
        }
        else
        {
            canUseKeyInput = false;
        }
    }

    


    // player�� respawnPosition�� ������
    /*public void RespawnPlayer()
    {
        gameObject.transform.position = blackHoleRespawnPosition;
        rb.linearVelocity = Vector3.zero;
        NoneGravityRoomStartSettings();
        print("Respawn!");
    }*/
}