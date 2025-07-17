using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuestionnaireManager : MonoBehaviour
{
    public List<QuestionnaireEntry> allQuestions;
    public GameObject questionPanelPrefab;
    public Transform questionContainer;
    public GameObject questionnairePanel;

    public GameObject Panel;
     private QuestionUI questionUI;

    private int currentQuestionIndex = 0;
    private List<QuestionnaireEntry> selectedQuestions = new();

    void Awake()
    {
        questionUI = Panel.GetComponent<QuestionUI>();
        if (allQuestions == null || allQuestions.Count == 0)
        {
            allQuestions = new List<QuestionnaireEntry>
            {

                new QuestionnaireEntry {
                    conditionKey = "PaulWon",
                    questionText = "It seems Paul won the election. What influenced your support for him?",
                    options = new List<string> {
                        "He seemed like a practical, no-nonsense leader",       // Representativeness Heuristic
                        "He felt like the most trustworthy candidate",          // Halo Effect
                        "I approved more articles about him without realizing", // Unconscious Bias
                        "Everyone seemed to be leaning toward him at the end",  // Bandwagon Effect
                        "I liked his backstory",                                // Affect Heuristic
                        "Write your own reason..."
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "ScientistWon",
                    questionText = "The Mad Scientist pulled off a surprise win. What led you to support him over Paul?",
                    options = new List<string> {
                        "His unconventional ideas kept working out somehow",            // Outcome Bias
                        "He seemed like a refreshing change from the usual",            // Novelty Bias
                        "Everyone was calling him a hero",                              // Bandwagon Effect
                        "I thought he had a clear plan, even if it was strange",        // Ambiguity Effect
                        "His past accidents had unexpectedly good outcomes",            // Optimism Bias
                        "I didn’t think he’d actually win",                             //Normalcy Bias
                        "Other..."
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "JeffWon",
                    questionText = "Jeff won the election due to a tie. What led you to support both Paul and the Mad Scientist equally?",
                    options = new List<string> {
                        "I tried to stay neutral between Paul and the Scientist",           // Ambiguity Aversion
                        "I didn’t realize neutrality could lead to Jeff winning",           // Outcome Bias
                        "I just thought it would be funny",                                 // Affect Heuristic
                        "I assumed the city would figure out who was best without me",      // Diffusion of Responsibility
                        "I thought being fair to both candidates would work out",           // Fairness Bias / Moral Credentialing
                        "I wasn’t really tracking who was ahead",                           // Unconscious Bias
                        "I thought the game would prevent a tie",                           // Normalcy Bias
                        "Other..."
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "LowPerception",
                    questionText = "The city grew paranoid and distrustful. What do you think led to this?",
                    options = new List<string> {
                        "I published too many fear-driven articles",     // Availability Bias
                        "I focused on controversy to boost engagement",  // Incentive Bias
                        "It wasn't intentional",                         // Omission Bias
                        "Other..."
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "HighPerception",
                    questionText = "The city feels hopeful and united. What contributed to this positive perception?",
                    options = new List<string> {
                        "I focused on uplifting and constructive articles",             // Framing Effect
                        "I avoided stories that could upset readers",                   // Negativity Avoidance
                        "It just sort of happened—I wasn’t tracking perception stats", // Normalcy Bias
                        "Other..."
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "AdBias",
                    questionText = "You published several revenue-heavy advertisements. Why?",
                    options = new List<string> {
                        "They provided the best revenue returns",                      // Incentive Bias
                        "I didn’t consider their effect on public trust",              // Neglect of Probability
                        "The secret messages were interesting or funny",               // Novelty Bias
                        "Other..."
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "Bankrupt",
                    questionText = "The newspaper establishment went bankrupt. Why do you think that happened?",
                    options = new List<string> {
                        "I prioritized short-term revenue over long-term trust",       // Present Bias
                        "I ran too many ads that damaged public trust",                // Moral Licensing
                        "I didn’t balance engagement with financial management",       // Planning Fallacy
                        "Other..."
                    }
                },
            };
        }
    }

    public void StartQuestionnaire(List<string> conditionKeys)
    {
        Debug.Log("Questionnaire started");
        questionnairePanel.SetActive(true);
        selectedQuestions.Clear();
        currentQuestionIndex = 0;

        foreach (var entry in allQuestions)
        {
            Debug.Log("Question entry key: " + entry.conditionKey);

            if (conditionKeys.Contains(entry.conditionKey))
            {
                Debug.Log("Matched condition: " + entry.conditionKey);
                selectedQuestions.Add(entry);
            }
        }

        ShowNextQuestion();
    }

    void ShowNextQuestion()
    {
        if (currentQuestionIndex >= selectedQuestions.Count)
        {
            Debug.Log("Questionnaire finished");
            questionnairePanel.SetActive(false);
            return;
        }

        var questionData = selectedQuestions[currentQuestionIndex];
        //var panel = Instantiate(questionPanelPrefab, questionContainer);
        
        questionUI.Initialize(questionData, () =>
        {
            currentQuestionIndex++;
            ShowNextQuestion();
        });
    }
}
