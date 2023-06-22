using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Quiz : MonoBehaviour
{
    QuestionSO currentQuestion;

    [Header("Questions")]
    [SerializeField]
    List<QuestionSO> questions = new List<QuestionSO>();

    [Header("Questions")]
    [SerializeField]
    TextMeshProUGUI questionText;

    [Header("Answers")]
    [SerializeField]
    GameObject[] answerButtons;
    int correctAnswerIndex;

    [Header("Button Colors")]
    [SerializeField]
    Sprite defaultAnswerSprite;

    [Header("Button Colors")]
    [SerializeField]
    Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField]
    Image timerImage;

    [Header("Scoring")]
    [SerializeField]
    TextMeshProUGUI scoreText;

    [Header("ProgressBar")]
    [SerializeField]
    Slider progressBar;

    ScoreKeeper scoreKeeper;
    public bool isComplete;
    bool hasAnsweredEarly = true;
    Timer timer;
    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.minValue = 0;
        progressBar.value = 0;
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value >= progressBar.maxValue)
            {
                isComplete = true;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }

    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";


    }

    void DisplayAnswer(int index)
    {
        Debug.Log(index + " " + currentQuestion.GetCorrectAnswerIndex());
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = "The correct Answer is " + currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex());
            buttonImage = answerButtons[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }

    }

    private void GetRandomQuestion()
    {
        int Index = Random.Range(0, questions.Count);
        currentQuestion = questions[Index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    private void SetDefaultButtonSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }

    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
}
