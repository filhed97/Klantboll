using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class MPSpeedBuff : PowerupEffects2
{
    public float amount;
    public int id = 0;
    public override void Apply(GameObject target)
    {
        float multiplier = target.transform.root.GetComponent<ActiveRagdoll.MPForcedMovement>().BoostSpeedMultiplier;

        if (target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayerJanne>().hasPowerup.Value = true;
            target.transform.root.GetComponent<ActiveRagdoll.MPForcedMovement>().boostMode = true;
            target.transform.root.GetComponent<ActiveRagdoll.MPForcedMovement>().MultiplySpeedByFactor(multiplier);
        }       
    }

    public override void remove(GameObject target)
    {
        float multiplier = target.transform.root.GetComponent<ActiveRagdoll.MPForcedMovement>().BoostSpeedMultiplier;
        if (target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayerJanne>().hasPowerup.Value = false;
            target.transform.root.GetComponent<ActiveRagdoll.MPForcedMovement>().boostMode = false;
            target.transform.root.GetComponent<ActiveRagdoll.MPForcedMovement>().MultiplySpeedByFactor((1 / multiplier));
        }
    }

    public override int GetId()
    {
        return id;
    }
}
