using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to control player movement speed
    public int currentRow = 2; // Starting row (assuming rows are indexed from 0 to 4)

    // Update is called once per frame
    void Update()
    {

        // Clamp player's position within the screen bounds
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -8f, 8f); // Adjust these values according to your screen size
        transform.position = clampedPosition;

        // Check for column switching input
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentRow < 4) // Check if the player is not in the rightmost column
            {
                currentRow++; // Move the player to the right column
                UpdatePosition();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentRow > 0) // Check if the player is not in the leftmost column
            {
                currentRow--; // Move the player to the left column
                UpdatePosition();
            }
        }
    }

    // Update player's position based on the current row
    void UpdatePosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = (currentRow - 2) * 1.5f; // Adjust the '2' according to the number of columns and multiply by row separation
        transform.position = newPosition;
    }
}
