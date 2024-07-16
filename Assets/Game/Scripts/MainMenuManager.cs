using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _newBestText;
    [SerializeField] private TMP_Text _bestScoreText;

    // Animation duration for score display
    [SerializeField] private float _animationTime;
    // Animation curve for score display
    [SerializeField] private AnimationCurve _speedCurve;
    
    // Sound clip for button click
    [SerializeField] private AudioClip _clickClip;
    
    
    private void Awake()
    {
        // Set initial best score text from GameManager
        _bestScoreText.text = GameManager.Instance.HighScore.ToString();

        // Check if GameManager is initialized (usually on game start)
        if (!GameManager.Instance.IsInitialized)
        {
            // Hide score text and new best text if GameManager is not initialized
            _scoreText.gameObject.SetActive(false);
            _newBestText.gameObject.SetActive(false);
        }
        else
        {
            // Start coroutine to animate score display if GameManager is initialized
            StartCoroutine(ShowScore());
        }
    }

    // Coroutine to animate the score display
    private IEnumerator ShowScore()
    {
        int tempScore = 0;
        _scoreText.text = tempScore.ToString();

        int currentScore = GameManager.Instance.CurrentScore;
        int highScore = GameManager.Instance.HighScore;

        // Check if current score is higher than previous high score
        if (highScore < currentScore)
        {
            // Show new best score text and update high score in GameManager
            _newBestText.gameObject.SetActive(true);
            GameManager.Instance.HighScore = currentScore;
        }
        else
        {
            // Hide new best score text if current score is not a new high score
            _newBestText.gameObject.SetActive(false);
        }

        // Update the displayed high score text
        _bestScoreText.text = GameManager.Instance.HighScore.ToString();

        // Animation variables initialization
        float speed = 1 / _animationTime;
        float timeElapsed = 0f;

        // Perform score animation over time using AnimationCurve
        while (timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            tempScore = (int)(_speedCurve.Evaluate(timeElapsed) * currentScore);
            _scoreText.text = tempScore.ToString();

            yield return null;
        }

        // Ensure final score displayed is accurate
        tempScore = currentScore;
        _scoreText.text = tempScore.ToString();
    }

    // Method called when the play button is clicked
    public void ClickedPlay()
    {
        // Play click sound using SoundManager
        SoundManager.Instance.PlaySound(_clickClip);
        // Trigger GameManager to start gameplay
        GameManager.Instance.GoToGamePlay();
    }
}
