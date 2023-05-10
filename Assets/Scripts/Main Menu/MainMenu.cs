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

    public void PlayGame() {
        //Debug.Log("PLAY!");
        SceneManager.LoadScene("Main");
        
    }

    public void QuitGame() {
        //Debug.Log("QUIT!");
        Application.Quit();
    }
}
