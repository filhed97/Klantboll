using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GoalRegister : MonoBehaviour
{
    public static int score1, score2;
    [SerializeField] private TextMeshProUGUI textScore;
    private AudioSource GoalCheering;
    public GameObject goalText;
    public GameObject ani;
    public GameObject ball;

    public static int toRespawnBall = 0;

    public static int pauseTimeVar = 0;

    public int temp = 0;

    void Start()
    {
        goalText.SetActive(false);
        GoalCheering = GetComponent<AudioSource> ();
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Ball")) {
            pauseTimeVar = 1;

            if(name.Equals("Goal1")) {
                this.GetComponent<Collider>().enabled = false;
                GoalCheering.Play();
                displayGoalText();
                IncreaseScore1();
            }

            else if(name.Equals("Goal2")){
                this.GetComponent<Collider>().enabled = false;
                GoalCheering.Play();
                displayGoalText();
                IncreaseScore2();
            }
        }
    }

    private void IncreaseScore1() {
        score1++;
        UpdateScore();
    }

    private void IncreaseScore2() {
        score2++;
        UpdateScore();
    }

    private void UpdateScore() {
        textScore.text = score1.ToString() +"       "+ score2.ToString();
    }

    private void displayGoalText()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        goalText.SetActive(true);
        ani.GetComponent<Animation>().Tween();
        yield return new WaitForSecondsRealtime(3);
        goalText.SetActive(false);
        ani.GetComponent<Animation>().Reset();
        if(TimerTextObject.timeValue > 0.9)
            ball.GetComponent<BallRespawn>().respawnBall();
        this.GetComponent<Collider>().enabled = true;
        pauseTimeVar = 0;

        if (score1 == score2)
            temp = 1;
        
    }
}
