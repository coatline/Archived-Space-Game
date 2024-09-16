using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogeEditor
{
    public class ConversationEditor : MonoBehaviour
    {
        [SerializeField] GameObject convChoiceHolder;
        [SerializeField] DialogueTurnNode turnNodePrefab;
        [SerializeField] OptionNode optionNodePrefab;
        [SerializeField] Animator savedTextAnimator;
        [SerializeField] float scrollSpeed;
        [SerializeField] float moveSpeed;
        [SerializeField] Vector2 spacing;

        public Vector2 Spacing => spacing;

        Dictionary<int, DialogueTurnNode> idToTurnNode;
        Dictionary<int, DialogueTurn> idToTurn;
        List<DialogueTurnNode> dialogueNodes;
        ConversationData data;

        private void Awake()
        {
            idToTurnNode = new Dictionary<int, DialogueTurnNode>();
            idToTurn = new Dictionary<int, DialogueTurn>();
            dialogueNodes = new List<DialogueTurnNode>();
        }


        public void StartDisplaying(ConversationData data)
        {
            this.data = data;

            convChoiceHolder.SetActive(false);
            idToTurnNode.Clear();
            idToTurn.Clear();

            for (int i = 0; i < data.dialogueTurns.Length; i++)
            {
                DialogueTurn turn = data.dialogueTurns[i];

                idToTurn.Add(turn.id, turn);
            }

            CreateTurn(1, Vector3.zero);
        }

        public void StopDisplaying()
        {
            convChoiceHolder.SetActive(true);
        }

        void CreateTurn(int id, Vector2 position)
        {
            if (id == -1)
                return;

            DialogueTurn turn = GetTurn(id);
            DialogueTurnNode turnNode = Instantiate(turnNodePrefab, position, Quaternion.identity, transform);

            dialogueNodes.Add(turnNode);
            idToTurnNode.Add(turn.id, turnNode);

            turnNode.Setup(turn, this);

            if (turn.options.Length > 0)
            {
                position.x += spacing.x;

                for (int i = 0; i < turn.options.Length; i++)
                {
                    DialogueOption option = turn.options[i];
                    Vector2 optionPosition = new Vector2(position.x, position.y + Spacing.y * i);

                    turnNode.CreateOptionNode(option, i, optionPosition);
                    CreateTurn(option.toDialogueId, optionPosition + new Vector2(spacing.x, 0));
                }
            }
            else if (turn.autoAdvanceToID != 0)
            {
                CreateTurn(turn.autoAdvanceToID, position + new Vector2(spacing.x, 0));
                return;
            }
        }


        private void Update()
        {
            Vector2 inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            transform.Translate(-inputs * Time.deltaTime * moveSpeed);

            float scrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
            transform.localScale += Vector3.one * scrollWheel * Time.deltaTime * scrollSpeed;
        }

        public DialogueTurn GetTurn(int id)
        {
            if (idToTurn.ContainsKey(id))
                return idToTurn[id];
            return null;
        }

        public DialogueTurnNode GetTurnNode(int id)
        {
            if (idToTurnNode.ContainsKey(id))
                return idToTurnNode[id];
            return null;
        }

        public void Save()
        {
            if (data != null)
            {
                DialogueParser.I.GetConversation(data.conversationName).dialogueTurns = GetDialogueTurns();
                DialogueParser.I.Save();
                savedTextAnimator.Play("Slideup");
            }
        }

        public DialogueTurn[] GetDialogueTurns()
        {
            DialogueTurn[] turns = new DialogueTurn[dialogueNodes.Count];

            for (int i = 0; i < dialogueNodes.Count; i++)
            {
                DialogueTurnNode node = dialogueNodes[i];
                turns[i] = node.GetData();
            }

            return turns;
        }
    }
}