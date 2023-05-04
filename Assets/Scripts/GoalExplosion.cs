using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalExplosion : MonoBehaviour
{
    [SerializeField] GameObject goalExplosion;
    private ParticleSystem goalPS;

    void Start()
    {
        goalPS = goalExplosion.GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Goal") && TieBreakerScript.endOfMatch == 0) {

            GameObject particleSystemInstance = Instantiate(goalExplosion, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleSystemInstance.GetComponent<ParticleSystem>();
            particleSystem.Play();
        }
    }
}
