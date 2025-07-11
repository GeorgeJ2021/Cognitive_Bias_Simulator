using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
   public static GameOver Instance;

    [Header("UI References")]
    public GameObject gameOverPanel;
    public TMP_Text finalStatsText;
    public TMP_Text endingNarrativeText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        gameOverPanel.SetActive(false);
    }

    // Call this from ArticleManager when all cycles are done
    public void ShowResults(StatsManager stats)
    {
        gameOverPanel.SetActive(true);

        // Example summary stats
        string statsSummary =
            $"Public Trust: {stats.publicTrust}\n" +
            $"Public Perception: {stats.publicPerception}\n" +
            $"Engagement: {stats.engagement}\n" +
            $"Total Revenue: {stats.totalRevenue:F2}";

        finalStatsText.text = statsSummary;

        // Evaluate ending narrative based on stats
        endingNarrativeText.text = GenerateEndingNarrative(stats);
    }

    string ColorText(string text, string color)
    {
        return $"<color={color}>{text}</color>";
    }
    string GenerateEndingNarrative(StatsManager stats)
    {
        if (stats.totalRevenue <= 0)
            return ColorText("The establishment has gone broke. The city struggles to maintain services.", "red");

        if (stats.publicPerception < 50)
            return ColorText("The City is gripped by paranoia and distrust.", "red");

        if (stats.publicPerception > 60)
            return ColorText("The City feels hopeful and united.", "green");
        
        return "The city remains in a fragile balance, awaiting its next chapter.";
    }
}
