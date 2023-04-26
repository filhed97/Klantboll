using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GoalRegister : MonoBehaviour
{
    public static int score1, score2;
    [SerializeField] private TextMeshProUGUI textScore;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Ball")) {
            if(name.Equals("Goal1")) {
                IncreaseScore1();
            }

            else if(name.Equals("Goal2")){
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
}
