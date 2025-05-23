using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionUIManager : MonoBehaviour
{
    public GameObject panel;

    int currentQuestion;
    public TextMeshProUGUI questionText;

    public Camera firstPersonCam;

    [TextArea(10, 15)]
    string question0 = "나는 태양신 라의 가호 아래 나일 강이 내려다보이는 이곳에서 그대에게 지혜의 시험을 내리겠노라. 나는 이집트의 귀족, 세네프루라고 하네." +
        " 나의 서기관이 기록한 문제이니, 잘 듣고 답해보게나.\n\n\"나에게는 3개의 튼튼한 곡물 창고가 있네. 각 창고에는 탐스러운 밀이 정확히 15자루씩 들어있다네. " +
        "내가 관할하는 5개의 마을에 이 밀을 공평하게 나누어 주려 하는데, 한 마을당 몇 자루의 밀을 받게 되겠는가?\"" +
        "\n자, 그대의 총명함을 보여주게.";
    [TextArea(10, 15)]
    string question1 = "나는 태양신 라의 가호 아래 나일 강이 내려다보이는 이곳에서 그대에게 지혜의 시험을 내리겠노라. 나는 이집트의 귀족, 세네프루라고 하네." +
        "\n나의 서기관이 기록한 문제이니, 잘 듣고 답해보게나.\n\n\"나의 영원한 안식처가 될 무덤에 특별한 방을 하나 계획하고 있네. 이 방은 직사각형 모양으로 만들 것이며, " +
        "그 방의 둘레를 재어보니 정확히 24 로열 규빗이었네. 나의 수석 건축가는 방의 너비가 그 가로 길이의 정확히 세 배가 되도록 설계하라는 엄명을 받았네. " +
        "자, 그렇다면 이 특별한 방의 가로 길이는 몇 로열 규빗이 되겠는가?\"" +
        "\n자, 그대의 총명함을 보여주게.";
    [TextArea(10, 15)]
    string question2 = "나는 태양신 라의 가호 아래 나일 강이 내려다보이는 이곳에서 그대에게 지혜의 시험을 내리겠노라. 나는 이집트의 귀족, 세네프루라고 하네." +
        " 나의 서기관이 기록한 문제이니, 잘 듣고 답해보게나.\n\n\"나의 수석 서기관인 '메텐'은 왕실의 모든 기록을 파피루스 두루마리 하나에 완벽히 기록하는 데 12일이 걸린다네. " +
        "그의 숙련된 제자 '카이'는 같은 양의 기록을 마치려면 36일이 필요하지. 만약 메텐과 카이가 각자 다른 부분을 맡아 하나의 두루마리를 함께 기록한다면, " +
        "그 두루마리를 완성하는 데 며칠이 걸리겠는가?\"" +
        "\n자, 그대의 총명함을 보여주게.";
    [TextArea(10, 15)]
    string question3 = "나는 태양신 라의 가호 아래 나일 강이 내려다보이는 이곳에서 그대에게 지혜의 시험을 내리겠노라. 나는 이집트의 귀족, 세네프루라고 하네." +
        " 나의 서기관이 기록한 문제이니, 잘 듣고 답해보게나.\n\n\"위대한 파라오를 기리기 위해 거대한 오벨리스크를 건설하려 하네. 이 오벨리스크의 기초는 완벽한 정사각형 모양으로 다져질 것이야." +
        " 수석 건축가가 신성한 계산을 통해 말하기를, '이 정사각형 기초의 넓이에서 그 기초의 한 변의 길이에 8을 곱한 값을 빼면, 그 결과는 정확히 9가 될 것입니다.' 라고 하였다네. " +
        "자, 그렇다면 이 오벨리스크 기초의 한 변의 길이는 몇 규빗이겠는가?\"" +
        "\n자, 그대의 총명함을 보여주게.";
    [TextArea(10, 15)]
    string question4 = "나는 태양신 라의 가호 아래 나일 강이 내려다보이는 이곳에서 그대에게 지혜의 시험을 내리겠노라. 나는 이집트의 귀족, 세네프루라고 하네." +
        " 나의 서기관이 기록한 문제이니, 잘 듣고 답해보게나.\n\n\"나의 왕실 정원에는 두 종류의 신성한 연꽃이 자라고 있다네. " +
        "하나는 하늘을 담은 듯한 푸른 연꽃이고, 다른 하나는 순결을 상징하는 흰 연꽃일세.\r\n\r\n정원에 있는 푸른 연꽃의 수는 그 자체로 어떤 자연수를 제곱한 수와 같다네. " +
        "만약 이 푸른 연꽃이 지금보다 정확히 5송이가 더 많았다면, 그 수는 41송이가 되었을 것이야.\r\n한편, 흰 연꽃의 수는, 현재 피어있는 푸른 연꽃의 총 수에서 빼면, " +
        "내가 특별히 아끼는 신성한 거위들의 수와 정확히 일치한다네. 내가 돌보는 신성한 거위는 총 27마리일세.\r\n자, 그렇다면 나의 정원에 곱게 피어있는 흰 연꽃은 총 몇 송이겠는가?\"" +
        "\n자, 그대의 총명함을 보여주게.";


    void Start()
    {
        /*panel.SetActive(false);*/
    }

    // 추가한 부분

    public void OperateStartSettings()
    {
        panel.SetActive(false);
    }

    //

    public void Show(int selectedQuestion)
    {
        SetCursorLock(false);       // 마우스 커서 락 해제
        panel.SetActive(true);
        switch (selectedQuestion) { 
        case 0:
                questionText.text = question0;
                break;
        case 1:
                questionText.text = question1;
                break;
        case 2:
                questionText.text = question2;
                break;
        case 3:
                questionText.text = question3;
                break;
        case 4:
                questionText.text = question4;
                break;
        default:
                break;
        }
    }


    public void Hide()
    {
        panel.SetActive(false);
        SetCursorLock(true);
        PlayerInteraction playerInteraction
            = firstPersonCam.GetComponent<PlayerInteraction>();
        if (playerInteraction != null) playerInteraction.ActivateMouseInput();
        else
        {
            Debug.LogError("PlayerInteraction를 찾을 수 없습니다!");
        }
    }

    public void SetCursorLock(bool lockCursor)
    {
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None; // 커서 잠금 해제
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // 커서 잠금 (원하는 경우)
        }
    }
}