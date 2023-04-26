using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/iceDebuff")]
public class iceDebuff : PowerupEffects
{
    public float duration = 2f; // duration of the stick effect in seconds

    public override void Apply(GameObject target)
    {
        // disable the target's Rigidbody and Collider components so it doesn't move or collide with anything
        target.GetComponent<Rigidbody>().isKinematic = true;
        target.GetComponent<Collider>().enabled = false;

        // start a coroutine to remove the stick effect after the specified duration
        target.GetComponent<PlayerMove>().StartCoroutine(RemoveStickEffect(target));
    }

    IEnumerator RemoveStickEffect(GameObject target)
    {
        // wait for the duration of the stick effect
        yield return new WaitForSeconds(duration);

        // re-enable the target's Rigidbody and Collider components so it can move and collide again
        target.GetComponent<Rigidbody>().isKinematic = false;
        target.GetComponent<Collider>().enabled = true;
    }
}
