using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOptionHandler : Singleton<DialogueOptionHandler>
{
    [SerializeField] VerticalLayoutGroup choiceHolder;
    [SerializeField] Button choicePrefab;

    public bool Choosing { get; private set; }

    TMP_Text[] buttonTexts;
    Button[] buttons;

    private void Start()
    {
        buttons = new Button[5];
        buttonTexts = new TMP_Text[5];

        for (int i = 0; i < 5; i++)
        {
            buttons[i] = Instantiate(choicePrefab, choiceHolder.transform);
            buttonTexts[i] = buttons[i].GetComponentInChildren<TMP_Text>();
        }
    }

    public void StartChoosing(DialogueOption[] options)
    {
        Choosing = true;
        choiceHolder.gameObject.SetActive(true);

        // Disable all buttons not current in use
        for (int k = options.Length - 1; k < buttons.Length; k++)
            buttons[k].gameObject.SetActive(false);

        // Enable all buttons in use and set their click events
        for (int i = 0; i < options.Length; i++)
        {
            DialogueOption option = options[i];
            Button button = buttons[i];

            buttonTexts[i].text = $"{i + 1}. {option.option}";

            if (option.effect != "")
                buttonTexts[i].text += $" </color=cyan>[{option.effect}]</color>";

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { ChooseOption(option); });
            button.gameObject.SetActive(true);
        }

        buttons[0].Select();
    }

    void ChooseOption(DialogueOption option)
    {
        // TODO: invoke effect
        if (option.effect != "" && option.effect != null)
            DialogueCommandExecuter.ExecuteActions(option.effect, DialogueDisplayer.I.TalkingTo.transform.parent.gameObject);

        Choosing = false;
        choiceHolder.gameObject.SetActive(false);
        DialogueDisplayer.I.ChoseOption(option);
    }

    public void ChooseOption(int optionNumber)
    {
        if (buttons.Length < optionNumber)
            return;

        Button b = buttons[optionNumber - 1];

        if (b.gameObject.activeSelf)
            b.onClick.Invoke();
    }
}
