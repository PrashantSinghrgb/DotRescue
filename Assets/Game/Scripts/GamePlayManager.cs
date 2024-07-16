using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GamePlayManager : MonoBehaviour
{
    private bool _hasGameFinished;

    [SerializeField] private TMP_Text _scoreText;

    private float _score;
    private float _scoreSpeed;
    private int _currentLevel;

    // Lists to hold level speeds and maximum scores
    [SerializeField] private List<int> _levelSpeed, _levelMax;

    private void Awake()
    {
        // Initialize GameManager to mark the game as initialized
        GameManager.Instance.IsInitialized = true;

        // Reset score and level
        _score = 0;
        _currentLevel = 0;
        
        // Display initial score
        _scoreText.text = ((int)_score).ToString();

        // Set initial score speed based on current level
        _scoreSpeed = _levelSpeed[_currentLevel];
    }

    private void Update()
    {
        // If the game has finished, stop updating
        if (_hasGameFinished)
        {
            return;
        }

        // Increase score based on score speed and delta time
        _score += _scoreSpeed * Time.deltaTime;

        // Update the displayed score text
        _scoreText.text = ((int)_score).ToString();

        // Check if current score surpasses the maximum score for the current level
        if (_score > _levelMax[Mathf.Clamp(_currentLevel, 0, _levelMax.Count - 1)]) 
        {
            // Move to the next level if possible
            _currentLevel = Mathf.Clamp(_currentLevel + 1, 0, _levelMax.Count - 1);
            _scoreSpeed = _levelSpeed[_currentLevel];
        }
    }

    // Method to call when the game ends
    public void GameEnded()
    {
        _hasGameFinished = true;

        // Save the final score to GameManager
        GameManager.Instance.CurrentScore = (int)_score;

        // Wait for a moment before going to game over screen
        StartCoroutine(GameOver());
    }

    // Coroutine to handle the game over transition
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);

        // Go back to the main menu using GameManager
        GameManager.Instance.GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(GameManager._mainMenu);
    }
    
}
