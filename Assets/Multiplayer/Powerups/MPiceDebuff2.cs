using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/iceDebuff")]

public class MPiceDebuff2 : PowerupEffects2
{
    public float amount;
    public int id = 4;
    public override void Apply(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayer>().hasPowerup.Value = true;
            target.GetComponent<NetworkMultiplayer>().playerSpeed -= amount;
        }
    }

    public override void remove(GameObject target)
    {
       // Debug.Log("REMOVE");
        if(target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayer>().hasPowerup.Value = false;
            target.GetComponent<NetworkMultiplayer>().playerSpeed += amount;
        }
    }

    public override int GetId()
    {
        return id;
    }
}
