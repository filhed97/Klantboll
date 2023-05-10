using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MPPowerup2 : NetworkBehaviour
{
    public float duration = 3f;
    public PowerupEffects2 powerupeffect;
    public static int numOfPowerups;
    [SerializeField] GameObject pickupeffect;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        
        if (other.CompareTag("Player") && !other.GetComponent<NetworkMultiplayer>().hasPowerup.Value)
        {
            numOfPowerups--;

            if (powerupeffect.GetId() == 0)
            {
                StartCoroutine(PickupSpeed(other.gameObject));
            }
            else if (powerupeffect.GetId() == 1)
            {
                Debug.Log("KICK");
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
        Debug.Log("KICK");
        DeactivatePowerup();
        powerupeffect.Apply(other);
        //yield return new WaitUntil(MIsPressed);
        yield return new WaitForSeconds(duration);
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
        Instantiate(pickupeffect, transform.position, transform.rotation);
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
    }
}
