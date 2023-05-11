using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlaySingle() {
        //Debug.Log("PLAY!");
        SceneManager.LoadScene("Main");
        
    }

    public void PlayMultiplayer() {
        //Debug.Log("PLAY!");
        SceneManager.LoadScene("MP-Janne");
        
    }

    public void QuitGame() {
        //Debug.Log("QUIT!");
        Application.Quit();
    }
}
