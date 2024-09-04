using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHaver : MonoBehaviour, IInteractable
{
    [SerializeField] Collider dialogueTriggerer;
    [SerializeField] string conversationName;
    ConversationData conversationData;

    private void Start()
    {
        conversationData = DialogueParser.I.GetConversation(conversationName);
    }

    public void Interact()
    {
        DialogueDisplayer.I.StartConversation(conversationData, this);

        if (conversationData.oneShot == true)
            SetConversationData(null);
    }

    public void SetConversationData(ConversationData conversationData)
    {
        this.conversationData = conversationData;

        if (conversationData == null)
            Disable();
    }

    void Disable()
    {
        dialogueTriggerer.enabled = false;
    }
}
