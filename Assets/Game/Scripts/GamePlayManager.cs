using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GamePlayManager : MonoBehaviour
{
    private bool _hasGameFinished;

    [SerializeField]
    private TMP_Text _scoreText;

    private float _score;
    private float _scoreSpeed;
    private int _currentLevel;

    [SerializeField]
    private List<int> _levelSpeed, _levelMax;


    private void Awake()
    {
        GameManager.Instance.IsInitialized = true;

        _score = 0;
        _currentLevel = 0;

        _scoreText.text = ((int)_score).ToString();
        _scoreSpeed = _levelSpeed[_currentLevel];
    }

    private void Update()
    {
        if (_hasGameFinished)
        {
            return;
        }

        _score += _scoreSpeed * Time.deltaTime;
        _scoreText.text = ((int)_score).ToString();

        if (_score > _levelMax[Mathf.Clamp(_currentLevel, 0, _levelMax.Count - 1)]) 
        {
            _currentLevel = Mathf.Clamp(_currentLevel + 1, 0, _levelMax.Count - 1);
            _scoreSpeed = _levelSpeed[_currentLevel];
        }
    }
    public void GameEnded()
    {
        _hasGameFinished = true;
        GameManager.Instance.CurrentScore = (int)_score;

        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(GameManager._mainMenu);
    }
    
}
