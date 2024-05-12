using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeleportationEvent
{
    public Vector3 targetPosition;
    public float timeToTeleport;
}

public class MovingManager : MonoBehaviour
{
    public Transform objectToMove;
    public List<TeleportationEvent> teleportationEvents = new List<TeleportationEvent>();

    private void Start()
    {
        StartCoroutine(TeleportObject());
    }

    IEnumerator TeleportObject()
    {
        foreach (var teleportEvent in teleportationEvents)
        {
            yield return new WaitForSeconds(teleportEvent.timeToTeleport);

            if (objectToMove != null)
            {
                objectToMove.position = teleportEvent.targetPosition;
            }
            else
            {
                Debug.LogError("Object to move is not assigned!");
            }
        }
    }
}
