using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Tooltip("The name of the scene to load")]
    public string sceneName;

    [Tooltip("The minute at which to change the scene")]
    public int changeMinute;

    [Tooltip("The second at which to change the scene")]
    public int changeSecond;

    private float startTime;

    private void Start()
    {
        // Store the start time when the scene is loaded
        startTime = Time.time;
        StartCoroutine(ChangeSceneAtSpecificTime());
    }

    IEnumerator ChangeSceneAtSpecificTime()
    {
        // Calculate the total seconds to wait
        float waitTime = (changeMinute * 60) + changeSecond;

        while (true)
        {
            // Calculate elapsed time
            float elapsedTime = Time.time - startTime;

            // Check if it's time to change the scene
            if (elapsedTime >= waitTime)
            {
                // Change the scene
                SceneManager.LoadScene(sceneName);
                yield break;
            }

            // Wait for the next frame before checking again
            yield return null;
        }
    }
}
