using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System;


[System.Serializable]
public class QuestionnaireResponse
{
    public string conditionKey;
    public string question;
    public string answer;
    public string biasKey;
}

public class QuestionUI : MonoBehaviour
{
    public TMP_Text questionText;

    public GameObject optionPanel;
    Transform optionsContainer;
    public GameObject buttonPrefab;
    //public TMP_InputField customInput;

    private System.Action onComplete;
    private List<QuestionnaireResponse> responses = new List<QuestionnaireResponse>();
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
                    LogAnswer(data.conditionKey,data.questionText, option.text, option.biasKey);
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

    void LogAnswer(string conditionKey, string question, string answer, string biasKey)
    {
        responses.Add(new QuestionnaireResponse
        {
            conditionKey = conditionKey,
            question = question,
            answer = answer,
            biasKey = biasKey
        });
        // Send to Firebase here or local log
        Debug.Log($"Q: {question} | A: {answer}");
    }

    public void UploadResponsesToFirebase()
    {
        if (responses.Count == 0)
            return;

        string sessionId = System.Guid.NewGuid().ToString();
       foreach (var r in responses)
        {
            string questionKey = r.conditionKey; // Add this to your AnswerData if not already there
            string firebaseUrl = $"https://cogbias1-default-rtdb.europe-west1.firebasedatabase.app/questionnaire/{questionKey}/{sessionId}.json";

            string json = JsonUtility.ToJson(r);
            StartCoroutine(SendToFirebase(firebaseUrl, json));
        }
    }

    [System.Serializable]
    public class Wrapper
    {
        public List<QuestionnaireResponse> responses;
    }

    IEnumerator SendToFirebase(string url, string json)
    {
        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Questionnaire responses uploaded.");
        }
        else
        {
            Debug.LogError("Questionnaire upload failed: " + request.error);
        }
    }

}
