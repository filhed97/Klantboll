using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour
{
    Rigidbody rb;
    public stickScript2 stick;
    [SerializeField] GameObject goalExplosionTeam;
    private ParticleSystem goalPS;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.position = new Vector3(0,1,0);
        goalPS = goalExplosionTeam.GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Goal") && TieBreakerScript.endOfMatch == 0) {
            stick.unstick();

            //Goal explosion effect
            GameObject particleSystemInstance = Instantiate(goalExplosionTeam, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleSystemInstance.GetComponent<ParticleSystem>();
            particleSystem.Play();

            rb.Sleep();
            rb.position = new Vector3(0,1,0);
            rb.velocity = Vector3.zero;
            rb.WakeUp();
        }
    }
}
