using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[Serializable]
public class PlaythroughData
{
    public string sessionId;
    public string candidateWinner;
    public bool wentAgainstFabrikator;
    public int publicTrust;
    public int publicPerception;
    public int engagement;
    public float finalRevenue;
    public string timestamp;
}
public class GameLogger : MonoBehaviour
{
    public StatsManager statsManager;

    [Header("Firebase Settings")]
    string firebaseUrl = "https://cogbias1-default-rtdb.europe-west1.firebasedatabase.app/logs/" + System.Guid.NewGuid().ToString() + ".json";

    // Call this when the game ends (e.g. in GameOverScene)
    public void LogAndSendData(string winner, bool turnedOnFabrikator)
    {
        PlaythroughData data = new PlaythroughData
        {
            sessionId = System.Guid.NewGuid().ToString(),
            candidateWinner = winner,
            wentAgainstFabrikator = turnedOnFabrikator,
            publicTrust = statsManager.publicTrust,
            publicPerception = statsManager.publicPerception,
            engagement = statsManager.engagement,
            finalRevenue = statsManager.totalRevenue,
            timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
        };

        string json = JsonUtility.ToJson(data);
        StartCoroutine(SendToFirebase(json));
    }

    IEnumerator SendToFirebase(string json)
    {
        UnityWebRequest request = new UnityWebRequest(firebaseUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Playthrough uploaded to Firebase.");
        }
        else
        {
            Debug.LogError("Firebase upload failed: " + request.error);
        }
    }
}
