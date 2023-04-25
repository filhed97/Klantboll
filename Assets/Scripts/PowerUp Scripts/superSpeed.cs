using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]

public class superSpeed : PowerupEffects
{
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMove>().speed += amount;
        target.GetComponent<PlayerMove>().strafeSpeed += amount;
    }
}
