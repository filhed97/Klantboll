using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/KickBuff")]

public class KickBuff : PowerupEffects
{
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<BallKicker>().kickforce += amount;
    }
}
