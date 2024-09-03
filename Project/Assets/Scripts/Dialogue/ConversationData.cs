using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConversationData
{
    public DialogueTurn[] dialogueTurns;
    public string conversationName;
}

[System.Serializable]
public class Conversations
{
    public ConversationData[] conversations;
}