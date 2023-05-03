using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGame : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textScoreBackground;

    public void Start() {
        int score1 = PlayerPrefs.GetInt("Score1");
        int score2 = PlayerPrefs.GetInt("Score2");
        textScore.text = score1.ToString() + "               " + score2.ToString();
        textScoreBackground.text = score1.ToString() + "               " + score2.ToString();
    }


    public void ResumeGame() {
        SceneManager.LoadScene("Menu");
    }
}
