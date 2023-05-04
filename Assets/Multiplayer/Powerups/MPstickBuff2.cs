using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powerups/StickBuff2")]
public class MPstickBuff2 : PowerupEffects2
{
    public int id = 3;

    public override void Apply(GameObject target)
    {
        target.GetComponent<NetworkMultiplayer>().hasPowerup = true;
        target.GetComponent<MPstickScript2>().sticky = true;
    }

    public override void remove(GameObject target)
    {
        target.GetComponent<NetworkMultiplayer>().hasPowerup = false;
        target.GetComponent<MPstickScript2>().unstick();
    }

    public override int GetId()
    {
        return id;
    }
}