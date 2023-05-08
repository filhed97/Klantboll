using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/speedDebuff")]

public class speedDebuff2 : PowerupEffects2
{
    public float amount;
    public int id = 2;
    public override void Apply(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<PlayerMove>().hasPowerup = true;
            target.GetComponent<PlayerMove>().strafeSpeed -= amount;
            target.GetComponent<PlayerMove>().speed -= amount;
        }
        else
        {
            target.GetComponent<AIScript>().hasPowerup = true;
            target.GetComponent<AIScript>().movementSpeed -= 5;
        }
    }

    public override void remove(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<PlayerMove>().hasPowerup = false;
            target.GetComponent<PlayerMove>().strafeSpeed += amount;
            target.GetComponent<PlayerMove>().speed += amount;
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
