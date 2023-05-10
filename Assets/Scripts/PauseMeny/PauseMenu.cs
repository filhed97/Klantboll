using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    int activatePauseMenu = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && activatePauseMenu == 0)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            activatePauseMenu = 1;
        }
        else if(activatePauseMenu == 1 && Input.GetKeyDown(KeyCode.Escape) )
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            activatePauseMenu = 0;
        }
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        activatePauseMenu = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        activatePauseMenu = 0;
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1;
        activatePauseMenu = 0;
        SceneManager.LoadScene(sceneID);
    }
}
