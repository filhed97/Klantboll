using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Image speedIcon;
    public Image slowIcon;
    public Image iceIcon;
    public Image kickIcon;
    private float speed;
    private float kick;
    public BallKicker ballkicking;
    public GameObject PlayerHudprefab;
    private GameObject PlayerHud;

    private void Start()
    {
        PlayerHud = Instantiate(PlayerHudprefab, gameObject.transform).transform.GetChild(0).gameObject;

        Image[] images = PlayerHud.GetComponentsInChildren<Image>();

        speedIcon = images[1];
        slowIcon = images[2];
        iceIcon = images[3];
        kickIcon = images[4];

    }
    void Update()
    {
        speed = GetComponent<ActiveRagdoll.ForcedMovement>().GetSpeed();
        kick = ballkicking.kickforce;

        if(speed < 0.1f)
        {
            iceIcon.enabled = true;
        }
        else if (speed > 8f)
        {
            speedIcon.enabled = true;
        }
        else if (speed < 4f)
        {
            slowIcon.enabled = true;
        }
        else if(kick > 900)
        {
            kickIcon.enabled = true;
        }
        else
        {
            speedIcon.enabled = false;
            slowIcon.enabled = false;
            iceIcon.enabled = false;
            kickIcon.enabled = false;
        }
        
    }
}
