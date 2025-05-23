using UnityEngine;
using TMPro;

public class TimerRespawn : MonoBehaviour
{
    public float timerDuration = 300f; // 5분 예시
    private float timer;

    public Transform respawnPosition;
    public GameObject player;

    public TextMeshProUGUI timerText;

    void Start()
    {
        timer = timerDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Max(0f, timer); // 음수 방지

        // 분:초 계산
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        // UI에 표시 (예: 2:09)
        if (timerText != null)
        {
            timerText.text = $"Timer: {minutes:00}:{seconds:00}";
        }

        // 시간 종료 후 리스폰
        if (timer <= 0f)
        {
            RespawnPlayer();
            timer = timerDuration;
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
}
