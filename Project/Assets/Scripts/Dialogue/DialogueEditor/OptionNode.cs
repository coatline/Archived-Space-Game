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

        DialogueTurnNode connectedTo;
        DialogueTurnNode turnNode;
        DialogueOption data;

        public void Setup(DialogueOption data, int optionIndex, DialogueTurnNode turnNode)
        {
            optionIndexText.text = $"Option {optionIndex + 1}";
            this.data = data;
            this.turnNode = turnNode;
            this.connectedTo = turnNode.Editor.GetTurnNode(data.toDialogueId);

            quote.text = data.option;
            effect.text = data.effect;
        }

        private void Update()
        {
            if (connectedTo == null)
            {
                connectedTo = turnNode.Editor.GetTurnNode(data.toDialogueId);
                return;
            }

            lineRenderer.SetPositions(new Vector3[3]
            {
            turnNode.transform.position,
            transform.position,
            connectedTo.transform.position
            });
        }
    }
}
