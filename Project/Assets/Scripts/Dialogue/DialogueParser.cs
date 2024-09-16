using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueParser : Singleton<DialogueParser>
{
    [SerializeField] TextAsset dialogueJson;

    Dictionary<string, ConversationData> getConversation;
    public Conversations Conversations { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        getConversation = new Dictionary<string, ConversationData>();

        string jsonString = dialogueJson.text;
        Conversations = JsonUtility.FromJson<Conversations>(jsonString);

        for (int i = 0; i < Conversations.conversations.Length; i++)
        {
            ConversationData conversationData = Conversations.conversations[i];
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

    public void Save()
    {
        string json = JsonUtility.ToJson(Conversations);
        string filePath = Path.Combine(Application.dataPath, "Dialogue.json");
        File.WriteAllText(filePath, json);
    }
}
