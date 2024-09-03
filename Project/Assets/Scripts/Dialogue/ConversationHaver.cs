using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHaver : MonoBehaviour, IInteractable
{
    [SerializeField] string conversationName;

    public string ConversationName => conversationName;

    public void Interact()
    {
        DialogueDisplayer.I.StartConversation(conversationName);
    }
}
