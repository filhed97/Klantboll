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
        target.GetComponent<PlayerMove>().hasPowerup = true;
        target.GetComponent<PlayerMove>().strafeSpeed += amount;
        target.GetComponent<PlayerMove>().speed += amount;
        
    }

    public override void remove(GameObject target)
    {
        target.GetComponent<PlayerMove>().hasPowerup = false;
        target.GetComponent<PlayerMove>().strafeSpeed -= amount;
        target.GetComponent<PlayerMove>().speed -= amount;
    }

    public override int GetId()
    {
        return id;
    }
}
