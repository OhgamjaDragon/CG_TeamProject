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
            Debug.LogError("JetPack에 Object가 연결되지 않았습니다!");
            enabled = false;
            return;
        }
        if (firstPersonCam == null || thirdPersonCam == null)
        {
            Debug.LogError("JetPack에 Camera가 연결되지 않았습니다!");
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

        // rigidbody 가져오는 거 성공, cameraSwitchr 가져오는 거 성공, fuel 1 이상
        if (rb != null && cameraSwitcher != null && fuel > 0)
        {
            //플레이어가 보는 방향(현재 적용되고 있는 카메라 방향)
            Vector3 direction;

            // 현재 적용되고 있는 카메라 방향 적용
            if (cameraSwitcher.IsFirstPersonCamera())
            {
                direction = firstPersonCam.transform.forward.normalized;
                /*ShowJetPackEffect(firstPersonCam);*/
            } else
            {
                direction = thirdPersonCam.transform.forward.normalized;
                /*ShowJetPackEffect(thirdPersonCam);*/
            }

            // 플레이어가 보는 방향에 힘 적용
            rb.AddForce(direction * jetPower, ForceMode.Impulse);

            fuel--; // 연료 감소

            print("제트팩 작동");

        }
        else if (rb == null)
        {
            Debug.LogError("PlayerObject에 Rigidbody 컴포넌트가 없습니다!");
            enabled = false;
        }
    }

    /*public void ShowJetPackEffect(Camera currentCam)
    {
    // 제트팩 작동 효과 시야각 전환으로 보여줌. 
    // 근데 시야각 전환하다가 또 요청 들어오면 하던 거 취소하고 갱신함.
    }*/
}
