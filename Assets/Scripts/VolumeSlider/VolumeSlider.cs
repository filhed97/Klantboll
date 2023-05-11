using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider m_Slider;
    public float myVolume; // Volume Value

    private void Start()
    {
        if (m_Slider.value != PlayerPrefs.GetFloat("MasterVolume"))
        {
            m_Slider.value = PlayerPrefs.GetFloat("MasterVolume");
        }

        myVolume = PlayerPrefs.GetFloat("MasterVolume");
    }

    public void OnValChange(float myValue)
    {
        myVolume = myValue;
        PlayerPrefs.SetFloat("MasterVolume", myVolume);
        myVolume = Mathf.Round(myVolume * 100f) / 100f;
        ApplyVolume(myVolume);
    }

    private void ApplyVolume(float volume)
    {
        AudioListener.volume = volume;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = volume;
        }
    }
}
