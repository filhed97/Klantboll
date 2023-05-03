using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TieBreakerScript : MonoBehaviour
{
    public GameObject Object;
    private int ShowTieBreakerTextOnce = 0;
    public static int endOfMatch = 0;
    public TimerTextObject timerTextObject;
    public GameObject timerObj;
    public GameObject football;

    public GameObject goal1, goal2;

    private void Start()
    {
        endOfMatch = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(TimerTextObject.timeValue <= 0)
        {
            if (GoalRegister.score1 == GoalRegister.score2)
            {
                if (ShowTieBreakerTextOnce == 0)
                {
                    ShowTieBreakerTextOnce = 1;
                    endOfMatch = 1;
                    StartCoroutine(ShowTieBreakerText());
                    timerObj.GetComponent<Text>().color = Color.red;
                    football.GetComponent<BallRespawn>().respawnBall();
                }
            }
            else
            {
                goal1.GetComponent<Collider>().enabled = false;
                goal2.GetComponent<Collider>().enabled = false;
                StartCoroutine(Delay());
                
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

    IEnumerator Delay()
    {

        yield return new WaitForSeconds(2);
        //GoalRegister.score1 = 0;
        //GoalRegister.score2 = 0;
        
        SceneManager.LoadScene("Menu");
    }

}
