using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] Transform leftDoor;
    [SerializeField] Transform rightDoor;
    [SerializeField] float openAmount;
    [SerializeField] float openSpeed;
    [SerializeField] float overreach;
    bool isManual;

    List<Collider> cols;

    private void Start()
    {
        cols = new List<Collider>();
    }

    public void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(MoveDoor(openAmount + overreach));
    }

    public void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(MoveDoor(-overreach));
    }

    public void SetManual() => isManual = true;

    IEnumerator MoveDoor(float openAmount)
    {
        while (Mathf.Abs(leftDoor.transform.localPosition.z - openAmount) > overreach)
        {
            rightDoor.localPosition = new Vector3(0, 0, Mathf.Lerp(rightDoor.localPosition.z, -openAmount, Time.deltaTime * openSpeed));
            leftDoor.localPosition = new Vector3(0, 0, Mathf.Lerp(leftDoor.localPosition.z, openAmount, Time.deltaTime * openSpeed));
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isManual) return;

        if (cols.Count == 0)
            OpenDoor();

        cols.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (isManual) return;

        cols.Remove(other);

        if (cols.Count == 0)
            CloseDoor();
    }
}
