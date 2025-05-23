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

    public void ChangeQuestion()   // 문제 0번부터 4번까지 랜덤 선택
    {
        selectedQuestion = UnityEngine.Random.Range(0, questionNum);
    }

    public int GetQuestion()
    {
        return selectedQuestion;
    }
}