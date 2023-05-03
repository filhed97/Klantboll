using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TimerMultiplayer : NetworkBehaviour
{
    public GameObject Object;
    public float timeValue;
    public Text timerText;

    // Update is called once per frame
    void Update()
    {
        // check if CountDownStarting is executed (3 2 1 GO!)
        if (CountDownStarting.activate == 0)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }

            DisplayTimeClientRpc(timeValue);
        }
    }

    [ClientRpc]
    private void DisplayTimeClientRpc(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}