using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTerminal : MonoBehaviour, IInteractable
{
    [SerializeField] SlidingDoor door;

    void Awake()
    {
        door.SetManual();
    }

    public void Interact()
    {
        StopAllCoroutines();
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        door.OpenDoor();
        yield return new WaitForSeconds(3);
        door.CloseDoor();
    }
}
