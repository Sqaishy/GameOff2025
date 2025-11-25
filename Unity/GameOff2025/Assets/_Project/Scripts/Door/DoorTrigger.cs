using System;
using UnityEngine;
/// <summary>
/// This class is responsible for triggering door-related events. This is a temporary solution for opening and closing the doors.
/// 
/// Contains events for door open and close actions to which other scripts can subscribe and react to the event.
/// </summary>
public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _doorObject;
    [SerializeField] private GameObject _doorframeObject;
    // todo: add locked doors functionality
    private Animator _doorAnimator;
    // Events for subscribing to door open/close actions
    // Usefull for sound effects or other interactions
    public event Action OnDoorOpened;
    public event Action OnDoorClosed;

    void OnEnable()
    {
        _doorAnimator = _doorframeObject.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Creature"))
        {
            _doorAnimator.SetTrigger("openDoor");
            _doorObject.GetComponent<Collider>().enabled = false;
            OnDoorOpened?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Creature"))
        {
            _doorAnimator.SetTrigger("closeDoor");
            _doorObject.GetComponent<Collider>().enabled = true;
            OnDoorClosed?.Invoke();
        }
    }
}
