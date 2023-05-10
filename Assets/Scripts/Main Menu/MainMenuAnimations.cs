using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAnimations : MonoBehaviour
{
    [SerializeField] GameObject logo; 
    [SerializeField] GameObject gameOptions;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject kickoffText;
    [SerializeField] GameObject buttons;

    void Start()
    {
        buttons.SetActive(false);
        kickoffText.SetActive(false);
        Invoke("displayText", 2f);

        LeanTween.scale(logo, new Vector3(20f, 12f, 1f), 1.8f).setDelay(0.2f).setEase(LeanTweenType.easeOutElastic);

    }

    public void onClickStart() {

        Invoke("displayTextButtons", 1.2f);

        if(startButton != null) {
        Destroy(startButton);
        }

        if(kickoffText != null) {
        Destroy(kickoffText);
        }
        //LeanTween.scale(logo, new Vector3(0.6f, 0.6f, 0.6f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(logo, new Vector3(10f, 6f, 1f), 3f).setDelay(0f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.move(logo, new Vector3(Screen.width/2, Screen.width/2.2f, 200f), 1.2f).setDelay(0f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.delayedCall(2f, () => { });
    }

    public void displayText() {
        kickoffText.SetActive(true);
    }

    public void displayTextButtons() {
        buttons.SetActive(true);
        //LeanTween.scale(buttons, new Vector3(0.1f, .1f, .1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveY(buttons, Screen.height/2, 1.4f).setDelay(0f).setEase(LeanTweenType.easeInOutElastic);
    }
}
