 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audiomix;
    public bool isFullscreen = true;
    public void SetResoulation(int index)
    {
        if (index ==0)
        {
            Screen.SetResolution(1920, 1080, isFullscreen);
        }
        else if (index==1)
        {
            Screen.SetResolution(800, 800, isFullscreen);
        }

    }
    public void SetQulity(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool fullscreen_enable)
    {
        Screen.fullScreen = fullscreen_enable;
        isFullscreen = fullscreen_enable;
    }
    public void SetMouseSensitivity(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);

        if (GameObject.FindGameObjectWithTag("Player") != null) 
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovoment>().mouseSensivity = value;
        }
    }

    public void SetMasterVolume(float value)
    {
        audiomix.SetFloat("MasterVolume", value);
    }
    public void SetMusicVolume(float value)
    {
        audiomix.SetFloat("MusicVolume", value);
    }
}
