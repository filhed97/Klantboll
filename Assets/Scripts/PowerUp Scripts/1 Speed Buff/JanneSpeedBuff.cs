using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/JanneSpeedBuff")]

public class JanneSpeedBuff : PowerupEffects2
{
    public int id = 6;

    public override void Apply(GameObject target)
    {
        float multiplier = target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().BoostSpeedMultiplier;

        if (target.CompareTag("Player"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = true;
            target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().boostMode = true;
            target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().MultiplySpeedByFactor(multiplier);
        }
        else
        {
            target.GetComponent<AIScript>().hasPowerup = true;
            target.GetComponent<AIScript>().movementSpeed *= multiplier;
        }
    }

    public override void remove(GameObject target)
    {      
        float multiplier = target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().BoostSpeedMultiplier;

        // Debug.Log("REMOVE");
        if (target.CompareTag("Player"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = false;
            target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().boostMode = false;
            target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().MultiplySpeedByFactor((1/multiplier));
        }
        else
        {
            target.GetComponent<AIScript>().hasPowerup = false;
            target.GetComponent<AIScript>().movementSpeed *= (1/multiplier);
        }
    }

    public override int GetId()
    {
        return id;
    }
}
