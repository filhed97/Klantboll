using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton1 : MonoBehaviour
{
    public Toggle muteToggle; 

    void Start()
    {
        if(PlayerPrefs.GetInt("muted") == 1) 
        {
            muteToggle.isOn = true;
        }
        else
        {
            muteToggle.isOn = false;
        }
    
    }

    void Update() 
    {
        if (PlayerPrefs.GetInt("muted") == 1) 
        {
            AudioListener.volume = 0;
        } 
        else 
        {
            AudioListener.volume = 1;
        }
    }


    public void OnMuteToggle() 
    {
        bool isMuted = muteToggle.isOn;
        PlayerPrefs.SetInt("muted", isMuted ? 1 : 0);
    }
}

