using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public GameObject answerAbackBlue, answerAbackGreen, answerAbackRed;
    public GameObject answerBbackBlue, answerBbackGreen, answerBbackRed;
    public GameObject answerCbackBlue, answerCbackGreen, answerCbackRed;
    public GameObject answerDbackBlue, answerDbackGreen, answerDbackRed;

    public GameObject answerA, answerB, answerC, answerD;

    public AudioSource correctFX;
    public AudioSource wrongFX;

    public GameObject currentScore;
    public GameObject bestDisplay;
    public int scoreValue;
    public int bestScore;

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScoreQuiz", 0);
        bestDisplay.GetComponent<TMPro.TMP_Text>().text = "BEST: " + bestScore;
    }

    void Update()
    {
        currentScore.GetComponent<TMPro.TMP_Text>().text = "SCORE: " + scoreValue;
    }

    public void AnswerA() => ProcessAnswer("A", answerAbackGreen, answerAbackRed, answerAbackBlue);
    public void AnswerB() => ProcessAnswer("B", answerBbackGreen, answerBbackRed, answerBbackBlue);
    public void AnswerC() => ProcessAnswer("C", answerCbackGreen, answerCbackRed, answerCbackBlue);
    public void AnswerD() => ProcessAnswer("D", answerDbackGreen, answerDbackRed, answerDbackBlue);

    private void ProcessAnswer(string answer, GameObject green, GameObject red, GameObject blue)
    {
        if (QuestionGenerate.actualAnswer == answer)
        {
            green.SetActive(true);
            blue.SetActive(false);
            correctFX.Play();
            scoreValue += 5;  // Increase score for correct answer
        }
        else
        {
            red.SetActive(true);
            blue.SetActive(false);
            wrongFX.Play();
            // Optionally, you could set score to 0 or apply a different penalty
        }

        DisableButtons(); // Disable buttons after an answer is selected
        StartCoroutine(NextQuestion()); // Proceed to the next question
    }

    private void DisableButtons()
    {
        answerA.GetComponent<Button>().enabled = false;
        answerB.GetComponent<Button>().enabled = false;
        answerC.GetComponent<Button>().enabled = false;
        answerD.GetComponent<Button>().enabled = false;
    }

    IEnumerator NextQuestion()
    {
        // Check if the score is a new best, and if so, update it
        if (bestScore < scoreValue)
        {
            PlayerPrefs.SetInt("BestScoreQuiz", scoreValue);
            bestScore = scoreValue;
            bestDisplay.GetComponent<TMPro.TMP_Text>().text = "BEST: " + bestScore;
        }

        yield return new WaitForSeconds(2); // Wait for 2 seconds before showing next question

        ResetButtonStates(); // Reset button visuals for the next question
        QuestionGenerate.displayingQuestion = false; // Allow the next question to appear
    }

    private void ResetButtonStates()
    {
        // Reset button visuals (Green, Red, Blue backgrounds)
        answerAbackGreen.SetActive(false);
        answerBbackGreen.SetActive(false);
        answerCbackGreen.SetActive(false);
        answerDbackGreen.SetActive(false);

        answerAbackRed.SetActive(false);
        answerBbackRed.SetActive(false);
        answerCbackRed.SetActive(false);
        answerDbackRed.SetActive(false);

        answerAbackBlue.SetActive(true);
        answerBbackBlue.SetActive(true);
        answerCbackBlue.SetActive(true);
        answerDbackBlue.SetActive(true);

        // Reactivate the answer buttons for the next question
        answerA.GetComponent<Button>().enabled = true;
        answerB.GetComponent<Button>().enabled = true;
        answerC.GetComponent<Button>().enabled = true;
        answerD.GetComponent<Button>().enabled = true;
    }
}
