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

    public GameObject biasPopupPanel;
    public TMP_Text biasTitleText;
    public TMP_Text biasExplanationText;
    public Button closePopupButton;

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
            btn.GetComponentInChildren<TMP_Text>().text = option.text;
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ShowBiasPopup(option.biasKey, () =>
                {
                    LogAnswer(data.questionText, option.text);
                    onComplete?.Invoke();
                });
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

    void ShowBiasPopup(string biasKey, System.Action onClose)
    {
        if (CognitiveBiasLibrary.Biases.TryGetValue(biasKey, out var bias))
        {
            biasPopupPanel.SetActive(true);
            biasTitleText.text = bias.name;
            biasExplanationText.text = bias.explanation;

            closePopupButton.onClick.RemoveAllListeners();
            closePopupButton.onClick.AddListener(() =>
            {
                biasPopupPanel.SetActive(false);
                onClose?.Invoke();
            });
        }
        else
        {
            Debug.LogWarning($"Bias key '{biasKey}' not found.");
            onClose?.Invoke(); // Skip popup if not found
        }
    }

    void LogAnswer(string question, string answer)
    {
        // Send to Firebase here or local log
        Debug.Log($"Q: {question} | A: {answer}");
    }
}
