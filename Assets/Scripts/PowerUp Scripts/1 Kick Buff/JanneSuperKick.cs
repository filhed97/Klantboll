using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/JanneSuperKick")]
public class JanneSuperKick : PowerupEffects2
{
    public float amount = 1000;
    public int id = 1;
    public override void Apply(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = true;
            target.GetComponent<BallKicker>().kickforce += amount;
        }
        else
        {
            target.GetComponent<AIScript>().hasPowerup = true;
            target.GetComponent<AIScript>().kickforce += amount;
        }
    }

    public override void remove(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.transform.root.GetComponent<powerupCheck>().hasPowerup = false;
            target.GetComponent<BallKicker>().kickforce -= amount;
        }
        else
        {
            target.GetComponent<AIScript>().hasPowerup = false;
            target.GetComponent<AIScript>().kickforce -= amount; 
        }
    }

    public override int GetId()
    {
        return id;
    }



}
