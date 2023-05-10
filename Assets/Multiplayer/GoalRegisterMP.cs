using UnityEngine;
using TMPro;
using Unity.Netcode;

public class GoalRegisterMP : NetworkBehaviour
{
    private static NetworkVariable<int> scoreOrange = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private static NetworkVariable<int> scoreBlue = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    //public static int score1, score2;
    [SerializeField] private TextMeshProUGUI textScore;
    private AudioSource GoalCheering;

    void Start()
    {
        GoalCheering = GetComponent<AudioSource>();
    }

    public override void OnNetworkSpawn()
    {
        scoreOrange.OnValueChanged += (int oldValue, int newValue) => { };
        scoreBlue.OnValueChanged += (int oldValue, int newValue) => { };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Ball"))
        {
            if (name.Equals("Goal1"))
            {
                GoalCheering.Play();
                IncreaseScore1ServerRpc();
            }

            else if (name.Equals("Goal2"))
            {
                GoalCheering.Play();
                IncreaseScore2ServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void IncreaseScore1ServerRpc()
    {
        scoreBlue.Value++;
        UpdateScoreClientRpc(scoreOrange.Value, scoreBlue.Value);
    }

    [ServerRpc(RequireOwnership = false)]
    private void IncreaseScore2ServerRpc()
    {
        scoreOrange.Value++;
        UpdateScoreClientRpc(scoreOrange.Value, scoreBlue.Value);
    }

    [ClientRpc]
    private void UpdateScoreClientRpc(int val1, int val2)
    {
        textScore.text = val1.ToString() + "       " + val2.ToString();
    }
}