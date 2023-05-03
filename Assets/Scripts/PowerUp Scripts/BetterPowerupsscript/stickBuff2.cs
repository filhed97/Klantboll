using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Powerups/StickBuff2")]
public class stickBuff2 : PowerupEffects2
{
    public int id = 3;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMove>().hasPowerup = true;
        target.GetComponent<stickScript2>().sticky = true;
    }

    public override void remove(GameObject target)
    {
        target.GetComponent<PlayerMove>().hasPowerup = false;
        target.GetComponent<stickScript2>().unstick();
    }

    public override int GetId()
    {
        return id;
    }
}