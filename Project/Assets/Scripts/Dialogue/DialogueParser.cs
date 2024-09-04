using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : Singleton<DialogueParser>
{
    [SerializeField] TextAsset dialogueJson;

    Dictionary<string, ConversationData> getConversation;
    Conversations conversations;

    protected override void Awake()
    {
        base.Awake();

        getConversation = new Dictionary<string, ConversationData>();

        string jsonString = dialogueJson.text;
        conversations = JsonUtility.FromJson<Conversations>(jsonString);

        for (int i = 0; i < conversations.conversations.Length; i++)
        {
            ConversationData conversationData = conversations.conversations[i];
            string conversationName = conversationData.conversationName;
            getConversation.Add(conversationName, conversationData);
        }
    }

    public ConversationData GetConversation(string conversationName)
    {
        if (getConversation.TryGetValue(conversationName, out ConversationData conversationData))
            return conversationData;

        Debug.LogWarning($"Couldn't get conversation: {conversationName}");
        return null;
    }
}
