using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
            DontDestroyOnLoad(gameObject);

            return;

        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private string _highScoreKey = "HighScore";

    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(_highScoreKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(_highScoreKey, value);
        }
    }

    public int CurrentScore { get; set; }
    public bool IsInitialized { get; set; }
  

    private void Init()
    {
        CurrentScore = 0;
        IsInitialized = false;
    }
    public const string _mainMenu = "MainMenu";
    private const string _gamePlay = "GamePlay";

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(_mainMenu);
    }
    public void GoToGamePlay()
    {
        SceneManager.LoadScene(_gamePlay);
    }
}
