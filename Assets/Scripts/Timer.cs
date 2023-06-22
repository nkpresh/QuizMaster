
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public bool isAnsweringQuestion = false;
    float timerValue;
    [SerializeField]
    float questionCompleteTime = 30;
    [SerializeField]
    float correctAnswerDisplayTime = 10;
    public bool loadNextQuestion = false;
    public float fillFraction { get; set; }

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }
    void UpdateTimer()
    {

        timerValue -= Time.deltaTime;
        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / questionCompleteTime;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = correctAnswerDisplayTime;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / correctAnswerDisplayTime;

            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = questionCompleteTime;
                loadNextQuestion = true;

            }
        }
    }
}
