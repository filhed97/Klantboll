using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Transform target;
    public GameObject bodypartWithJanneRagdoll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!bodypartWithJanneRagdoll.GetComponent<JanneRagdollOnKick>().getRagdolled())
        {
            Quaternion rotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = rotation;
        }
        
    }
}
