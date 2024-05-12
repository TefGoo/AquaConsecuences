using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public BulletSpawner[] spawners;
    public float delayBeforeActivation = 30f; // Delay before activating the spawners

    private bool spawnersActivated = false;

    void Start()
    {
        // Deactivate all spawners at the start
        foreach (BulletSpawner spawner in spawners)
        {
            spawner.enabled = false;
        }

        // Start the coroutine to activate the spawners after a delay
        StartCoroutine(ActivateSpawnersAfterDelay());
    }

    IEnumerator ActivateSpawnersAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeActivation);

        // Activate all spawners after the delay
        foreach (BulletSpawner spawner in spawners)
        {
            spawner.enabled = true;
        }

        spawnersActivated = true;
    }
}
