using UnityEngine;
using TMPro;

public class TimerRespawn : MonoBehaviour
{
    public float timerDuration = 300f; // ��: 5��
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
        // Ÿ�̸� ����
        timer -= Time.deltaTime;
        timer = Mathf.Max(0f, timer);

        // Ÿ�̸� UI ǥ��
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        if (timerText != null)
        {
            timerText.text = $"Timer: {minutes:00}:{seconds:00}";
        }

        // �ڵ� ������
        if (timer <= 0f)
        {
            RespawnPlayer();
            timer = timerDuration;
        }

        // Ű �Է� ������ (��: R Ű)
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
            timer = timerDuration; // Ÿ�̸ӵ� �ʱ�ȭ�ϰ� �ʹٸ� ����
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
        // ������ ���� ��ġ ���ϱ� ���ؼ� ���� player �������� Ȯ���ϴ� �ڵ�
        if (respawnPosition.position != newRespawn.position) {
            currentStage++;
        }
        //

        respawnPosition.position = newRespawn.position;
        respawnPosition.rotation = newRespawn.rotation;
    }
}