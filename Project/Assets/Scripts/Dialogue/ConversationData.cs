[System.Serializable]
public class Conversations
{
    public ConversationData[] conversations;

    public Conversations(ConversationData[] conversations)
    {
        this.conversations = conversations;
    }
}


[System.Serializable]
public class ConversationData
{
    public DialogueTurn[] dialogueTurns;
    public string conversationName;
    public string nextConversation;
    public bool oneShot;

    public ConversationData(DialogueTurn[] dialogueTurns, string conversationName, string nextConversation, bool oneShot)
    {
        this.dialogueTurns = dialogueTurns;
        this.conversationName = conversationName;
        this.nextConversation = nextConversation;
        this.oneShot = oneShot;
    }
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

    public DialogueTurn(int id, string speakerName, QuoteData[] quotes, DialogueOption[] options, int autoAdvanceToID)
    {
        this.id = id;
        this.speakerName = speakerName;
        this.quotes = quotes;
        this.options = options;
        this.autoAdvanceToID = autoAdvanceToID;
    }
}


[System.Serializable]
public class QuoteData
{
    public int emotion;
    public string quote;

    // call a function
    public string effect;

    public QuoteData(int emotion, string quote, string effect)
    {
        this.emotion = emotion;
        this.quote = quote;
        this.effect = effect;
    }
}


[System.Serializable]
public class DialogueOption
{
    // -1 means end the dialogue
    public int toDialogueId;
    public string option;

    // call a function
    public string effect;

    public DialogueOption(int toDialogueId, string option, string effect)
    {
        this.toDialogueId = toDialogueId;
        this.option = option;
        this.effect = effect;
    }
}

public enum Emotion
{
    None,
    Excited,
    Angry
}