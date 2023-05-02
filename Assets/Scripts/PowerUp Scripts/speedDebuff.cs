using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/speedDebuff")]

public class speedDebuff : PowerupEffects
{
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMove>().strafeSpeed -= amount;
        target.GetComponent<PlayerMove>().speed -= amount;
    }
}
