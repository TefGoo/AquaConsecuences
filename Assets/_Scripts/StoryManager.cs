using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class StoryManager : MonoBehaviour
{
    public Transform imageTransform; // Transform of the image to move
    public Vector3 pointB; // Destination position for the image
    public float moveSpeed = 1f; // Speed of the image movement

    public Button nextButton; // Button to trigger the next part of the story
    public TMP_Text storyText; // Text component to display the story

    public Animator character1Animator; // Animator for character 1
    public Animator character2Animator; // Animator for character 2

    private Queue<string> dialogueQueue = new Queue<string>(); // Queue of dialogue strings
    private Animator currentCharacterAnimator; // Reference to the current speaking character
    private bool isAnimating = false; // Flag to track if animation is playing
    private bool isTextRevealed = false; // Flag to track if text is fully revealed

    private void Start()
    {
        // Start moving the image from point A to point B
        StartCoroutine(MoveImage());

        // Example dialogue initialization, replace with your own dialogue
        dialogueQueue.Enqueue("First line of dialogue...");
        dialogueQueue.Enqueue("Second line of dialogue...");
        dialogueQueue.Enqueue("Third line of dialogue...");

        // Initialize the first dialogue
        UpdateDialogue();
    }

    IEnumerator MoveImage()
    {
        Vector3 pointA = imageTransform.position;

        while (imageTransform.position != pointB)
        {
            imageTransform.position = Vector3.MoveTowards(imageTransform.position, pointB, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Image has reached point B, stay there
        imageTransform.position = pointB;
    }

    public void NextPartOfStory()
    {
        // Check if text is fully revealed before allowing next dialogue
        if (!isTextRevealed)
        {
            // Fully reveal text
            isTextRevealed = true;
        }
        else
        {
            // Remove the current dialogue from the queue
            dialogueQueue.Dequeue();

            // Update the dialogue
            UpdateDialogue();
        }
    }

    private void UpdateDialogue()
    {
        // Check if there is more dialogue to display
        if (dialogueQueue.Count > 0)
        {
            // Update the text with the next dialogue
            storyText.text = dialogueQueue.Peek();

            // Reset text reveal flag
            isTextRevealed = false;

            // Assign the character animator based on the current dialogue
            if (dialogueQueue.Count % 2 == 1)
            {
                currentCharacterAnimator = character1Animator;
            }
            else
            {
                currentCharacterAnimator = character2Animator;
            }

            // Start the animation for the current character
            if (!isAnimating)
            {
                StartCoroutine(PlayAnimation());
            }
        }
        else
        {
            // No more dialogue, display end message or perform other actions
            Debug.Log("End of dialogue!");
        }
    }

    IEnumerator PlayAnimation()
    {
        isAnimating = true;

        // Play the animation for the current character
        currentCharacterAnimator.SetBool("IsTalking", true);

        // Wait for a short duration to simulate talking
        yield return new WaitForSeconds(2f);

        // Stop the animation
        currentCharacterAnimator.SetBool("IsTalking", false);

        isAnimating = false;
    }
}