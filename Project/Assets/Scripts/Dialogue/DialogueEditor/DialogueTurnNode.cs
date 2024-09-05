using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogeEditor
{
    public class DialogueTurnNode : MonoBehaviour
    {
        [SerializeField] TMP_Text speakerText;
        [SerializeField] QuoteNode quoteNodePrefab;
        [SerializeField] OptionNode optionNodePrefab;
        [SerializeField] VerticalLayoutGroup quotesHolder;
        [SerializeField] RectTransform rectTransform;

        public ConversationEditor Editor { get; private set; }

        public void Setup(DialogueTurn turn, ConversationEditor editor)
        {
            this.Editor = editor;

            //idText.text = turn.id.ToString();
            speakerText.text = $"{turn.speakerName} {turn.id}";

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + (turn.quotes.Length * quoteNodePrefab.GetComponent<RectTransform>().sizeDelta.y));

            for (int k = 0; k < turn.quotes.Length; k++)
            {
                QuoteData quoteData = turn.quotes[k];
                QuoteNode node = Instantiate(quoteNodePrefab, quotesHolder.transform);
                node.Setup(quoteData);
            }
        }
    }
}