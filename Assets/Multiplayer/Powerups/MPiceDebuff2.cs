using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/iceDebuff")]

public class MPiceDebuff2 : PowerupEffects2
{
    public float multiplier;
    public int id = 4;
    public override void Apply(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayerJanne>().hasPowerup.Value = true;
            target.transform.root.GetComponent<ActiveRagdoll.MPForcedMovement>().MultiplySpeedByFactor(multiplier);
        }
    }

    public override void remove(GameObject target)
    {
       // Debug.Log("REMOVE");
        if(target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayerJanne>().hasPowerup.Value = false;
            target.transform.root.GetComponent<ActiveRagdoll.MPForcedMovement>().MultiplySpeedByFactor(1 / (multiplier));
        }
    }

    public override int GetId()
    {
        return id;
    }
}
