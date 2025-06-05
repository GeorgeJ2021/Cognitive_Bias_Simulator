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

    public float totalRevenue => baseRevenue + adRevenue;

    public GameObject FullStats;
    public Button viewFullStatsButton;
    public Button closeFullStatsButton;

    public TMP_Text trustText;
    public TMP_Text perceptionText;
    public TMP_Text engagementText;
    public TMP_Text revenueText;

    void Start()
    {
        viewFullStatsButton.onClick.AddListener(() =>
        {
            FullStats.SetActive(true);
            UpdateStatsDisplay();
        });
        closeFullStatsButton.onClick.AddListener(() => FullStats.SetActive(false));
    }
    public void ApplyArticleEffects(int trustChange, int perceptionChange, int engagementChange)
    {
        publicTrust = Mathf.Clamp(publicTrust + trustChange, 0, 100);
        publicPerception = Mathf.Clamp(publicPerception + perceptionChange, 0, 100);
        engagement = Mathf.Clamp(engagement + engagementChange, 0, 100);

        UpdateEngagement();
        UpdateStatsDisplay();
    }

    public void UpdateEngagement()
    {
        float baseEngagement = engagement;
        float trustModifier = (publicTrust - 50f) / 50f * publicTrustEffectOnEngagement;
        float perceptionModifier = (publicPerception - 50f) / 50f * publicPerceptionEffectOnEngagement;

        float finalEngagement = baseEngagement + (baseEngagement * (trustModifier + perceptionModifier));
        engagement = Mathf.Clamp((int)finalEngagement, 0, 100);

        UpdateRevenue();
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
        baseRevenue = engagement * 10f;
    }

    public void AddAdRevenue(float amount)
    {
        adRevenue += amount;
    }

    public void RemoveAdRevenue(float amount)
    {
        adRevenue = Mathf.Max(adRevenue - amount, 0f);
    }
}
