using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance;

    public GameLogger gameLogger;

    [Header("UI References")]
    public GameObject gameOverPanel;
    public TMP_Text finalStatsText;
    public TMP_Text endingNarrativeText;

    List<string> triggeredConditions = new List<string>();

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

        if (stats.NoOfAdvert >= 3)
        {
            triggeredConditions.Add("AdBias");
        }
        string winner = CheckWinner(stats);
        // Evaluate ending narrative based on stats
        endingNarrativeText.text = GenerateEndingNarrative(stats);
        //gameLogger.LogAndSendData(CheckWinner(), false);
    }

    string ColorText(string text, string color)
    {
        return $"<color={color}>{text}</color>";
    }
    string GenerateEndingNarrative(StatsManager stats)
    {
        if (stats.totalRevenue <= 0)
        {
            triggeredConditions.Add("Bankrupt");
            return ColorText("The establishment has gone broke. The city struggles to maintain services.", "red");
        }
            

        if (stats.publicPerception < 50)
        {
            triggeredConditions.Add("LowPerception");
            return ColorText("The City is gripped by paranoia and distrust.", "red");
        }

        if (stats.publicPerception > 60)
        {
            triggeredConditions.Add("HighPerception");
            return ColorText("The City feels hopeful and united.", "green");
        }

        return "The city remains in a fragile balance, awaiting its next chapter.";
    }

    string CheckWinner(StatsManager stats)
    {
        if (stats.paulPopularity > stats.scientistPopularity)
        {
            triggeredConditions.Add("PaulWon");
            return "Paul";
        }
        if (stats.paulPopularity < stats.scientistPopularity)
        {
            triggeredConditions.Add("ScientistWon");
            return "Scientist";
        }
        if (stats.paulPopularity == stats.scientistPopularity)
        {
            triggeredConditions.Add("JeffWon");
            return "Jeff";
        }

        return "error, no winner";
    }
}
