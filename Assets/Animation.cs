using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{

    public GameObject goalText;
    Vector3 myVector;
    public float tweenTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Tween();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Tween()
    {
        LeanTween.scale(goalText, Vector3.one * 5, tweenTime);
    }

    public void Reset()
    {
        goalText.transform.localScale = new Vector3(1, 1, 1);
    }
}
