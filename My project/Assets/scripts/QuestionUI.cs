using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public TMP_Text questionText;

    public GameObject optionPanel;
    Transform optionsContainer;
    public GameObject buttonPrefab;
    //public TMP_InputField customInput;

    private System.Action onComplete;


    public void Initialize(QuestionnaireEntry data, System.Action onCompleteCallback)
    {
        onComplete = onCompleteCallback;
        questionText.text = data.questionText;

        optionsContainer = optionPanel.transform; 

        foreach (Transform child in optionsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var option in data.options)
        {
            var btn = Instantiate(buttonPrefab, optionsContainer);
            btn.GetComponentInChildren<TMP_Text>().text = option;
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                LogAnswer(data.questionText, option);
                onComplete?.Invoke();
                //Destroy(gameObject);
            });
        }

        // customInput.onEndEdit.AddListener((text) =>
        // {
        //     if (!string.IsNullOrEmpty(text))
        //     {
        //         LogAnswer(data.questionText, text);
        //         onComplete?.Invoke();
        //         Destroy(gameObject);
        //     }
        // });
    }

    void LogAnswer(string question, string answer)
    {
        // Send to Firebase here or local log
        Debug.Log($"Q: {question} | A: {answer}");
    }
}
