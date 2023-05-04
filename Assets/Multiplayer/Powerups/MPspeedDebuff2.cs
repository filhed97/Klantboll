using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/speedDebuff")]

public class MPspeedDebuff2 : PowerupEffects2
{
    public float amount;
    public int id = 2;
    public override void Apply(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayer>().hasPowerup = true;
            target.GetComponent<NetworkMultiplayer>().playerSpeed -= amount;
        }
    }

    public override void remove(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayer>().hasPowerup = false;
            target.GetComponent<NetworkMultiplayer>().playerSpeed += amount;
        }
    }

    public override int GetId()
    {
        return id;
    }
}
