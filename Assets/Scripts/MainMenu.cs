using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("MinotaurLevel");
    }

    public void OnQuit()
    {   
        #if (UNITY_STANDALONE)
            Debug.Log("App Quit");
            Application.Quit();
        #elif (UNITY_WEBGL)
            SceneManagement.LoadScene("Quit");
        #endif
    }
    Resolution resolution;
    public void ChangeResolution()
    {
        resolution.height = 1080;
        resolution.width = 1920;
    }
}
