using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectators : MonoBehaviour
{
    public float jumpHeight = 2f;   // The height of the jump
    public float jumpTime = 1f;     // The time it takes to complete one jump cycle
    public bool jumping = false;    // Flag to indicate if the character is currently jumping

    private Vector3 startPos;       // The starting position of the character
    private float elapsedTime = 0f; // The amount of time elapsed since the start of the jump

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (!jumping)
        {
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        jumping = true;

        float halfTime = jumpTime / 2f;
        float jumpVelocity = (jumpHeight * 2f) / halfTime;

        while (elapsedTime < jumpTime)
        {
            float yPos = startPos.y + (jumpVelocity * (elapsedTime - halfTime) * (elapsedTime - halfTime - jumpTime));
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
        elapsedTime = 0f;
        jumping = false;
    }
}
