using UnityEngine;
using System;

public class QuestionNpc : MonoBehaviour
{
    int questionNum = 5;
    int selectedQuestion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DateTime currentTime = DateTime.Now;

        int seed = currentTime.GetHashCode();

        UnityEngine.Random.InitState(seed);

        ChangeQuestion();
    }

    public void ChangeQuestion()   // ���� 0������ 4������ ���� ����
    {
        selectedQuestion = UnityEngine.Random.Range(0, questionNum);
    }

    public int GetQuestion()
    {
        return selectedQuestion;
    }
}