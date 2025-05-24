using TMPro;
using UnityEngine;

public class HintUIManager : MonoBehaviour
{
    public string[,] stageHints = new string[5, 3]
    {
        { "1. 두 방은 완전히 같지 않아.",
        "\n2. 총 세 개의 다른 점이 있어.",
        "\n3. 순서는 나로베."},
        { "1. 왼쪽 벽에 놓인 세 개의 액자를 활용해야 해!",
        "\n2. 액자 앞에 놓인 것은 조명이야.",
        "\n3. 그림자의 위치를 특정 액자로 옮기면 돼!" },
        { "",
        "",
        "" },
        { "1. 앞에 놓인 삼각형을 건드려 볼까?",
        "\n2. 삼각형을 들고 움직여 봐!",
        "\n3. 보는 위치에 따라 크기를 조정할 수 있어." },
        {"1. 미라한테 말을 걸어볼까?",
        "\n2. 시점을 3인칭으로 바꿔봐!",
        "\n3. 벽과 천장이 수상해..." }
    };


    public int currentStage;
    int count;

    public TextMeshProUGUI hintText;

    public GameObject respawner;

    public GameObject chaserSpawner1;
    public GameObject chaserSpawner2;
    public GameObject chaserSpawner4;
    public GameObject chaserSpawner5;

    // Update is called once per frame
    void Start()
    {
        UpdateRespawnPosition(1);
    }

    public void UpdateRespawnPosition(int stage)
    {
        currentStage = stage;
        count = 0;
        hintText.text = "";
    }

    public void ShowHintText()
    {
        TimerRespawn tr = respawner.GetComponent<TimerRespawn>();

        if (tr != null)
        {
            if (currentStage != tr.currentStage)
            {
                UpdateRespawnPosition(tr.currentStage);
            }
        }

        if (currentStage == 0 || currentStage == 3) {
            print("힌트가 없는 방입니다.");
        }
        else if (count >= 3)
        {
            print("더이상 힌트를 볼 수 없습니다.");
        } else
        {
            hintText.text += stageHints[currentStage - 1, count];
            count++;
            if (IsSuccessWithTwentyPercent())
            {
                switch (currentStage)
                {
                    case 1:
                        ChaserSpawner cs1 = chaserSpawner1.GetComponent<ChaserSpawner>();
                        cs1.SpawnChaser();
                        break;
                    case 2:
                        ChaserSpawner cs2 = chaserSpawner1.GetComponent<ChaserSpawner>();
                        cs2.SpawnChaser();
                        break;
                    case 4:
                        ChaserSpawner cs4 = chaserSpawner1.GetComponent<ChaserSpawner>();
                        cs4.SpawnChaser();
                        break;
                    case 5:
                        ChaserSpawner cs5 = chaserSpawner1.GetComponent<ChaserSpawner>();
                        cs5.SpawnChaser();
                        break;
                    default:
                        break;
                }
            }
            
        }
    }

    public bool IsSuccessWithTwentyPercent()
    {
        return UnityEngine.Random.value < 0.9f;
    }
}
