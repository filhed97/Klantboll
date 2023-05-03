using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupEffects2 : ScriptableObject
{
    public abstract void Apply(GameObject target);

    public abstract void remove(GameObject target);

    public abstract int GetId();
}
