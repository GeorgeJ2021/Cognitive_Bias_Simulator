using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArticleUnlockConditionType
{
    None,
    IfApproved,
    IfRejected
}

[System.Serializable]
public class ArticleUnlockCondition
{
    public Article referenceArticle;
    public ArticleUnlockConditionType conditionType;
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

    [Header("Unlock Conditions")]
    public List<ArticleUnlockCondition> unlockConditions;
    
    public bool IsUnlocked()
    {
        if (unlockConditions == null || unlockConditions.Count == 0) return true;

        foreach (var cond in unlockConditions)
        {
            if (cond.referenceArticle == null) continue;

            switch (cond.conditionType)
            {
                case ArticleUnlockConditionType.IfApproved:
                    if (!cond.referenceArticle.isApproved) return false;
                    break;
                case ArticleUnlockConditionType.IfRejected:
                    if (cond.referenceArticle.isApproved) return false;
                    break;
            }
        }
        return true;
    }

}
