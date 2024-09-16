using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DialogeEditor
{
    public class OptionNode : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] TMP_InputField quote;
        [SerializeField] TMP_InputField effect;
        [SerializeField] TMP_Text optionIndexText;

        public DialogueTurnNode ConnectedTo { get; private set; }
        public DialogueTurnNode TurnNode { get; private set; }
        DialogueOption data;

        public void Setup(DialogueOption data, int optionIndex, DialogueTurnNode turnNode)
        {
            optionIndexText.text = $"Option {optionIndex + 1}";
            this.data = data;
            this.TurnNode = turnNode;
            this.ConnectedTo = turnNode.Editor.GetTurnNode(data.toDialogueId);

            quote.text = data.option;
            effect.text = data.effect;
        }

        private void Update()
        {
            if (ConnectedTo == null)
            {
                ConnectedTo = TurnNode.Editor.GetTurnNode(data.toDialogueId);
                return;
            }

            lineRenderer.SetPositions(new Vector3[3]
            {
            TurnNode.transform.position,
            transform.position,
            ConnectedTo.transform.position
            });
        }

        public DialogueOption GetData()
        {
            int connectedToID = 0;

            if (ConnectedTo != null)
                connectedToID = ConnectedTo.ID;

            return new DialogueOption(connectedToID, quote.text, effect.text);
        }
    }
}
