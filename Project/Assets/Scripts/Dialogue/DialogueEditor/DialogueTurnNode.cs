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
        List<OptionNode> optionNodes;
        DialogueTurn dialogueTurn;

        public int ID => dialogueTurn.id;

        public void Setup(DialogueTurn turn, ConversationEditor editor)
        {
            optionNodes = new List<OptionNode>();

            this.dialogueTurn = turn;
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

        public void CreateOptionNode(DialogueOption option, int optionIndex, Vector2 position)
        {
            OptionNode oNode = Instantiate(optionNodePrefab, position, Quaternion.identity, transform);
            oNode.Setup(option, optionIndex, this);
            optionNodes.Add(oNode);
        }

        public DialogueTurn GetData()
        {
            QuoteData[] quotes = new QuoteData[quotesHolder.transform.childCount];
            DialogueOption[] options = new DialogueOption[optionNodes.Count];

            for (int i = 0; i < quotes.Length; i++)
                quotes[i] = quotesHolder.transform.GetChild(i).GetComponent<QuoteNode>().GetData();

            for (int k = 0; k < optionNodes.Count; k++)
                options[k] = optionNodes[k].GetData();

            return new DialogueTurn(dialogueTurn.id, dialogueTurn.speakerName, quotes, options, dialogueTurn.autoAdvanceToID);
        }
    }
}