using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{

    [Header("Win Text")]
    [SerializeField]
    TextMeshProUGUI finalScoreText;

    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations! \n You scored" +
        scoreKeeper.CalculateScore() + "%";
    }
}
