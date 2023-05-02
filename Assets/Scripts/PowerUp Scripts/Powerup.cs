using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float duration = 4f;
    public float defaultspeed = 500;
    public PowerupEffects powerupEffects;
    public static int numOfPowerups;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !(other.gameObject.GetComponent<PlayerMove>().hasPowerup))
        {
            other.gameObject.GetComponent<PlayerMove>().hasPowerup = true;
            StartCoroutine(Pickup(other));
        }
    }


    IEnumerator Pickup(Collider other)
    {
        numOfPowerups--;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        powerupEffects.Apply(other.gameObject);
        yield return new WaitForSeconds(duration);

        other.gameObject.GetComponent<PlayerMove>().speed = defaultspeed;
        other.gameObject.GetComponent<PlayerMove>().strafeSpeed = defaultspeed;
        if (other.gameObject.GetComponent<stickscript>().sticky2)
        {
            other.gameObject.GetComponent<stickscript>().unstick();
        }
        other.gameObject.GetComponent<PlayerMove>().hasPowerup = false;
        Destroy(gameObject);

    }


}