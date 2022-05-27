using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    public Slider mouse_slider;
    private void Awake()
    {
        //Set Mouse Sensitivity
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovoment>().mouseSensivity = PlayerPrefs.GetFloat("MouseSensitivity",200);
        
       mouse_slider.value= PlayerPrefs.GetFloat("MouseSensitivity", 200);
    }
}
