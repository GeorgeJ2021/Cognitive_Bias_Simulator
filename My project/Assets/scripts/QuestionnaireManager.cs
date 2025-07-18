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
                    options = new List<OptionData> {
                        new OptionData { text = "He seemed like a practical, no-nonsense leader", biasKey = "RepresentativenessHeuristic" },
                        new OptionData { text = "He felt like the most trustworthy candidate", biasKey = "HaloEffect" },
                        new OptionData { text = "I approved more articles about him without realizing", biasKey = "UnconsciousBias" },
                        new OptionData { text = "Everyone seemed to be leaning toward him at the end", biasKey = "BandwagonEffect" },
                        new OptionData { text = "I liked his backstory", biasKey = "AffectHeuristic" },
                        new OptionData { text = "Write your own reason...", biasKey = "Custom" }
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "ScientistWon",
                    questionText = "The Mad Scientist pulled off a surprise win. What led you to support him over Paul?",
                    options = new List<OptionData> {
                        new OptionData { text = "His unconventional ideas kept working out somehow", biasKey = "OutcomeBias" },
                        new OptionData { text = "He seemed like a refreshing change from the usual", biasKey = "NoveltyBias" },
                        new OptionData { text = "Everyone was calling him a hero", biasKey = "BandwagonEffect" },
                        new OptionData { text = "I thought he had a clear plan, even if it was strange", biasKey = "AmbiguityEffect" },
                        new OptionData { text = "His past accidents had unexpectedly good outcomes", biasKey = "OptimismBias" },
                        new OptionData { text = "I didn’t think he’d actually win", biasKey = "NormalcyBias" },
                        new OptionData { text = "Other...", biasKey = "Custom" }
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "JeffWon",
                    questionText = "Jeff won the election due to a tie. What led you to support both Paul and the Mad Scientist equally?",
                    options = new List<OptionData> {
                        new OptionData { text = "I tried to stay neutral between Paul and the Scientist", biasKey = "AmbiguityAversion" },
                        new OptionData { text = "I didn’t realize neutrality could lead to Jeff winning", biasKey = "OutcomeBias" },
                        new OptionData { text = "I just thought it would be funny", biasKey = "AffectHeuristic" },
                        new OptionData { text = "I assumed the city would figure out who was best without me", biasKey = "DiffusionOfResponsibility" },
                        new OptionData { text = "I thought being fair to both candidates would work out", biasKey = "FairnessBias" },
                        new OptionData { text = "I wasn’t really tracking who was ahead", biasKey = "UnconsciousBias" },
                        new OptionData { text = "I thought the game would prevent a tie", biasKey = "NormalcyBias" },
                        new OptionData { text = "Other...", biasKey = "Custom" }
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "LowPerception",
                    questionText = "The city grew paranoid and distrustful. What do you think led to this?",
                    options = new List<OptionData> {
                        new OptionData { text = "I published too many fear-driven articles", biasKey = "AvailabilityBias" },
                        new OptionData { text = "I focused on controversy to boost engagement", biasKey = "IncentiveBias" },
                        new OptionData { text = "It wasn't intentional", biasKey = "OmissionBias" },
                        new OptionData { text = "Other...", biasKey = "Custom" }
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "HighPerception",
                    questionText = "The city feels hopeful and united. What contributed to this positive perception?",
                    options = new List<OptionData> {
                        new OptionData { text = "I focused on uplifting and constructive articles", biasKey = "FramingEffect" },
                        new OptionData { text = "I avoided stories that could upset readers", biasKey = "NegativityAvoidance" },
                        new OptionData { text = "I wasn’t tracking perception stats", biasKey = "NormalcyBias" },
                        new OptionData { text = "Other...", biasKey = "Custom" }
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "AdBias",
                    questionText = "You published several revenue-heavy advertisements. Why?",
                    options = new List<OptionData> {
                        new OptionData { text = "They provided the best revenue returns", biasKey = "IncentiveBias" },
                        new OptionData { text = "I didn’t consider their effect on public trust", biasKey = "NeglectOfProbability" },
                        new OptionData { text = "The secret messages were interesting or funny", biasKey = "NoveltyBias" },
                        new OptionData { text = "Other...", biasKey = "Custom" }
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "Bankrupt",
                    questionText = "The newspaper establishment went bankrupt. Why do you think that happened?",
                    options = new List<OptionData> {
                        new OptionData { text = "I prioritized short-term revenue over long-term trust", biasKey = "PresentBias" },
                        new OptionData { text = "I ran too many ads that damaged public trust", biasKey = "MoralLicensing" },
                        new OptionData { text = "I didn’t balance engagement with financial management", biasKey = "PlanningFallacy" },
                        new OptionData { text = "Other...", biasKey = "Custom" }
                    }
                },
                new QuestionnaireEntry {
                    conditionKey = "ProFab",
                    questionText = "You published articles in support of Fabrikator.What motivated that choice?",
                    options = new List<OptionData> {
                        new OptionData { text = "He was creating jobs and boosting the economy which that felt more important.", biasKey = "AffectHeuristic" },
                        new OptionData { text = "The pizza ads kept hinting he was innocent", biasKey = "AnchoringBias" },
                        new OptionData { text = "I assumed someone else would expose him if he were truly guilty.", biasKey = "DiffusionOfResponsibility" },
                        new OptionData { text = "I thought the timing of the case was too suspicious to be real.", biasKey = "NormalcyBias" },
                        new OptionData { text = "The company had a polished image, so I assumed it was trustworthy.", biasKey = "HaloEffect" },
                        new OptionData { text = "I chose to keep things as they were by supporting them.", biasKey = "StatusQuoBias" },
                        new OptionData { text = "Other...", biasKey = "Custom" }
                    }
                },

                new QuestionnaireEntry {
                    conditionKey = "AntiFab",
                    questionText = "You published articles that criticized Fabrikator. What motivated that choice?",
                    options = new List<OptionData> {
                        new OptionData { text = "I trusted the whistleblowers because they seemed credible", biasKey = "AuthorityBias" },
                        new OptionData { text = "Everyone around me seemed to be turning on the company.", biasKey = "BandwagonEffect" },
                        new OptionData { text = "Negative articles drove more engagement and revenue.", biasKey = "IncentiveBias" },
                        new OptionData { text = "I didn’t want to risk downplaying something that could be serious.", biasKey = "LossAversion" },
                        new OptionData { text = "The stories confirmed what I already suspected about the company.", biasKey = "ConfirmationBias" },
                        new OptionData { text = "I figured the damage was already done, so why not dig deeper?", biasKey = "SunkCostFallacy" },
                        new OptionData { text = "Other...", biasKey = "Custom" }
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
