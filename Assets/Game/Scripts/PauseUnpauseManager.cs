using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUnpauseManager : MonoBehaviour
{
    public void Pause_Unpause(float time)
    {
        Time.timeScale = time;
    }
}
