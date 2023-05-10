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

    [SerializeField] GameObject endScene;
    [SerializeField] GameObject frame;
    [SerializeField] GameObject scores;
    [SerializeField] GameObject teams;
    [SerializeField] GameObject resume;
    [SerializeField] GameObject paueButton;
    
    private AudioSource RefreeWhistle;
    
    private void Start()
    {
        footballRB = football.GetComponent<Rigidbody>();
        endOfMatch = 0;
        endScene.SetActive(false);
        RefreeWhistle = GetComponent<AudioSource> ();
    }
    // Update is called once per frame
    void Update()
    {
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
                paueButton.SetActive(false);
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
        footballRB.position = new Vector3(0, 10, 0);
        footballRB.velocity = Vector3.zero;
        footballRB.WakeUp();
    }

    IEnumerator Delay()
    { 
    yield return new WaitForSeconds(0.1f);
    RefreeWhistle.Play();

    endScene.SetActive(true);
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    Time.timeScale = 0f;

    LeanTween.moveX(frame, 960f, 1f).setDelay(0f).setEase(LeanTweenType.easeInOutCirc).setIgnoreTimeScale(true);
    LeanTween.moveX(scores, 960f, 1f).setDelay(2f).setEase(LeanTweenType.easeInOutBack).setIgnoreTimeScale(true);
    LeanTween.moveX(teams, 960f, 1f).setDelay(1.5f).setEase(LeanTweenType.easeInOutBack).setIgnoreTimeScale(true);
    LeanTween.moveY(resume, 275f, 1.4f).setDelay(2f).setEase(LeanTweenType.easeInOutElastic).setIgnoreTimeScale(true);
    }
}
