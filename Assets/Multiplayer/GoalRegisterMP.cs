using UnityEngine;
using TMPro;
using Unity.Netcode;

public class GoalRegisterMP : NetworkBehaviour
{
    public static int score1, score2;
    [SerializeField] private TextMeshProUGUI textScore;
    private AudioSource GoalCheering;

    void Start()
    {
        GoalCheering = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Ball"))
        {
            if (name.Equals("Goal1"))
            {
                GoalCheering.Play();
                IncreaseScore1ClientRpc();
            }

            else if (name.Equals("Goal2"))
            {
                GoalCheering.Play();
                IncreaseScore2ClientRpc();
            }
        }
    }

    [ClientRpc]
    private void IncreaseScore1ClientRpc()
    {
        score1++;
        UpdateScoreClientRpc();
    }

    [ClientRpc]
    private void IncreaseScore2ClientRpc()
    {
        score2++;
        UpdateScoreClientRpc();
    }

    [ClientRpc]
    private void UpdateScoreClientRpc()
    {
        textScore.text = score1.ToString() + "       " + score2.ToString();
    }
}