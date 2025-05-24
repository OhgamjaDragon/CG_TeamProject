using UnityEngine;
using TMPro;

public class TimerRespawn : MonoBehaviour
{
    public float timerDuration = 300f; // 예: 5분
    private float timer;

    public Transform respawnPosition;
    public GameObject player;
    public TextMeshProUGUI timerText;

    //
    public int currentStage = 1;
    //

    void Start()
    {
        timer = timerDuration;
    }

    void Update()
    {
        // 타이머 감소
        timer -= Time.deltaTime;
        timer = Mathf.Max(0f, timer);

        // 타이머 UI 표시
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        if (timerText != null)
        {
            timerText.text = $"Timer: {minutes:00}:{seconds:00}";
        }

        // 자동 리스폰
        if (timer <= 0f)
        {
            RespawnPlayer();
            timer = timerDuration;
        }

        // 키 입력 리스폰 (예: R 키)
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
            timer = timerDuration; // 타이머도 초기화하고 싶다면 포함
        }
    }

    void RespawnPlayer()
    {
        if (player != null && respawnPosition != null)
        {
            player.transform.position = respawnPosition.position;
            player.transform.rotation = respawnPosition.rotation;
        }
    }

    public void UpdateRespawnPoint(Transform newRespawn)
    {
        // 추적자 생성 위치 정하기 위해서 현재 player 스테이지 확인하는 코드
        if (respawnPosition.position != newRespawn.position) {
            currentStage++;
        }
        //

        respawnPosition.position = newRespawn.position;
        respawnPosition.rotation = newRespawn.rotation;
    }
}