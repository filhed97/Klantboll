using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/JanneSlowDebuff")]

public class JanneSlowDebuff : PowerupEffects2
{
    public float multiplier = 0.5f;
    public int id = 4;
    public override void Apply(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = true;
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
        // Debug.Log("REMOVE");
        if (target.CompareTag("Player"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = false;
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
