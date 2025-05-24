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
            Debug.LogError("QuestionUIManager�� ã�� �� �����ϴ�!");
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
                playerInteraction.canUseMouseInput = false; // ��й�ȣ �Է� �ÿ��� ��Ŭ�� �Է� �� ����
                uIManager.Show(selectedQuestion);
            }
            else
            {
                Debug.LogError("PlayerInteraction�� ã�� �� �����ϴ�!");
                return;
            }
        }
        else
        {
            Debug.LogError("QuestionUIManager�� ã�� �� �����ϴ�!");
        }
    }


    
}
