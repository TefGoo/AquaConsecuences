using UnityEngine;

public class BulletSpawnerManager : MonoBehaviour
{
    public delegate void SpawnerActivated();
    public event SpawnerActivated OnSpawnerActivated;

    public delegate void SpawnerDeactivated();
    public event SpawnerDeactivated OnSpawnerDeactivated;

    void OnEnable()
    {
        // Invoke the event when the spawner is activated
        if (OnSpawnerActivated != null)
        {
            OnSpawnerActivated();
        }
    }

    void OnDisable()
    {
        // Invoke the event when the spawner is deactivated
        if (OnSpawnerDeactivated != null)
        {
            OnSpawnerDeactivated();
        }
    }
}