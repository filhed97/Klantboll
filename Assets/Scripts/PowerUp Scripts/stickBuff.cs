using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/stickBuff")]

public class stickBuff : PowerupEffects
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<stickscript>().sticky = true;
        target.GetComponent<stickscript>().sticky2 = true;
    }
}

