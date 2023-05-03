using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownStarting : MonoBehaviour
{
    public float countdownDuration; // Duration of the countdown in seconds
    public TextMeshProUGUI countDownDisplay;
    public static int activate = 1;
    private AudioSource RefreeWhistle;

    private void Start()
    {
        RefreeWhistle = GetComponent<AudioSource> ();
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
       
        // Freeze the game
        Time.timeScale = 0f;

        // Display "3"
        yield return new WaitForSecondsRealtime(0.5f);
        countDownDisplay.text = "3";

        // Display "2"
        yield return new WaitForSecondsRealtime(0.5f);
        countDownDisplay.text = "2";

        // Display "1"
        yield return new WaitForSecondsRealtime(0.5f);
        countDownDisplay.text = "1";

        // Display "Go!"
        yield return new WaitForSecondsRealtime(0.5f);
        countDownDisplay.text = "GO!";

        yield return new WaitForSecondsRealtime(0.5f);
        
        RefreeWhistle.Play();
        yield return new WaitForSecondsRealtime(1f);
        // Unfreeze the game
        Time.timeScale = 1f;
        activate = 0;
        countDownDisplay.gameObject.SetActive(false);
    }

}