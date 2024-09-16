using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueDisplayer : Singleton<DialogueDisplayer>
{
    //public event System.Action StartedTalking;
    //public event System.Action StoppedTalking;

    [SerializeField] DialogueOptionHandler dialogueOptionHandler;
    [SerializeField] DialogueParser dialogueParser;

    [SerializeField] GameObject dialogueVisuals;
    [SerializeField] TMP_Text conversationNameText;
    [SerializeField] TMP_Text quoteText;
    [SerializeField] TMP_Text speakerNameText;
    [SerializeField] int framesBetweenTyping;

    public ConversationHaver TalkingTo { get; private set; }
    public bool Talking { get; private set; }

    string currentQuote;
    bool isTyping;

    Dictionary<int, DialogueTurn> idToDialogue;
    ConversationData conversationData;
    DialogueTurn currentTurn;
    int currentDialogueID = 1;
    int currentQuoteIndex;

    public void StartConversation(ConversationData conversationData, ConversationHaver talkingTo)
    {
        Talking = true;
        this.TalkingTo = talkingTo;
        this.conversationData = conversationData;

        currentDialogueID = 1;
        idToDialogue = new Dictionary<int, DialogueTurn>();

        for (int i = 0; i < conversationData.dialogueTurns.Length; i++)
        {
            idToDialogue.Add(conversationData.dialogueTurns[i].id, conversationData.dialogueTurns[i]);
        }

        if (this.conversationData == null)
        {
            EndConversation();
            return;
        }

        conversationNameText.text = this.conversationData.conversationName;

        currentTurn = idToDialogue[currentDialogueID];
        Display();

        dialogueVisuals.SetActive(true);
    }

    public void AdvanceDialogue()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            quoteText.text = currentQuote;
            isTyping = false;
            return;
        }

        if (dialogueOptionHandler.Choosing) return;

        AdvanceQuote();

        // If we didn't just end the conversation, display it.
        if (Talking && dialogueOptionHandler.Choosing == false)
            Display();
    }

    void AdvanceTurn()
    {
        // Simply advance to the next dialogue turn
        if (currentTurn.autoAdvanceToID != 0)
        {
            currentDialogueID = currentTurn.autoAdvanceToID;
            currentTurn = idToDialogue[currentDialogueID];
        }
        // Try to choose options
        else
        {
            if (currentTurn.options.Length > 0)
                StartChoosing();
            else
                EndConversation();
        }
    }

    void AdvanceQuote()
    {
        currentQuoteIndex++;

        // We are done with this quote
        if (currentQuoteIndex >= currentTurn.quotes.Length)
        {
            currentQuoteIndex = 0;
            AdvanceTurn();
        }
    }

    void StartChoosing()
    {
        quoteText.text = "";
        dialogueOptionHandler.StartChoosing(currentTurn.options);
    }

    public void ChoseOption(DialogueOption option)
    {
        if (option.toDialogueId == -1)
        {
            EndConversation();
            return;
        }

        currentDialogueID = option.toDialogueId;
        currentTurn = idToDialogue[currentDialogueID];
        Display();
    }

    void Display()
    {
        speakerNameText.text = currentTurn.speakerName;

        currentQuote = currentTurn.quotes[currentQuoteIndex].quote;

        StopAllCoroutines();
        StartCoroutine(TypeQuote());
    }


    IEnumerator TypeQuote()
    {
        isTyping = true;
        quoteText.text = "";
        foreach (char letter in currentQuote.ToCharArray())
        {
            quoteText.text += letter;

            for (int i = 0; i < framesBetweenTyping; i++)
                yield return null;
        }

        isTyping = false;
    }

    void EndConversation()
    {
        currentDialogueID = 1;
        currentQuoteIndex = 0;

        Talking = false;
        dialogueVisuals.SetActive(false);

        string nextConversation = conversationData.nextConversation;

        if (nextConversation != "" && nextConversation != string.Empty && nextConversation != null)
            TalkingTo.SetConversationData(dialogueParser.GetConversation(nextConversation));

        TalkingTo = null;
        currentTurn = null;
        conversationData = null;
    }
}
