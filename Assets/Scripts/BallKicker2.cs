using UnityEngine;
public class BallKicker2 : MonoBehaviour
{
    public float kickforce = 500;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") && Input.GetKey(KeyCode.C))
        {
            Vector3 centre = other.gameObject.transform.position;
            Vector3 contact = other.ClosestPointOnBounds(transform.position);
            Vector3 appliedforce = (centre - contact) * kickforce;

            other.attachedRigidbody.AddForce(appliedforce);
            Debug.Log(" print please" + appliedforce);
        }
    }

}