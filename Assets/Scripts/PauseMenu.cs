using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{   
    public GameObject pauseMenu;
    Animator anim;
    private bool isShown = false;
    
    private void Awake() {
        pauseMenu = GetComponent<GameObject>();
        anim = GetComponent<Animator>();
    }
    public void OnPause()
    {
        anim.SetBool(AnimationStrings.isShown, true);
        isShown = true;
        Time.timeScale = 0;
        
    }

    public void OnResume()
    {
        anim.SetBool(AnimationStrings.isShown, false);
        isShown = false;
        Time.timeScale = 1;
    }

    public void OnHome()
    {   
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void OnOption()
    {
        Time.timeScale = 1;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isShown)
                OnPause();
            else if (isShown)
                OnResume();
        }
    }
}
