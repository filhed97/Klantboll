using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedBuff : PowerupEffects2
{
    public float amount = 1000;
    public int id = 0;
    public override void Apply(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<PlayerMove>().hasPowerup = true;
            target.GetComponent<PlayerMove>().strafeSpeed += amount;
            target.GetComponent<PlayerMove>().speed += amount;
        }
        else
        {
            target.GetComponent<AIScript>().hasPowerup = true;
            target.GetComponent<AIScript>().movementSpeed += 5;
        }
        
    }

    public override void remove(GameObject target)
    {
       if(target.CompareTag("Player"))
       {
            target.GetComponent<PlayerMove>().hasPowerup = false;
            target.GetComponent<PlayerMove>().strafeSpeed -= amount;
            target.GetComponent<PlayerMove>().speed -= amount;
       } 
       else 
       {
            target.GetComponent<AIScript>().hasPowerup = false;
            target.GetComponent<AIScript>().movementSpeed -= 5;
       }
    }

    public override int GetId()
    {
        return id;
    }
}
