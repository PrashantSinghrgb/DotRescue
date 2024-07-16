using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singletone instance of the gamemanager
    public static GameManager Instance;
    
    // high score key for playerprefs
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

    public const string _mainMenu = "MainMenu";
    private const string _gamePlay = "GamePlay";

    private void Awake()
    {
        if (Instance == null)
        {
            // set the instance of this object
            Instance = this;
            
            // initialize the game manaer
            Init();
            
            // Prevent this object from being destroyed when loading a new scen
            DontDestroyOnLoad(gameObject);

            return;

        }
        else
        {
            // Destroy duplicate instances of the game manager
            Destroy(gameObject);
        }
        
    }
    
    private void Init()
    {
        CurrentScore = 0;
        IsInitialized = false;
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(_mainMenu);
    }
    public void GoToGamePlay()
    {
        SceneManager.LoadScene(_gamePlay);
    }
}
