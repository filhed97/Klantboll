using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupEffects powerupEffects;

    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
        powerupEffects.Apply(collision.gameObject);
    }
}
