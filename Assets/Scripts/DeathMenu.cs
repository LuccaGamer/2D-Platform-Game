using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{   
    public float delay = 0.5f;
    Animator anim;
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    private void Update() {
        if (anim.GetBool(AnimationStrings.isShown)) 
        {
            Invoke("FreezeTime",delay);
        }
    }

    public void OnRespawn()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    private void FreezeTime()
    {
        Time.timeScale = 0f;
    }
}
