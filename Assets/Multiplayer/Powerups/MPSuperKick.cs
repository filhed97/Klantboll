using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SuperKick")]
public class MPSuperKick : PowerupEffects2
{
    public float amount = 1000;
    public int id = 1;
    public override void Apply(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayerJanne>().hasPowerup.Value = true;
            target.GetComponent<BallKickerMP>().kickforce = amount;
        }
    }

    public override void remove(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<NetworkMultiplayerJanne>().hasPowerup.Value = false;
            target.GetComponent<BallKickerMP>().kickforce = 200;
        }
    }

    public override int GetId()
    {
        return id;
    }



}
