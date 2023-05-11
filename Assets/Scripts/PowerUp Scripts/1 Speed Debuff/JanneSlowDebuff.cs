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
        else if (target.CompareTag("AI-character"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = true;
            target.transform.root.GetComponent<ActiveRagdoll.JensAiForcedMovement>().MultiplySpeedByFactor(multiplier);
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
        else if (target.CompareTag("AI-character"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = false;
            target.transform.root.GetComponent<ActiveRagdoll.JensAiForcedMovement>().MultiplySpeedByFactor(1 / multiplier);
        }
    }

    public override int GetId()
    {
        return id;
    }
}
