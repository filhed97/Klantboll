using UnityEngine;
public class BallKicker : MonoBehaviour
{
    public float kickforce = 200;
    private AudioSource KickingBallSound;

    void Start()
    {
        KickingBallSound = GetComponent<AudioSource> ();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && Input.GetKey(KeyCode.M))
        {
            Vector3 centre = other.gameObject.transform.position;
            Vector3 contact = other.ClosestPointOnBounds(transform.position);
            Vector3 appliedforce = (centre - contact) * kickforce;

            other.attachedRigidbody.AddForce(appliedforce);
            KickingBallSound.Play();
        }
    }

}