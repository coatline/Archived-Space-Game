using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DialogeEditor
{
    public class ConversationNode : MonoBehaviour
    {
        [SerializeField] TMP_Text convNameText;

        ConversationEditor editor;
        ConversationData data;

        public void Setup(ConversationData data, ConversationEditor editor)
        {
            this.data = data;
            this.editor = editor;

            convNameText.text = data.conversationName;
        }

        // Called from Button
        public void EditConversation()
        {
            editor.StartDisplaying(data);
        }
    }
}