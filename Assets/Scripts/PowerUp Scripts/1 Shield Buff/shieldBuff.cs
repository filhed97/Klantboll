using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/ShieldBuff")]
public class shieldBuff : PowerupEffects2
{
    public GameObject shieldPrefab;
    public int id = 5;
    private GameObject shieldInstance;

    public override void Apply(GameObject target)
    {
        if(target.CompareTag("Player"))
        {    
            target.GetComponent<PlayerMove>().hasPowerup = true;
        
            if (shieldInstance == null)
            {
                Rigidbody hips = target.GetComponent<Rigidbody>();
                float shieldDistance = 2f; 
                float shieldHeightOffset = 2f;
                Vector3 shieldPosition = hips.transform.position + hips.transform.forward * shieldDistance + Vector3.up * shieldHeightOffset;
                Quaternion shieldRotation = hips.transform.rotation * Quaternion.Euler(90, 0, 0);
                shieldInstance = Instantiate(shieldPrefab, shieldPosition, shieldRotation, hips.transform);
            }
        }
        else
        {
            target.GetComponent<AIScript>().hasPowerup = true;
        
            if (shieldInstance == null)
            {
                Rigidbody hips = target.GetComponent<Rigidbody>();
                float shieldDistance = 2f; 
                float shieldHeightOffset = 2f;
                Vector3 shieldPosition = hips.transform.position + hips.transform.forward * shieldDistance + Vector3.up * shieldHeightOffset;
                Quaternion shieldRotation = hips.transform.rotation * Quaternion.Euler(90, 0, 0);
                shieldInstance = Instantiate(shieldPrefab, shieldPosition, shieldRotation, hips.transform);
            }
        }
    }

    public override void remove(GameObject target)
    {
        if(target.CompareTag("Player"))
        {
            target.GetComponent<PlayerMove>().hasPowerup = false;
            
            if (shieldInstance != null)
            {
                Destroy(shieldInstance);
                shieldInstance = null;
            }  
        }
        else 
        {
            target.GetComponent<AIScript>().hasPowerup = false;
            
            if (shieldInstance != null)
            {
                Destroy(shieldInstance);
                shieldInstance = null;
            }  
        }
    }

        public override int GetId()
    {
        return id;
    }
}