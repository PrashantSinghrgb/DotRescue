using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour
{
    
    private GameObject _gameCanvas;
    private GameObject _mainCanvas;
    private GameObject _settingsCanvas;
    private GameObject _activeCanvas;


    private void Start()
    {
        InitializeCanvases();

        // Ensure the main Canvas is active initially
        SwitchCanvas(_mainCanvas.name);
    }

    // Subscribe to the sceneLoaded event
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    // Unsubscribe from the sceneLoaded event
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeCanvases();
    }    

    private void InitializeCanvases()
    {
        _gameCanvas = FindInactiveObjectByName("Game_Canvas");
        _mainCanvas = FindInactiveObjectByName("Main_Canvas");
        _settingsCanvas = FindInactiveObjectByName("Settings_Canvas");
       // SoundManager.Instance._slider = FindSlider("Audio_Slider");

        // Activate the main canvas if no active canvas is set
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

    // Switch the active canvas based on the given name
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
            SoundManager.Instance._slider = FindSlider("Audio_Slider");
            SoundManager.Instance._toggle = FindToggle("Toggle");
        }
        else if (name == _gameCanvas.name)
        {
            targetCanvas = _gameCanvas;
        }

        // Switch the active canvas if a valid target canvas is found
        if (targetCanvas != null && targetCanvas != _activeCanvas)
        {
            _activeCanvas.SetActive(false);
            targetCanvas.SetActive(true);
            _activeCanvas = targetCanvas;
        }
    }

    // Find an inactive object by name
    public GameObject FindInactiveObjectByName(string name)
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

    public Slider FindSlider(string name)
    {
        Slider[] allSliders = Resources.FindObjectsOfTypeAll<Slider>();

        foreach (Slider slider in allSliders)
        {
            if (slider.name== name)
            {
                return slider;
            }
        }
        return null;
    }
    public Toggle FindToggle(string name)
    {
        Toggle[] allToggles = Resources.FindObjectsOfTypeAll<Toggle>();

        foreach(Toggle toggle in allToggles)
        {
            if (toggle.name == name)
            {
                return toggle;
            }
        }
        return null;
    }
}
