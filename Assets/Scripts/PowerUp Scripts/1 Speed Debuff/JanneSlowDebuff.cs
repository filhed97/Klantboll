using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/JanneSlowDebuff")]

public class JanneSlowDebuff : PowerupEffects2
{
    public float amount = 2;
    public int id = 4;
    public float originalSpeed;
    public override void Apply(GameObject target)
    {
        originalSpeed = target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().MovementSpeed;

        if (target.CompareTag("Player"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = true;
            target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().MovementSpeed /= amount;
        }
        else
        {
            target.GetComponent<AIScript>().hasPowerup = true;
            target.GetComponent<AIScript>().movementSpeed -= 5;
        }
    }

    public override void remove(GameObject target)
    {
       // Debug.Log("REMOVE");
        if(target.CompareTag("Player"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = false;
            target.transform.root.GetComponent<ActiveRagdoll.ForcedMovement>().MovementSpeed = originalSpeed;
        }
        else
        {
            target.GetComponent<AIScript>().hasPowerup = false;
            target.GetComponent<AIScript>().movementSpeed += 5;    
        }
    }

    public override int GetId()
    {
        return id;
    }
}
