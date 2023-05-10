using UnityEngine;
public class BallKickerMP : MonoBehaviour
{
    public float kickforce;
    private AudioSource KickingBallSound;

    void Start()
    {
        KickingBallSound = GetComponent<AudioSource> ();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && Input.GetKey(KeyCode.Space))
        {
            Vector3 centre = other.gameObject.transform.position;
            Vector3 contact = other.ClosestPointOnBounds(transform.position);
            Vector3 appliedforce = (centre - contact) * kickforce;

            other.attachedRigidbody.AddForce(appliedforce);
            gameObject.GetComponent<NetworkMultiplayer>().hasPowerup.Value = false;
            KickingBallSound.Play();
        }
    }

}