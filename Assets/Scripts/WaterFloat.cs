using UnityEngine;

public class WaterFloat : MonoBehaviour
{
    private float amplitude = 1f; // The height of the oscillation
    private float frequency = 2f;  // The speed of the oscillation

    // Initial position of the GameObject
    private Vector3 startPosition;

    private void Start()
    {
        // Store the initial position of the GameObject
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new Y position based on a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Update the GameObject's position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
