using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTurnNode : MonoBehaviour
{
    [SerializeField] TMP_Text idText;
    [SerializeField] TMP_Text speakerText;
    [SerializeField] Image optionPrefab;
    [SerializeField] QuoteNode quoteNodePrefab;
    [SerializeField] VerticalLayoutGroup quotesHolder;

    public void Setup(DialogueTurn turn)
    {
        idText.text = turn.id.ToString();
        speakerText.text = turn.speakerName;

        for (int k = 0; k < turn.quotes.Length; k++)
        {
            QuoteData quoteData = turn.quotes[k];
            QuoteNode node = Instantiate(quoteNodePrefab, quotesHolder.transform);
            node.Setup(quoteData);
        }

        for (int i = 1; i < turn.options.Length; i++)
        {
            Instantiate(optionPrefab, transform);
        }
    }
}
