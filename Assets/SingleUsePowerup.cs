using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleUsePowerup : MonoBehaviour
{
    public PowerupEffects powerupEffects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.GetComponent<PlayerMove>().hasPowerup)
        {
            other.GetComponent<PlayerMove>().hasPowerup = true;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            powerupEffects.Apply(other.gameObject);
            Destroy(gameObject);
        }
    }
}
