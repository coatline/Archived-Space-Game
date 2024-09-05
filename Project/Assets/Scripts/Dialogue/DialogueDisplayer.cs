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

    public ConversationHaver TalkingTo { get; private set; }
    public bool Talking { get; private set; }

    ConversationData conversationData;
    DialogueTurn currentTurn;
    int currentQuoteIndex;
    int dialogueTurnIndex;

    public void StartConversation(ConversationData conversationData, ConversationHaver talkingTo)
    {
        Talking = true;
        this.TalkingTo = talkingTo;
        this.conversationData = conversationData;
        //dialogueParser.GetConversation(conversationData);

        if (this.conversationData == null)
        {
            EndConversation();
            return;
        }

        conversationNameText.text = this.conversationData.conversationName;

        currentTurn = this.conversationData.dialogueTurns[dialogueTurnIndex];
        Display();

        dialogueVisuals.SetActive(true);
    }

    public void AdvanceDialogue()
    {
        if (dialogueOptionHandler.Choosing) return;

        AdvanceQuote();

        // If we didn't just end the conversation, display it.
        if (Talking && dialogueOptionHandler.Choosing == false)
            Display();
    }

    void AdvanceTurn()
    {
        dialogueTurnIndex++;

        // Simply advance to the next dialogue turn
        if (currentTurn.autoAdvance)
        {
            if (dialogueTurnIndex >= conversationData.dialogueTurns.Length)
                EndConversation();
            else
                currentTurn = conversationData.dialogueTurns[dialogueTurnIndex];
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
        dialogueTurnIndex = option.toDialogueId;
        currentTurn = conversationData.dialogueTurns[dialogueTurnIndex];
        Display();
    }

    void Display()
    {
        speakerNameText.text = currentTurn.speakerName;

        string quote = currentTurn.quotes[currentQuoteIndex].quote;
        quoteText.text = quote;
    }

    void EndConversation()
    {
        dialogueTurnIndex = 0;
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
