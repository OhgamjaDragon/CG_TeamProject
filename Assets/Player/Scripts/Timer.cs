using UnityEngine;

public class Timer : MonoBehaviour
{
    public float currentTime;
    public float second;
    public float timeOutTimeInFourthRoom = 40f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartSetting();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null && !playerMovement.canUseKeyInput)   // 무중력 공간에서 시간 측정
        {
            currentTime += Time.deltaTime;

            CheckTimeOutInThirdRoom();
            if (currentTime >= second)
            {
                print(second++);
            }
        }
    }

    private void CheckTimeOutInThirdRoom()
    {
        if (currentTime >= timeOutTimeInFourthRoom)
        {
            TimeOut();
        }
    }

    private void TimeOut()
    {
        PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            print("time out! " + second + "s");
            playerMovement.RespawnPlayer();
        }
    }

    public void StartSetting()
    {
        currentTime = 0f;
        second = 1f;
    }
}
