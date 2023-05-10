using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPGoalTextScript : MonoBehaviour
{
    public GameObject goalText;
    public float tweenTime = 0.05f;
    public GameObject ball;
    //public MPstickScript2 stick;

    // Start is called before the first frame update
    void Start()
    {
        goalText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Goal1"))
        {
            StartCoroutine(PauseGoalCollider(other.GetComponent<Collider>()));
            StartCoroutine(PlayGoalTextAnimation());;
            StartCoroutine(SpawnBall());
        }

        else if (other.gameObject.name.Equals("Goal2"))
        {
            StartCoroutine(PauseGoalCollider(other.GetComponent<Collider>()));
            StartCoroutine(PlayGoalTextAnimation());
            StartCoroutine(SpawnBall());
        }
        
    }

    public void PlayAnimation()
    {
        Debug.Log("Skurk");
        LeanTween.scale(goalText, Vector3.one * 1.5f, 0.1f);
    }

    public void ResetGoalTextSize()
    {
        goalText.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void RespawnBall()
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        //stick.unstick();
        rb.Sleep();
        rb.position = new Vector3(0, 10, 0);
        rb.velocity = Vector3.zero;
        rb.WakeUp();
    }

    IEnumerator PauseGoalCollider(Collider collider)
    {
        collider.enabled = false;
        yield return new WaitForSecondsRealtime(2);
        collider.enabled = true;
    }

    IEnumerator SpawnBall()
    {
        yield return new WaitForSecondsRealtime(2);
        RespawnBall();
    }


    IEnumerator PlayGoalTextAnimation()
    {
        goalText.SetActive(true);
        PlayAnimation();
        yield return new WaitForSecondsRealtime(2);
        goalText.SetActive(false);
        ResetGoalTextSize();
    }
}
