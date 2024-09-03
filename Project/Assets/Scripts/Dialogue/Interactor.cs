using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] TMP_Text pressEText;

    List<IInteractable> interactables;
    IInteractable currentInteraction;

    private void Awake()
    {
        interactables = new List<IInteractable>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        IInteractable interactable = collider.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactables.Add(interactable);
            SetCurrentInteraction(interactable);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        IInteractable interactable = collider.GetComponent<IInteractable>();

        if (interactable != null)
        {
            if (interactable == currentInteraction)
                SetCurrentInteraction(null);

            interactables.Remove(interactable);
        }
    }

    void SetCurrentInteraction(IInteractable interactable)
    {
        currentInteraction = interactable;

        if (interactable == null)
            pressEText.gameObject.SetActive(false);
        else
            pressEText.gameObject.SetActive(true);
    }

    public void TryInteract()
    {
        if (currentInteraction != null)
        {
            currentInteraction.Interact();
            interactables.Remove(currentInteraction);

            if (interactables.Count > 0)
                SetCurrentInteraction(interactables[0]);
            else
                SetCurrentInteraction(null);
        }
    }
}
