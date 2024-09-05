[System.Serializable]
public class Conversations
{
    public ConversationData[] conversations;
}


[System.Serializable]
public class ConversationData
{
    public DialogueTurn[] dialogueTurns;
    public string conversationName;
    public string nextConversation;
    public bool oneShot;
}


[System.Serializable]
public class DialogueTurn
{
    // starts at 1
    public int id;
    public string speakerName;
    public QuoteData[] quotes;
    public DialogueOption[] options;

    // 0 - do not automatically advance
    public int autoAdvanceToID;
}


[System.Serializable]
public class QuoteData
{
    public int emotion;
    public string quote;

    // call a function
    public string effect;
}


[System.Serializable]
public class DialogueOption
{
    // -1 means end the dialogue
    public int toDialogueId;
    public string option;

    // call a function
    public string effect;
}
