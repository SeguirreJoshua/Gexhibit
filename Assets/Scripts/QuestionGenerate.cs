using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public GameObject visual; // Visual representation of the question
    public string questionText; // The question text
    public string[] answers; // Array of answer choices
    public string correctAnswer; // Correct answer
}

public class QuestionGenerate : MonoBehaviour
{
    public List<QuestionData> questions; // List of all questions

    public static string actualAnswer;
    public static bool displayingQuestion = false;

    public int questionNumber;
    public GameObject gameOverPanel;
    public TMPro.TMP_Text gameOverText; // To display the game over text

    private List<int> remainingQuestions = new List<int> { 0, 1, 2, 3, 4 }; // List of all questions
    private bool gameOver = false;  // Flag to check if game is over
    private int answeredQuestions = 0; // Track how many questions have been answered

    void Start()
    {
        // Initialize the list of remaining questions
        remainingQuestions = new List<int>();
        for (int i = 0; i < questions.Count; i++)
        {
            remainingQuestions.Add(i);
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true); // Show the Game Over panel
        gameOverText.text = "Game Over!\nFinal Score: " + PlayerPrefs.GetInt("FinalScoreQuiz", 0);
        gameOver = true; // Set the game over flag to true
    }

    void Update()
    {
        if (!gameOver)  // Only display new questions if game is not over
        {
            if (!displayingQuestion && remainingQuestions.Count > 0)
            {
                displayingQuestion = true;
                DisplayRandomQuestion();  // Call to display a random question
            }
            else if (remainingQuestions.Count == 0 && !gameOverPanel.activeSelf && answeredQuestions == questions.Count)
            {
                // After the last question has been answered, trigger Game Over screen
                Debug.Log("All questions answered. Triggering Game Over.");
                GameOver();
            }
        }
    }

    void DisplayRandomQuestion()
    {
        displayingQuestion = true;

        // Select a random question
        int randomIndex = Random.Range(0, remainingQuestions.Count);
        int questionIndex = remainingQuestions[randomIndex];
        remainingQuestions.RemoveAt(randomIndex); // Remove from the list of remaining questions

        // Deactivate all visuals
        foreach (var question in questions)
        {
            question.visual.SetActive(false);
        }

        // Activate the selected question's visual and set its data
        var selectedQuestion = questions[questionIndex];
        selectedQuestion.visual.SetActive(true);

        // Update UI with question and answers
        QuestionDisplay.newQuestion = selectedQuestion.questionText;
        QuestionDisplay.newA = selectedQuestion.answers[0];
        QuestionDisplay.newB = selectedQuestion.answers[1];
        QuestionDisplay.newC = selectedQuestion.answers[2];
        QuestionDisplay.newD = selectedQuestion.answers[3];
        actualAnswer = selectedQuestion.correctAnswer;

        // Notify QuestionDisplay to refresh UI
        QuestionDisplay.pleaseUpdate = false;
    }

    // Optionally, reset the `displayingQuestion` flag when moving to the next question
    public void ResetDisplayingQuestion()
    {
        displayingQuestion = false;
        answeredQuestions++; // Increment the number of answered questions
        PlayerPrefs.SetInt("FinalScoreQuiz", answeredQuestions * 5); // Update the final score
    }

    // Helper method to reset everything for replay (if needed)
    public void ResetGame()
    {
        remainingQuestions.Clear();
        answeredQuestions = 0; // Reset answered questions count
        for (int i = 0; i < questions.Count; i++)
        {
            remainingQuestions.Add(i);
        }

        foreach (var question in questions)
        {
            question.visual.SetActive(false);
        }

        displayingQuestion = false;
    }
}
