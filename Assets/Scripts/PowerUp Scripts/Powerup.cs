using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupEffects powerupEffects;

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            powerupEffects.Apply(other.gameObject);
        }
    }
}
