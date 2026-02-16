using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Start Screen")]
    public GameObject startPanel;

    [Header("Game UI")]
    public Text roundText;
    public GameObject restartButton;

    [Header("Game Settings")]
    public int maxRounds = 4;
    private int currentRound = 1;

    public ApplePicker applePicker;

    void Start()
    {
        // Show start screen and pause game
        Time.timeScale = 0;
        startPanel.SetActive(true);

        // Hide restart button at start
        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }

        // Set initial round text
        if (roundText != null)
        {
            UpdateRoundText();
        }
    }

    public void StartGame()
    {
        // Hide start screen and begin game
        startPanel.SetActive(false);
        Time.timeScale = 1;
    }

    void UpdateRoundText()
    {
        if (roundText != null)
        {
            roundText.text = "Round " + currentRound;
        }
    }

    // Called when player loses all baskets - returns true if should continue to next round
    public bool ShouldContinueToNextRound()
    {
        if (currentRound < maxRounds)
        {
            // Move to next round
            currentRound++;
            UpdateRoundText();
            return true; // Continue to next round
        }
        else
        {
            // Already at max rounds
            return false; // Game over
        }
    }

    public void GameOver()
    {
        if (roundText != null)
        {
            roundText.text = "Game Over";
        }
        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}