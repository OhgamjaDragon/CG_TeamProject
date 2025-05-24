using UnityEngine;

public class QuestionActivator : MonoBehaviour
{
    public GameObject panel;
    public Camera firstPersonCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QuestionUIManager uIManager = panel.GetComponent<QuestionUIManager>();

        if (uIManager != null)
        {
            uIManager.OperateStartSettings();
        }
        else
        {
            Debug.LogError("QuestionUIManager를 찾을 수 없습니다!");
            return;
        }

    }

    public void ShowQuestionUI(int selectedQuestion)
    {

        QuestionUIManager uIManager = panel.GetComponent<QuestionUIManager>();

        if (uIManager != null)
        {
            PlayerInteraction playerInteraction = firstPersonCam.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.canUseMouseInput = false; // 비밀번호 입력 시에는 좌클릭 입력 안 받음
                uIManager.Show(selectedQuestion);
            }
            else
            {
                Debug.LogError("PlayerInteraction를 찾을 수 없습니다!");
                return;
            }
        }
        else
        {
            Debug.LogError("QuestionUIManager를 찾을 수 없습니다!");
        }
    }


    
}
