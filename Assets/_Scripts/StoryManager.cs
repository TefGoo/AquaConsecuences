using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // Add this for changing scenes

public class StoryManager : MonoBehaviour
{
    public Transform imageTransform; // Transform of the first image to move
    public Vector3 pointB; // Destination position for the first image
    public float moveSpeed = 1f; // Speed of the first image movement

    public Transform secondImageTransform; // Transform of the second image to move
    public Vector3 secondPointB; // Destination position for the second image
    public float secondMoveSpeed = 1f; // Speed of the second image movement
    public float secondImageDelay = 2f; // Delay before the second image starts moving

    public Transform thirdImageTransform; // Transform of the third image to move
    public Vector3 thirdPointB; // Destination position for the third image
    public float thirdMoveSpeed = 1f; // Speed of the third image movement

    public GameObject delayedGameObject; // GameObject to activate after a delay
    public float gameObjectActivationDelay = 3f; // Delay before activating the GameObject

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
        // Start moving the first image from point A to point B
        StartCoroutine(MoveImage(imageTransform, pointB, moveSpeed));

        // Start moving the second image after a delay
        StartCoroutine(DelayedMoveImage(secondImageTransform, secondPointB, secondMoveSpeed, secondImageDelay));

        // Start moving the third image from point A to point B
        StartCoroutine(MoveImage(thirdImageTransform, thirdPointB, thirdMoveSpeed));

        // Deactivate the delayed GameObject initially
        delayedGameObject.SetActive(false);
        // Activate the GameObject after a delay
        StartCoroutine(ActivateGameObjectAfterDelay(delayedGameObject, gameObjectActivationDelay));

        // Example dialogue initialization, replace with your own dialogue
        dialogueQueue.Enqueue("First line of dialogue...");
        dialogueQueue.Enqueue("Second line of dialogue...");
        dialogueQueue.Enqueue("Third line of dialogue...");

        // Initialize the first dialogue
        UpdateDialogue();
    }

    IEnumerator MoveImage(Transform image, Vector3 destination, float speed)
    {
        Vector3 startPoint = image.position;

        while (image.position != destination)
        {
            image.position = Vector3.MoveTowards(image.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        // Image has reached its destination, stay there
        image.position = destination;
    }

    IEnumerator DelayedMoveImage(Transform image, Vector3 destination, float speed, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(MoveImage(image, destination, speed));
    }

    IEnumerator ActivateGameObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }

    public void NextPartOfStory()
    {
        // Check if text is fully revealed before allowing next dialogue
        if (!isTextRevealed)
        {
            // Fully reveal text
            storyText.maxVisibleCharacters = storyText.text.Length;
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
            StartCoroutine(RevealText(dialogueQueue.Peek()));

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
            ChangeScene(); // Call to change the scene
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

    IEnumerator RevealText(string text)
    {
        storyText.text = text;
        int totalVisibleCharacters = 0;

        while (totalVisibleCharacters < text.Length)
        {
            totalVisibleCharacters++;
            storyText.maxVisibleCharacters = totalVisibleCharacters;
            yield return new WaitForSeconds(0.05f); // Adjust speed of text reveal
        }

        // Text fully revealed
        isTextRevealed = true;
    }

    private void ChangeScene()
    {
        // Change to the next scene (replace "NextSceneName" with your scene name)
        SceneManager.LoadScene("Level1");
    }
}
