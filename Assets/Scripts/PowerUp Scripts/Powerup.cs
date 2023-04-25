using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float duration = 4f;
    public float defaultspeed = 500;
    public PowerupEffects powerupEffects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider other)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        powerupEffects.Apply(other.gameObject);
        yield return new WaitForSeconds(duration);

        other.gameObject.GetComponent<PlayerMove>().speed = defaultspeed;
        other.gameObject.GetComponent<PlayerMove>().strafeSpeed = defaultspeed;
        Destroy(gameObject);

    }
}
