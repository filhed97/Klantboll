using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float duration = 3f;

    private PlayerMove playerMove;
    //private BallKicker ballKicker;

    public PowerupEffects powerupEffects;

    private float originalSpeed;
    private float originalStrafeSpeed;
    //private float originalKickForce;

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        //ballKicker = GetComponent<BallKicker>();

        // Store the original values
        originalSpeed = playerMove.speed;
        originalStrafeSpeed = playerMove.strafeSpeed;
        //originalKickForce = ballKicker.kickforce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider other)
    {
        powerupEffects.Apply(other.gameObject);

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Reset to the original values
        yield return new WaitForSeconds(duration);
        //other.GetComponent<BallKicker>().kickforce = 80;
        other.GetComponent<PlayerMove>().speed = 500;
        other.GetComponent<PlayerMove>().strafeSpeed = 500;
        Destroy(gameObject);

    }
}