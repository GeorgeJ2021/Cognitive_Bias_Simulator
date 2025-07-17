using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class OptionData
{
    public string text;
    public string biasKey; 
    //public string explanation; 
}

[System.Serializable]
public class QuestionnaireEntry
{
    public string conditionKey;               
    public string questionText;               
    public List<OptionData> options;
}
