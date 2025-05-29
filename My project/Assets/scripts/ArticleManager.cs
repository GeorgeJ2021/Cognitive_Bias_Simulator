using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArticleManager : MonoBehaviour
{
    [Header("News Cycle")]
    public List<NewsCycle> newsCycles;
    private int currentCycleIndex = 0;

    [Header("UI")]
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public TMP_Text articleHeadline;
    public TMP_Text articleBody;
    public Button approveButton;
    public Button rejectButton;
    private Article currentArticle;
    private Dictionary<Article, GameObject> articleButtons = new();

    [Header("Stats Preview UI")]
    public GameObject statsPopup;
    public TMP_Text trustText;
    public TMP_Text perceptionText;
    public TMP_Text engagementText;
    public TMP_Text revenueText;
    public Button viewStatsButton;
    public Button closePopupButton;

    // Start is called before the first frame update
    void Start()
    {
        LoadCycle(currentCycleIndex);
        viewStatsButton.onClick.AddListener(ShowStatsPopup);
        closePopupButton.onClick.AddListener(() => statsPopup.SetActive(false));
    }
    public void LoadCycle(int index)
    {
        ClearButtons();
        currentCycleIndex = index;

        foreach (var article in newsCycles[index].articles)
        {
            if (!article.IsUnlocked()) continue;

            GameObject btn = Instantiate(buttonPrefab, buttonContainer);
            btn.GetComponentInChildren<TMP_Text>().text = article.headline;
            articleButtons[article] = btn;
            
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                DisplayArticle(article);
            });
        }
    }
    void ClearButtons()
    {
        foreach (Transform child in buttonContainer)
            Destroy(child.gameObject);
    }

    void DisplayArticle(Article article)
    {
        currentArticle = article;
        articleHeadline.text = article.headline;
        articleBody.text = article.body;

        approveButton.gameObject.SetActive(true);
        rejectButton.gameObject.SetActive(true);

        approveButton.onClick.RemoveAllListeners();
        rejectButton.onClick.RemoveAllListeners();

        approveButton.onClick.AddListener(() => ApproveArticle());
        rejectButton.onClick.AddListener(() => RejectArticle());

        Debug.Log("Listeners activated");
    }
    public void ApproveArticle()
    {
        currentArticle.isApproved = true;
        currentArticle.isReviewed = true;
        Debug.Log("Approved");
        ApplyArticleEffects(currentArticle);
        UpdateButtonColor(currentArticle, Color.green);
        //HideDecisionButtons();
    }
    
    public void RejectArticle()
    {
        currentArticle.isApproved = false;
        currentArticle.isReviewed = true;
        UpdateButtonColor(currentArticle, Color.red);
        //HideDecisionButtons();
    }

    void UpdateButtonColor(Article article, Color color)
    {
        if (articleButtons.TryGetValue(article, out GameObject btn))
        {
            var image = btn.GetComponent<Image>();
            if (image != null)
            {
                image.color = color;
            }
        }
    }

    void ShowStatsPopup()
    {
        if (currentArticle == null) return;

        statsPopup.SetActive(true);
        //viewStatsButton.SetActive(false);
        trustText.text = "Public Trust: " + FormatImpact(currentArticle.trustImpact);
        perceptionText.text = "Public Perception: " + FormatImpact(currentArticle.perceptionImpact);
        engagementText.text = "Engagement: " + FormatImpact(currentArticle.engagementImpact);
        revenueText.text = "Revenue: " + FormatImpact(currentArticle.revenueImpact);
    }

    string FormatImpact(int value)
    {
        if (value == 0) return "+0";
        return value > 0 ? "+" + value.ToString() : value.ToString();
    }

    void ApplyArticleEffects(Article article)
    {
        // manages stat 
    }
    void HideDecisionButtons()
    {
        approveButton.gameObject.SetActive(false);
        rejectButton.gameObject.SetActive(false);
    }

    public void LoadNextCycle()
    {
        if (currentCycleIndex + 1 < newsCycles.Count)
        {
            LoadCycle(++currentCycleIndex);
        }
        else
        {
            Debug.Log("No more news cycles.");
        }
    }

}
