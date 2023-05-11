using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JannePowerup2 : MonoBehaviour
{
    public float duration = 3f;
    public PowerupEffects2 powerupeffect;
    public static int numOfPowerups;
    //public GameObject powerupIcon;
    [SerializeField] GameObject pickupeffect;


    private void OnTriggerEnter(Collider playerCollider)
    {
        //Debug.Log(other);
        
        if (playerCollider.CompareTag("Player") && !playerCollider.gameObject.transform.root.GetComponent<powerupCheck>().hasPowerup)
        {
            numOfPowerups--;
            Debug.Log("Power picked up");

            if (powerupeffect.GetId() == 0)
            {
                
                StartCoroutine(PickupSpeed(playerCollider.gameObject));
                
            }
            else if (powerupeffect.GetId() == 1)
            {
                Debug.Log("Collided with: " + playerCollider.gameObject.name);
                
                StartCoroutine(PickupKick(playerCollider.gameObject));
                
            }
            else if (powerupeffect.GetId() == 2)
            {
                
                StartCoroutine(PickupSlow(playerCollider.gameObject));
                
            }
            else if (powerupeffect.GetId() == 3)
            {
                
                StartCoroutine(PickupStick(playerCollider.gameObject));
                
            }
            else if (powerupeffect.GetId() == 4)
            {
                
                StartCoroutine(PickupFreeze(playerCollider.gameObject));
                
            }
            else if (powerupeffect.GetId() == 5)
            {
                
                StartCoroutine(PickupShield(playerCollider.gameObject));
                
            }
            else if (powerupeffect.GetId() == 6)
            {

                StartCoroutine(PickupSpeed(playerCollider.gameObject));

            }
        }
        else if (playerCollider.CompareTag("AI-character") && !playerCollider.transform.root.GetComponent<powerupCheck>().hasPowerup)
        {
            numOfPowerups--;

            if (powerupeffect.GetId() == 0)
            {

                StartCoroutine(PickupSpeed(playerCollider.gameObject));
            }
            else if (powerupeffect.GetId() == 1)
            {
                StartCoroutine(PickupKick(playerCollider.gameObject));
            }
            else if (powerupeffect.GetId() == 2)
            {
                StartCoroutine(PickupSlow(playerCollider.gameObject));
            }
            else if (powerupeffect.GetId() == 3)
            {
                Debug.Log("Inide sticker");
                StartCoroutine(PickupStick(playerCollider.gameObject));
            }
            else if (powerupeffect.GetId() == 4)
            {
                StartCoroutine(PickupFreeze(playerCollider.gameObject));
            }
            else if (powerupeffect.GetId() == 5)
            {
                StartCoroutine(PickupShield(playerCollider.gameObject));
            }
            else if (powerupeffect.GetId() == 6)
            {
                StartCoroutine(PickupSpeed(playerCollider.gameObject));
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
        yield return new WaitUntil(SpaceIsPressed);
        
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
        
        powerupeffect.remove(other);
        Destroy(gameObject);

    }

    IEnumerator PickupShield(GameObject other)
    {
        DeactivatePowerup();
        
        powerupeffect.Apply(other);
        yield return new WaitForSeconds(duration);
        Debug.Log("WeAreTesting");
        
        powerupeffect.remove(other);
        Destroy(gameObject);


    }




    public bool SpaceIsPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public void DeactivatePowerup()
    {
        Debug.Log("remove");
        Instantiate(pickupeffect, transform.position, transform.rotation);
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
    }

   /* public void EnableIcon()
    {
        powerupIcon.GetComponent<Image>().enabled = true;
    }
    public void DisableIcon()
    {
        powerupIcon.GetComponent<Image>().enabled = false;
    }*/

}
