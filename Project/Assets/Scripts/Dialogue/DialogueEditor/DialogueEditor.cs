using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogeEditor
{
    public class DialogueEditor : MonoBehaviour
    {
        [SerializeField] ConversationEditor conversationEditor;
        [SerializeField] ConversationNode conversationNodePrefab;
        [SerializeField] DialogueTurnNode dialogueTurnNodePrefab;
        [SerializeField] VerticalLayoutGroup convChoiceHolder;

        void Start()
        {
            for (int i = 0; i < DialogueParser.I.Conversations.conversations.Length; i++)
            {
                ConversationNode node = Instantiate(conversationNodePrefab, convChoiceHolder.transform);
                node.transform.position = new Vector3(Random.Range(-6, 6f), Random.Range(-5, 5f));
                ConversationData conversationData = DialogueParser.I.Conversations.conversations[i];
                node.Setup(conversationData, conversationEditor);

                //node.GetComponent<Button>().onClick.AddListener(() => { ChooseConversation(conversationData); });
                //node.GetComponentInChildren<TMP_Text>().text = conversationData.conversationName;
            }

        }

        void ChooseConversation(ConversationData data)
        {
            convChoiceHolder.gameObject.SetActive(false);
            //conversationEditor.
            //conversationNode.Setup(data);
        }
    }
}