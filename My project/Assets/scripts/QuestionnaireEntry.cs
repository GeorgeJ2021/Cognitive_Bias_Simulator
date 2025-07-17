using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestionnaireEntry
{
    public string conditionKey;               
    public string questionText;               
    public List<string> options = new();
}
