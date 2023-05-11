using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/JanneSpeedBuff")]

public class JanneSpeedBuff : PowerupEffects2
{
    public int id = 6;
    private float multiplier;
    private float AIMultiplier;

    public override void Apply(GameObject target)
    {

        if (target.CompareTag("Player"))
        {
            multiplier = target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().BoostSpeedMultiplier;
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = true;
            target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().boostMode = true;
            target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().MultiplySpeedByFactor(multiplier);
        }
        else if (target.CompareTag("AI-character"))
        {
            AIMultiplier = target.transform.root.GetComponent<ActiveRagdoll.JensAiForcedMovement>().BoostSpeedMultiplier;
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = true;
            target.transform.root.GetComponent<ActiveRagdoll.JensAiForcedMovement>().boostMode = true;
            target.transform.root.GetComponent<ActiveRagdoll.JensAiForcedMovement>().MultiplySpeedByFactor(AIMultiplier);
        }
    }

    public override void remove(GameObject target)
    {      

        // Debug.Log("REMOVE");
        if (target.CompareTag("Player"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = false;
            target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().boostMode = false;
            target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().MultiplySpeedByFactor((1/AIMultiplier));
        }
        else if(target.CompareTag("AI-character"))
        {
            Debug.Log("hALF OF THINGS");
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = false;
            target.transform.root.GetComponent<ActiveRagdoll.JensAiForcedMovement>().boostMode = false;
            target.transform.root.GetComponent<ActiveRagdoll.JensAiForcedMovement>().MultiplySpeedByFactor(1/AIMultiplier);
        }
    }

    public override int GetId()
    {
        return id;
    }
}
