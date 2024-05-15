using UnityEngine;
using System.Collections;

public class PanelMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float distanceToMove = 5f;
    public float pauseTime = 1f;

    private Vector3 originalPosition;
    private bool movingRight = true;

    private void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MovePanel());
    }

    private IEnumerator MovePanel()
    {
        while (true)
        {
            float targetX = movingRight ? originalPosition.x - distanceToMove : originalPosition.x;
            Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(pauseTime);

            // Teleport back to the original position
            transform.position = originalPosition;

            movingRight = !movingRight;
        }
    }
}
