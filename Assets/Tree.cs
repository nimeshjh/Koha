using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public float topY = 5f; // Maximum Y (top)
    public float bottomY = -5f; // Minimum Y (bottom)

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set initial position within bounds
        SetRandomYPosition();
    }

    void SetRandomYPosition()
    {
        // Generate a random Y value between bottomY and topY
        float randomY = Random.Range(bottomY, topY);

        // Update the position of the sprite
        transform.position = new Vector3(transform.position.x, randomY, transform.position.z);
    }

    // Optional: Call this function in Update to keep moving the sprite randomly.
    void Update()
    {
        // If you want to keep changing its position every frame or after a delay
        if (Time.frameCount % 60 == 0) // Example: change position every 60 frames
        {
            SetRandomYPosition();
        }
    }
}
