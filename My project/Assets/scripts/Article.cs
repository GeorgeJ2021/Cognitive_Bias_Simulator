using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArticleUnlockConditionType
{
    None,
    IfApproved,
    IfRejected,
    IfStatGreaterThan,
    IfStatLessThan,
    IfPaulMorePopular,
    CheckEqualPopularity,       
    IfScientistMorePopular
}

public enum StatType
{
    PublicTrust,
    PublicPerception,
    Engagement,
    PaulPopularity,
    ScientistPopularity
}

[System.Serializable]
public class ArticleUnlockCondition
{
    public Article referenceArticle;
    public ArticleUnlockConditionType conditionType;

    public StatType statType;
    public int statThreshold;
}

[CreateAssetMenu(fileName = "NewArticle", menuName = "News/Article")]
public class Article : ScriptableObject
{
    [TextArea(3,5)] public string headline;
    [TextArea(3,10)] public string body;
    public Sprite image;
    public bool isApproved = false;
    public bool isReviewed = false;
    //public Sprite image;

    public int trustImpact;
    public int perceptionImpact;
    public int engagementImpact;
    public int revenueImpact;
    
    public int paulSupportImpact = 0;
    public int scientistSupportImpact = 0;

    [Header("Unlock Conditions")]
    public List<ArticleUnlockCondition> unlockConditions;
    
    public bool IsUnlocked()
    {
        StatsManager stats = GameObject.FindObjectOfType<StatsManager>(); // or assign manually
        if (unlockConditions == null || unlockConditions.Count == 0 || stats == null) return true;

        foreach (var cond in unlockConditions)
        {
            switch (cond.conditionType)
            {
                case ArticleUnlockConditionType.IfApproved:
                    if (cond.referenceArticle == null || !cond.referenceArticle.isApproved) return false;
                    break;
                case ArticleUnlockConditionType.IfRejected:
                    if (cond.referenceArticle == null || cond.referenceArticle.isApproved) return false;
                    break;

                case ArticleUnlockConditionType.IfStatGreaterThan:
                    if (GetStatValue(stats, cond.statType) <= cond.statThreshold)
                        return false;
                    break;

                case ArticleUnlockConditionType.IfStatLessThan:
                    if (GetStatValue(stats, cond.statType) >= cond.statThreshold)
                        return false;
                    break;
                case ArticleUnlockConditionType.IfPaulMorePopular:
                    if (stats.paulPopularity <= stats.scientistPopularity)
                        return false;
                    break;
                case ArticleUnlockConditionType.IfScientistMorePopular:
                    if (stats.scientistPopularity <= stats.paulPopularity)
                        return false;
                    break;
                case ArticleUnlockConditionType.CheckEqualPopularity:
                    if (stats.paulPopularity != stats.scientistPopularity)
                        return false;
                    break;
            }
        }

        return true;
    }

        int GetStatValue(StatsManager stats, StatType type)
    {
        return type switch
        {
            StatType.PublicTrust => stats.publicTrust,
            StatType.PublicPerception => stats.publicPerception,
            StatType.Engagement => stats.engagement,
            StatType.PaulPopularity => stats.paulPopularity,
            StatType.ScientistPopularity => stats.scientistPopularity,
            _ => 0
        };
    }

}
