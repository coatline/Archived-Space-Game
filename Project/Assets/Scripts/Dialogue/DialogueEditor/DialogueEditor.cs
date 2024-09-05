using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEditor : MonoBehaviour
{
    [SerializeField] Button conversationChoicePrefab;
    [SerializeField] VerticalLayoutGroup choiceHolder;
    [SerializeField] DialogueTurnNode dialogueTurnNodePrefab;
    ConversationData conversationData;

    void Start()
    {
        for (int i = 0; i < DialogueParser.I.Conversations.conversations.Length; i++)
        {
            Button choice = Instantiate(conversationChoicePrefab, choiceHolder.transform);
            ConversationData conversationData = DialogueParser.I.Conversations.conversations[i];
            choice.onClick.AddListener(() => { ChooseConversation(conversationData); });
            choice.GetComponentInChildren<TMP_Text>().text = conversationData.conversationName;
        }

    }

    void ChooseConversation(ConversationData data)
    {
        conversationData = data;

        for (int i = 0; i < conversationData.dialogueTurns.Length; i++)
        {
            DialogueTurn turn = conversationData.dialogueTurns[i];
            DialogueTurnNode node = Instantiate(dialogueTurnNodePrefab, transform);
            node.Setup(turn);
        }
    }
}
