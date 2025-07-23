using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBiasKeys
{
    public string name;
    public string explanation;
}

public static class CognitiveBiasLibrary
{
    public static Dictionary<string, CogBiasKeys> Biases = new Dictionary<string, CogBiasKeys>
    {
        ["AnchoringBias"] = new CogBiasKeys {
            name = "Anchoring Bias",
            explanation = "You relied too heavily on the first piece of information you saw (like a hint or early impression) when making decisions."
        },
        ["RepresentativenessHeuristic"] = new CogBiasKeys {
            name = "Representativeness Heuristic",
            explanation = "You judged something based on how much it resembles a stereotype, rather than logic or probability."
        },
        ["StatusQuoBias"] = new CogBiasKeys {
            name = "Status Quo Bias",
            explanation = "You preferred to keep things the same, avoiding change even when better options might have been available."
        },
        ["HaloEffect"] = new CogBiasKeys {
            name = "Halo Effect",
            explanation = "You attributed unrelated positive qualities based on one good trait or impression."
        },
        ["UnconsciousBias"] = new CogBiasKeys {
            name = "Unconscious Bias",
            explanation = "You made decisions without realizing you were influenced by personal preferences or stereotypes."
        },
        ["BandwagonEffect"] = new CogBiasKeys {
            name = "Bandwagon Effect",
            explanation = "You chose something because others were doing the same."
        },
        ["AffectHeuristic"] = new CogBiasKeys {
            name = "Affect Heuristic",
            explanation = "You let emotions or gut feelings guide your decision rather than facts."
        },
        ["LossAversion"] = new CogBiasKeys {
            name = "Loss Aversion",
            explanation = "You prefer avoiding losses more than acquiring equivalent gains, making you more sensitive to potential losses."
        },
        ["OutcomeBias"] = new CogBiasKeys {
            name = "Outcome Bias",
            explanation = "You judged a decision based on the outcome rather than how the decision was made."
        },
        ["SunkCostFallacy"] = new CogBiasKeys {
            name = "Sunk Cost Fallacy",
            explanation = "You continued with a decision because you had already invested time, effort, or resources, even when it was no longer the best choice."
        },
        ["NoveltyBias"] = new CogBiasKeys {
            name = "Novelty Bias",
            explanation = "You favored something just because it felt new or different."
        },
        ["AmbiguityEffect"] = new CogBiasKeys {
            name = "Ambiguity Effect",
            explanation = "You avoided a choice because it seemed unclear or uncertain."
        },
        ["OptimismBias"] = new CogBiasKeys {
            name = "Optimism Bias",
            explanation = "You overestimated the likelihood of positive outcomes."
        },
        ["NormalcyBias"] = new CogBiasKeys {
            name = "Normalcy Bias",
            explanation = "You assumed things would continue as normal, even during disruptions."
        },
        ["AmbiguityAversion"] = new CogBiasKeys {
            name = "Ambiguity Aversion",
            explanation = "You preferred known risks over unknown risks, even if the unknown could be better."
        },
        ["DiffusionOfResponsibility"] = new CogBiasKeys {
            name = "Diffusion of Responsibility",
            explanation = "You assumed someone else would act, so you didn’t feel personally responsible."
        },
        ["FairnessBias"] = new CogBiasKeys {
            name = "Fairness Bias / Moral Credentialing",
            explanation = "You tried to be neutral or fair, believing it protected you from bias or bad outcomes."
        },
        ["AvailabilityBias"] = new CogBiasKeys {
            name = "Availability Bias",
            explanation = "You relied on information that was recent or memorable, rather than accurate."
        },
        ["IncentiveBias"] = new CogBiasKeys {
            name = "Incentive Bias",
            explanation = "You favored options that gave you personal benefits, even if not ideal overall."
        },
        ["AuthorityBias"] = new CogBiasKeys {
            name = "Authority Bias",
            explanation = "You trusted someone because of their position/status, even if it wasn’t fully justified."
        },
        ["ConfirmationBias"] = new CogBiasKeys {
            name = "Confirmation Bias",
            explanation = "You focused on information that supported what you already believed, while ignoring evidence that contradicted it."
        },

        ["OmissionBias"] = new CogBiasKeys {
            name = "Omission Bias",
            explanation = "You viewed harmful inactions as less bad than harmful actions, even with the same outcome."
        },
        ["FramingEffect"] = new CogBiasKeys {
            name = "Framing Effect",
            explanation = "You were influenced by how information was presented rather than what was presented."
        },
        ["NegativityAvoidance"] = new CogBiasKeys {
            name = "Negativity Avoidance",
            explanation = "You avoided negative information to maintain a positive emotional state."
        },
        ["NeglectOfProbability"] = new CogBiasKeys {
            name = "Neglect of Probability",
            explanation = "You ignored how likely an event was when making a decision."
        },
        ["PresentBias"] = new CogBiasKeys {
            name = "Present Bias",
            explanation = "You prioritized short-term rewards over long-term benefits."
        },
        ["MoralLicensing"] = new CogBiasKeys {
            name = "Moral Licensing",
            explanation = "You justified a poor choice because you felt you had earned it with previous good behavior."
        },
        ["PlanningFallacy"] = new CogBiasKeys {
            name = "Planning Fallacy",
            explanation = "You underestimated how long or difficult a task would be, despite past experience."
        },
        ["ReactanceBias"] = new CogBiasKeys {
            name = "Reactance Bias",
            explanation = "You resisted a suggestion or influence because it felt like your freedom to choose was being restricted."
        },
        ["Custom"] = new CogBiasKeys {
            name = "Other / Custom Reason",
            explanation = "This was your own reason and may not reflect a known cognitive bias."
        }
    };
}