using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float scrollSpeed = 2f;  // Speed at which the background moves
    private float width;            // Width of the background
    private Vector3 startPosition;  // Starting position of the background

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;  // Get background width
        startPosition = transform.position; // Save the starting position
    }

    void Update()
    {
        // Move the background to the left
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // Once the background has moved off the screen, reset its position
        if (transform.position.x <= startPosition.x - width)
        {
            transform.position = startPosition; // Reset position
        }
    }
}
