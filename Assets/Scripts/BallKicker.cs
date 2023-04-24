using UnityEngine;
public class BallKicker : MonoBehaviour
{
    public float kickforce = 80;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && Input.GetKey(KeyCode.M))
        {
            Vector3 centre = other.gameObject.transform.position;
            Vector3 contact = other.ClosestPointOnBounds(transform.position);
            Vector3 appliedforce = (centre - contact) * kickforce;

            other.attachedRigidbody.AddForce(appliedforce);
            Debug.Log(" print please" + appliedforce);
            kickforce = 80;
        }
    }

}