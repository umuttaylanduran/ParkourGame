using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void Play_btn()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit_btn()
    {
        Application.Quit();
    }
}
