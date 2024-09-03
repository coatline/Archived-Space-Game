using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public int id;
    public int emotion;
    public string speakerName;
    public string[] quotes;
    public DialogueOption[] options;
}