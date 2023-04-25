using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerTextObject : MonoBehaviour
{
   public float timeValue;
   public Text timerText;
   public static int activate = 2;

    // Update is called once per frame
    void Update()
    {
        // check if CountDownStarting is executed (3 2 1 GO!)
        if(CountDownStarting.activate == 0)
        {
            if(timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;

                // reset the scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                

                // to activate the countDownStarting 
                CountDownStarting.activate = 1;
            }

            DisplayTime(timeValue);
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = timeToDisplay % 1 * 100;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
