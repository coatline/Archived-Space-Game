using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueDisplayer : Singleton<DialogueDisplayer>
{
    [SerializeField] DialogueParser dialogueParser;

    [SerializeField] GameObject dialogueVisuals;
    [SerializeField] TMP_Text conversationNameText;
    [SerializeField] TMP_Text speakerDialogueText;
    [SerializeField] TMP_Text speakerNameText;

    public bool Talking { get; private set; }

    ConversationData conversationData;
    DialogueTurn dialogueTurn;
    int currentQuoteIndex;
    int dialogueTurnIndex;

    public void StartConversation(string conversationName)
    {
        conversationData = dialogueParser.GetConversation(conversationName);

        if (conversationData == null)
        {
            EndConversation();
            return;
        }

        conversationNameText.text = conversationData.conversationName;

        AdvanceDialogue();

        dialogueVisuals.SetActive(true);
    }

    public void AdvanceDialogue()
    {
        if (dialogueTurnIndex == conversationData.dialogueTurns.Length)
        {
            EndConversation();
            return;
        }
        else
            DisplayDialogueTurn(conversationData.dialogueTurns[dialogueTurnIndex++]);
    }

    void DisplayDialogueTurn(DialogueTurn dialogueData)
    {
        speakerNameText.text = dialogueData.speakerName;

        string quote = dialogueData.quotes[currentQuoteIndex++];
        speakerDialogueText.text = quote;
    }

    void EndConversation()
    {

    }
}
