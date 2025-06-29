using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public int publicTrust = 50;
    public int publicPerception = 50;
    public int engagement = 0;

    public float baseRevenue = 0f;
    public float adRevenue = 0f;

    [Range(-1f, 1f)] public float publicTrustEffectOnEngagement = 0.1f;
    [Range(-1f, 1f)] public float publicPerceptionEffectOnEngagement = 0.2f;

    public float totalRevenue;

    public GameObject FullStats;
    public Button viewFullStatsButton;
    public Button closeFullStatsButton;

    [Header("Stats Text fields")]
    public TMP_Text trustText;
    public TMP_Text perceptionText;
    public TMP_Text engagementText;
    public TMP_Text revenueText;

    [Header("Popularity")]
    public int paulPopularity = 0;
    public int scientistPopularity = 0;

    void Start()
    {
        viewFullStatsButton.onClick.AddListener(() =>
        {
            FullStats.SetActive(true);
            UpdateStatsDisplay();
        });
        closeFullStatsButton.onClick.AddListener(() => FullStats.SetActive(false));
    }
    public void ApplyArticleEffects(int trustChange, int perceptionChange, int engagementChange, int paulSupportImpact, int scientistSupportImpact)
    {
        publicTrust = Mathf.Clamp(publicTrust + trustChange, 0, 100);
        publicPerception = Mathf.Clamp(publicPerception + perceptionChange, 0, 100);
        engagement += engagementChange;

        paulPopularity += paulSupportImpact;
        scientistPopularity += scientistSupportImpact;

        UpdateEngagement();
        UpdateStatsDisplay();
    }

    public void UpdateEngagement()
    {
        engagement = CalculateRevEng(engagement, publicTrust, publicPerception);
        UpdateRevenue();
    }

    public int CalculateRevEng(int Engagement, int Trust, int Perception)
    {
        float baseEngagement = Engagement;
        float trustModifier = (Trust - 50f) / 50f * publicTrustEffectOnEngagement;
        float perceptionModifier = (Perception - 50f) / 50f * publicPerceptionEffectOnEngagement;

        float finalEngagement = baseEngagement + (baseEngagement * (trustModifier + perceptionModifier));
        Engagement = Mathf.Clamp((int)finalEngagement, 0, 100);
        return Engagement;
    }

    public void UpdateStatsDisplay()
    {
        trustText.text = publicTrust.ToString();
        perceptionText.text = publicPerception.ToString();
        engagementText.text = engagement.ToString();
        revenueText.text = totalRevenue.ToString("F0");
    }

    public void UpdateRevenue()
    {
        baseRevenue = engagement * 3.0f;
        totalRevenue += baseRevenue;
    }

    public void AddAdRevenue(float amount)
    {
        totalRevenue += amount;
    }

    public void RemoveAdRevenue(float amount)
    {
        adRevenue = Mathf.Max(adRevenue - amount, 0f);
    }
}
