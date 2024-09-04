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
    public int id;
    public string speakerName;
    public QuoteData[] quotes;
    public DialogueOption[] options;
    // Whether or not to automatically advance to the next dialogue turn
    public bool autoAdvance;
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
    public int toDialogueId;
    public string option;

    // call a function
    public string effect;
}
