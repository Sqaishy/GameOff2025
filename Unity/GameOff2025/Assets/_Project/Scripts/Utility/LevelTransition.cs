using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
/**
* This class handles the Teleportation of the Player between two points. Mostly used for transitioning in the submarine.
*/
public class LevelTransition : MonoBehaviour
{
    [SerializeField] private GameObject destination;
    // For locking it until a condition is met.
    // [SerializeField] private bool isLocked = false;
    [SerializeField] private bool debugMode = false;

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void OnTriggerEnter(Collider other)
    {
        if (debugMode)
        {
            Debug.Log("Player entered Teleporter... Waiting 3 seconds.");
        }
        if (other.CompareTag("Creature") && (other.GetComponent<PlayerInput>() != null))
        {
            StartCoroutine(delayedTeleport(other.gameObject));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Creature") && (other.GetComponent<PlayerInput>() != null))
        {
            StopAllCoroutines();
        }
    }

    IEnumerator delayedTeleport(GameObject sourceObject)
    {
        yield return new WaitForSeconds(3);
        teleport(sourceObject, destination);
    }
    
    private void teleport(GameObject sourceObject, GameObject destination)
    {
        sourceObject.transform.position = destination.transform.position;
        if (debugMode)
        {
            Debug.Log("Player teleported to: " + destination.transform.position);
        }
    }
}
