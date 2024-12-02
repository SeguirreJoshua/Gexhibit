using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Game Over UI panel
    public TMP_Text finalScoreText; // Final score display
    public TMP_Text bestScoreText; // Best score display
    public TMP_Text gameOverText; // Game Over text display

    public GameObject retryButton;  // Retry button
    public GameObject mainMenuButton; // Main Menu button

    private int finalScore;
    private int bestScore;

    void Start()
    {
        // Hide the buttons and text at the start, they will be shown when the game is over
        retryButton.SetActive(false);
        mainMenuButton.SetActive(false);
        gameOverPanel.SetActive(false); // Hide Game Over panel initially
        gameOverText.gameObject.SetActive(false); // Hide the "Game Over" text initially
    }

    // Call this method when the game ends
    public void DisplayGameOver(int score)
    {
        finalScore = score;
        bestScore = PlayerPrefs.GetInt("BestScoreQuiz", 0);

        // If the final score is higher than the best score, update the best score
        if (finalScore > bestScore)
        {
            bestScore = finalScore;
            PlayerPrefs.SetInt("BestScoreQuiz", bestScore);
        }

        // Update the UI elements with the final and best scores
        finalScoreText.text = "Final Score: " + finalScore;
        bestScoreText.text = "Best Score: " + bestScore;

        // Show the Game Over panel and buttons
        gameOverPanel.SetActive(true);
        retryButton.SetActive(true);
        mainMenuButton.SetActive(true);

        // Display the Game Over text
        gameOverText.gameObject.SetActive(true);
    }

    // Restart the game by reloading the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    // Load the main menu scene (replace "MainMenu" with your main menu scene name)
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    // Quit the game (only works in a built game, not in the editor)
    public void QuitGame()
    {
        Application.Quit();
    }
}
