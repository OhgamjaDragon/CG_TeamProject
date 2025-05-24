using TMPro;
using UnityEngine;

public class HintUIManager : MonoBehaviour
{
    public string[,] stageHints = new string[5, 3]
    {
        { "1. �� ���� ������ ���� �ʾ�.",
        "\n2. �� �� ���� �ٸ� ���� �־�.",
        "\n3. ������ ���κ�."},
        { "1. ���� ���� ���� �� ���� ���ڸ� Ȱ���ؾ� ��!",
        "\n2. ���� �տ� ���� ���� �����̾�.",
        "\n3. �׸����� ��ġ�� Ư�� ���ڷ� �ű�� ��!" },
        { "",
        "",
        "" },
        { "1. �տ� ���� �ﰢ���� �ǵ�� ����?",
        "\n2. �ﰢ���� ��� ������ ��!",
        "\n3. ���� ��ġ�� ���� ũ�⸦ ������ �� �־�." },
        {"1. �̶����� ���� �ɾ��?",
        "\n2. ������ 3��Ī���� �ٲ��!",
        "\n3. ���� õ���� ������..." }
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
            print("��Ʈ�� ���� ���Դϴ�.");
        }
        else if (count >= 3)
        {
            print("���̻� ��Ʈ�� �� �� �����ϴ�.");
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
