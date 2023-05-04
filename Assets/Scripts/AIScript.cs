using UnityEngine;

public class AIScript : MonoBehaviour
{
    public Transform ball;
    public float ballDistanceThreshold = 50f;
    public float movementSpeed = 7f;
    public float rotationSpeed = 10f;

    public Rigidbody hips;
    public bool isGrounded;
    public bool hasPowerup = false;

    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Determine if the ball is close enough to the player
        float distanceToBall = Vector3.Distance(transform.position, ball.position);
        bool isBallClose = distanceToBall <= ballDistanceThreshold;

        // If the ball is close, move towards it
        if (isBallClose)
        {
            Vector3 lookDir = ball.position - transform.position;
            lookDir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            Vector3 movementDir = lookDir.normalized;
            hips.velocity = movementDir * movementSpeed;
        }
        else
        {
            // Move back and forth using WASD
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDir = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            hips.velocity = movementDir * movementSpeed;
        }
    }

    public Transform goal;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ball.gameObject)
        {
            // Determine the direction to kick the ball (towards the goal position)
            Vector3 kickDir = (goal.position - ball.position).normalized;
            
            // Apply force to the ball to kick it
            Rigidbody ballRb = ball.GetComponent<Rigidbody>();
            ballRb.AddForce(kickDir * 1000f);

            // Log a message to the console to indicate that the player has kicked the ball
            //Debug.Log("Player kicked the ball towards the goal!");
        }
    }
}
