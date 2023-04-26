using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TieBreakerScript : MonoBehaviour
{
    public GameObject Object;
    public Vector3 spawnPoint = new(1, 1, 0);
    private int temp = 0;
    public TimerTextObject timerTextObject;
    public GameObject timerObj;

    // Update is called once per frame
    void Update()
    {
        if(timerTextObject.timeValue <= 0)
        {
            if (GoalRegister.score1 == GoalRegister.score2)
            {
                if (temp == 0)
                {
                    temp = 1;
                    StartCoroutine(ShowTieBreakerText());
                    timerObj.GetComponent<Text>().color = Color.red;
                }
            }
            else
            {
                GoalRegister.score1 = 0;
                GoalRegister.score2 = 0;
                SceneManager.LoadScene("Menu");
            }
        }
    }

    IEnumerator ShowTieBreakerText()
    {
        Object.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2);
        Object.SetActive(false);
        Time.timeScale = 1f;
    }
}
