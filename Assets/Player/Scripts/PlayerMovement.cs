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

    // ThirdRoom 관련
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
        if (canUseKeyInput) // 입력 키 사용 가능할 때(중력 적용될 때)
        {
            Move();
            Jump();
        }
        else // 입력 키 사용 불가능할 때(무중력일 때)
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

    // 이 밑으로는 FourthRoom 관련
    public void ReadyNoneGravityRoom()
    {
        
        if (jetPackObject == null)
        {
            Debug.LogError("JetPack 오브젝트에 연결되지 않았습니다!");
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

    // 이전 프레임 속도 기록
    private void FixedUpdate()
    {
        fixedCurrentSpeed = rb.linearVelocity;
    }

    // 처음 시작할 때, 리스폰할 때 세팅들
    public void NoneGravityRoomStartSettings()
    {
        canUseKeyInput = true;   // input 사용 가능으로 시작
        rb.useGravity = true;
        jetPack.AddFuel();
    }


    // 무중력 공간으로 들어갈 때 발동
    public void InNoneGravity()
    {
        KeyInputControl(false);
        GravityControl(false);
    }

    // 플레이어에게 적용되는 중력 조작
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

    // 플레이어의 키 입력 능력 조작
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

    


    // player를 respawnPosition에 리스폰
    /*public void RespawnPlayer()
    {
        gameObject.transform.position = blackHoleRespawnPosition;
        rb.linearVelocity = Vector3.zero;
        NoneGravityRoomStartSettings();
        print("Respawn!");
    }*/
}