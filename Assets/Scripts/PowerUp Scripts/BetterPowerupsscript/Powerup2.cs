using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup2 : MonoBehaviour
{
    public float duration = 3f;
    public PowerupEffects2 powerupeffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.GetComponent<PlayerMove>().hasPowerup)
        {
            if (powerupeffect.GetId() == 0)
            {
                StartCoroutine(PickupSpeed(other.gameObject));
            }
            else if (powerupeffect.GetId() == 1)
            {
                StartCoroutine(PickupKick(other.gameObject));
            }
            else if (powerupeffect.GetId() == 2)
            {
                StartCoroutine(PickupSlow(other.gameObject));
            }
            else if (powerupeffect.GetId() == 3)
            {
                StartCoroutine(PickupStick(other.gameObject));
            }
            else if (powerupeffect.GetId() == 4)
            {
                StartCoroutine(PickupFreeze(other.gameObject));
            }
        }
    }

    IEnumerator PickupSpeed(GameObject other)
    {
        DeactivatePowerup();
        powerupeffect.Apply(other);
        yield return new WaitForSeconds(duration);
        powerupeffect.remove(other);
        Destroy(gameObject);
    }

    IEnumerator PickupKick(GameObject other)
    {
        DeactivatePowerup();
        powerupeffect.Apply(other);
        yield return new WaitUntil(MIsPressed);
        powerupeffect.remove(other);
        Destroy(gameObject);
    }

    IEnumerator PickupSlow(GameObject other)
    {
        DeactivatePowerup();
        powerupeffect.Apply(other);
        yield return new WaitForSeconds(duration);
        powerupeffect.remove(other);
        Destroy(gameObject);
    }

    IEnumerator PickupStick(GameObject other)
    {
        DeactivatePowerup();
        powerupeffect.Apply(other);
        yield return new WaitUntil(other.GetComponent<stickScript2>().touchingBall);
        yield return new WaitForSeconds(duration);

        powerupeffect.remove(other);
        Destroy(gameObject);
    }

    IEnumerator PickupFreeze(GameObject other)
    {
        DeactivatePowerup();
        powerupeffect.Apply(other);
        yield return new WaitForSeconds(duration);
        Debug.Log("WeAreTesting");
        powerupeffect.remove(other);
        Destroy(gameObject);
    }

    public bool MIsPressed()
    {
        return Input.GetKeyDown(KeyCode.M);
    }
    public void DeactivatePowerup()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

    }

}
