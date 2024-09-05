using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTurnNode : MonoBehaviour
{
    [SerializeField] TMP_Text idText;
    [SerializeField] TMP_Text speakerText;
    [SerializeField] TMP_Text quoteText;
    [SerializeField] Image optionPrefab;
    [SerializeField] VerticalLayoutGroup optionsHOlder;

    public void Setup(DialogueTurn turn)
    {
        idText.text = turn.id.ToString();
        speakerText.text = turn.speakerName;
        quoteText.text = turn.quotes[0].quote;

        for (int i = 1; i < turn.options.Length; i++)
        {
            Instantiate(optionPrefab, optionsHOlder.transform);
        }
    }
}
