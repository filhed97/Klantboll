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
            target.GetComponent<NetworkMultiplayerJanne>().hasPowerup.Value = true;
            target.transform.root.GetComponent<ActiveRagdoll.MPForcedMovement>().MovementSpeed -= amount;
        }
    }

    public override void remove(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayerJanne>().hasPowerup.Value = false;
            target.transform.root.GetComponent<ActiveRagdoll.MPForcedMovement>().MovementSpeed += amount;
        }
    }

    public override int GetId()
    {
        return id;
    }
}
