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
    private Rigidbody footballRB;
    public GameObject goal1, goal2;

    private void Start()
    {
        footballRB = football.GetComponent<Rigidbody>();
        endOfMatch = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(endOfMatch);
        if(timerTextObject.timeValue <= 0)
        {

            if (GoalRegister.score1 == GoalRegister.score2)
            {
                if (ShowTieBreakerTextOnce == 0)
                {
                    ShowTieBreakerTextOnce = 1;
                    endOfMatch = 1;
                    StartCoroutine(ShowTieBreakerText());
                    timerObj.GetComponent<Text>().color = Color.red;
                }
            }
            else
            {
                Debug.Log("ELSE!!");
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
        footballRB.Sleep();
        footballRB.position = new Vector3(0, 1, 0);
        footballRB.velocity = Vector3.zero;
        footballRB.WakeUp();
    }

    IEnumerator Delay()
    { 
        yield return new WaitForSeconds(1f);
        GoalRegister.score1 = 0;
        GoalRegister.score2 = 0;
        
        SceneManager.LoadScene("Menu");
    }
}
