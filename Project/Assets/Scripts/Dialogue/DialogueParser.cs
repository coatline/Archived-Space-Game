using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    [SerializeField] TextAsset dialogueJson;

    Dictionary<string, ConversationData> getConversation;
    Conversations conversations;

    void Start()
    {
        string jsonString = dialogueJson.text;
        conversations = JsonUtility.FromJson<Conversations>(jsonString);
    }

    void Update()
    {

    }
}
