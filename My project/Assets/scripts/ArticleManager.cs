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
    private List<Article> approvedArticles = new List<Article>();

    [Header("Image Viewer UI")]
    public GameObject imagePopupPanel;   
    public Image popupArticleImage;
    public Button showAttachmentsButton; 

    [Header("Stats Preview UI")]
    public GameObject statsPopup;
    public StatsManager statsManager;
    public TMP_Text trustText;
    public TMP_Text perceptionText;
    public TMP_Text engagementText;
    public TMP_Text revenueText;
    public Button viewStatsButton;
    public Button closePopupButton;

    [Header("Preview approved articles UI")]
    public Button PublishButton;
    public GameObject publishPanel;
    public TMP_Text publishPreviewText;
    public Button confirmPublishButton;
    public Button cancelPublishButton;

    // Start is called before the first frame update
    void Start()
    {
        LoadCycle(currentCycleIndex);
        viewStatsButton.onClick.AddListener(ShowStatsPopup);
       
        PublishButton.onClick.AddListener(OnPublishClicked);
        confirmPublishButton.onClick.AddListener(ApplyApprovedArticleEffects);       
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void LoadCycle(int index)
    {
        ClearButtons();
        approvedArticles.Clear();
        articleHeadline.text = " ";
        articleBody.text = " ";
        UpdatePublishButton();
        currentCycleIndex = index;

        foreach (var article in newsCycles[index].articles)
        {
            if (!article.IsUnlocked()) continue;

            GameObject btn = Instantiate(buttonPrefab, buttonContainer);
            btn.GetComponentInChildren<TMP_Text>().text = article.headline;
            articleButtons[article] = btn;
            article.isApproved = false;
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

        //Debug.Log("Listeners activated");

        if (article.image != null)
        {
            Debug.Log("Image detected");
            popupArticleImage.sprite = article.image;
            imagePopupPanel.SetActive(true); 
            showAttachmentsButton.interactable = true;

            showAttachmentsButton.onClick.RemoveAllListeners();
            showAttachmentsButton.onClick.AddListener(() =>
            {
                imagePopupPanel.SetActive(true);
                //showAttachmentsButton.interactable = false; 
            });
        }
        else
        {
            imagePopupPanel.SetActive(false);
            showAttachmentsButton.interactable = false;
        }
    }

    private void UpdatePublishButton()
    {
        PublishButton.gameObject.SetActive(approvedArticles.Count == 4);
    }
    public void ApproveArticle()
    {
        currentArticle.isApproved = true;
        currentArticle.isReviewed = true;
        Debug.Log("Approved");
        if (!approvedArticles.Contains(currentArticle))
        {
            approvedArticles.Add(currentArticle);
        }
        UpdatePublishButton();
        UpdateButtonColor(currentArticle, Color.green);
        
        //HideDecisionButtons();
    }

    
    public void RejectArticle()
    {
        currentArticle.isApproved = false;
        currentArticle.isReviewed = true;
        if (approvedArticles.Contains(currentArticle))
        {
            approvedArticles.Remove(currentArticle);
        }
        UpdatePublishButton();
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
    public void OnPublishClicked()
    {
        if (approvedArticles.Count < 4)
        {
            Debug.Log("You need to approve at least 4 articles.");
            return;
        }

        int trustSum = 0;
        int perceptionSum = 0;
        int engagementSum = 0;
        float revenueSum = 0;

        foreach (var article in approvedArticles)
        {
            trustSum += article.trustImpact;
            perceptionSum += article.perceptionImpact;
            engagementSum += article.engagementImpact;
            revenueSum += article.revenueImpact;
        }

        int simulatedTrust = statsManager.publicTrust + trustSum;
        int simulatedPerception = statsManager.publicPerception + perceptionSum;
        int simulatedEngagement = statsManager.engagement + engagementSum;

        int projectedEngagement = statsManager.CalculateRevEng(simulatedEngagement,simulatedTrust,
        simulatedPerception);

        float projectedRevenue = (projectedEngagement * 3.0f) + revenueSum;
        Debug.Log("project revenue is: "+ projectedRevenue);

        publishPreviewText.text =
            $"<b>Projected Changes:</b>\n" +
            $"Public Trust: {statsManager.publicTrust} {FormatImpact(trustSum)}\n" +
            $"Public Perception: {statsManager.publicPerception} {FormatImpact(perceptionSum)}\n" +
            $"Engagement: {statsManager.engagement} {FormatImpact(engagementSum)}\n" +
            $"Revenue: {statsManager.totalRevenue:F0} {FormatImpact((int)projectedRevenue)}";

        publishPanel.SetActive(true);
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
        if (value == 0) return "<color=white>+0</color>";
        string color = value > 0 ? "green" : "red";
        string sign = value > 0 ? "+" : "";
        return $"<color={color}>{sign}{value}</color>";
    }

    void ApplyApprovedArticleEffects()
    {
        int totalTrustImpact = 0;
        int totalPerceptionImpact = 0;
        int totalEngagementImpact = 0;
        int totalPaulSupportImpact = 0;
        int totalScientistSupportImpact = 0;
        float totalRevenueImpact = 0f;

        foreach (var article in approvedArticles)
        {
            totalTrustImpact += article.trustImpact;
            totalPerceptionImpact += article.perceptionImpact;
            totalEngagementImpact += article.engagementImpact;
            totalPaulSupportImpact += article.paulSupportImpact;
            totalScientistSupportImpact += article.scientistSupportImpact;
            totalRevenueImpact += article.revenueImpact;

            article.isApproved = true;
        }
        statsManager.ApplyArticleEffects(totalTrustImpact, totalPerceptionImpact, totalEngagementImpact, totalPaulSupportImpact, totalScientistSupportImpact);
        statsManager.AddAdRevenue(totalRevenueImpact);

        publishPanel.SetActive(false);
        Debug.Log("Published. Stats updated.");
        LoadNextCycle();
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
