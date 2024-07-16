using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameCanvas;
    [SerializeField]
    private GameObject _mainCanvas;
    [SerializeField]
    private GameObject _settingsCanvas;
    [SerializeField]
    private GameObject _activeCanvas;


    private void Start()
    {
        InitializeCanvases();

        // Ensure the main Canvas is active initially
        SwitchCanvas(_mainCanvas.name);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeCanvases();
    }    

    private void InitializeCanvases()
    {
        _gameCanvas = FindInactiveObjectByName("Game_Canvas");
        _mainCanvas = FindInactiveObjectByName("Main_Canvas");
        _settingsCanvas = FindInactiveObjectByName("Settings_Canvas");

        if (_activeCanvas != null)
        {
            _activeCanvas.SetActive(true);
        }
        else if(_mainCanvas != null)
        {
            _activeCanvas = _mainCanvas;
            _activeCanvas.SetActive(true);
        }
    }
    public void SwitchCanvas(string name)
    {
        GameObject targetCanvas = null;

        if (name == _mainCanvas.name)
        {
            targetCanvas = _mainCanvas;
        }
        else if (name == _settingsCanvas.name)
        {
            targetCanvas = _settingsCanvas;
        }
        else if (name == _gameCanvas.name)
        {
            targetCanvas = _gameCanvas;
        }

        if (targetCanvas != null && targetCanvas != _activeCanvas)
        {
            _activeCanvas.SetActive(false);
            targetCanvas.SetActive(true);
            _activeCanvas = targetCanvas;
        }
    }
    private GameObject FindInactiveObjectByName(string name)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }
}
