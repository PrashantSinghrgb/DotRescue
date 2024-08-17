using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.Haptics;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _effectSource;
    
    public Slider _slider;
    public Toggle _toggle;
    public static SoundManager Instance;
    private CanvasController _canvasController;

    private bool _hapticEnables = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_slider == null)
        {

        }
        else
        {
            _slider.value = _effectSource.volume;
            _slider.onValueChanged.AddListener(OnValueChange);
        }

        if (_toggle != null)
        {
            _toggle.isOn = _hapticEnables;
            _toggle.onValueChanged.AddListener(TriggerHaptic);
        }
        else
        {
            Debug.LogWarning("Toggle Is Missing");
        }
    }
    

    public void TriggerHaptic(bool isOn)
    {
        _hapticEnables = isOn;

        if (_hapticEnables)
        {
            TriggerHapticFeedback();
            Debug.Log("Haptic feedback is enabled");
        }
        else
        {
            Debug.Log("Haptic feedback is disabled");
        }
    }
    public void TriggerHapticFeedback()
    {
        if (_hapticEnables)
        {
            Handheld.Vibrate();
            Debug.Log("Haptic On called");
        }
        else
        {
            Debug.Log("Haptic Off called");
        }
    }

    private void OnValueChange(float value)
    {
        _effectSource.volume = value;
    }
    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }
}
