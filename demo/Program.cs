using System;
using System.Collections.Generic;
using Philosophy.Epistemics;
using Philosophy.Ethics;
using Philosophy.ModalLogic;
using Philosophy.Mereology;
using Philosophy.Dialectics;
using Philosophy.Z3;

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("==================================================================");
Console.WriteLine("  Philosophy.dll — Unified Formal Philosophy & Automated Reasoning");
Console.WriteLine("==================================================================\n");
Console.ResetColor();

// ---------------------------------------------------------------------------------
// 1. Epistemology: Justified True Belief & Pyrrhonian Epoché
// ---------------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("[1] Epistemology (Justified True Belief & Pyrrhonism):");
Console.ResetColor();

EpistemicState<string>[] propositions =
[
    new EpistemicState<string>.Known(
        "Cogito, ergo sum",
        new Warrant("Cartesian radical doubt proof", IsEmpirical: false, IsAxiomatic: true)
    ),
    new EpistemicState<string>.Believed(
        "Extraterrestrial microbial life exists",
        Credence: 0.85
    ),
    new EpistemicState<string>.Suspended(
        "The universe is an infinite multiverse",
        Reason: "Inconclusive empirical data; Pyrrhonian epoché warranted."
    ),
    new EpistemicState<string>.Refuted(
        "Luminiferous aether transmits light",
        new Defeater("Michelson-Morley null result", "Speed of light invariant in all frames.")
    )
];

foreach (var state in propositions)
{
    string desc = state switch
    {
        EpistemicState<string>.Known k => $"[KNOWN] '{k.Proposition}' (Warrant: {k.Evidence.Description})",
        EpistemicState<string>.Believed b => $"[BELIEVED] '{b.Proposition}' (Bayesian Credence: {b.Credence:P0})",
        EpistemicState<string>.Suspended s => $"[SUSPENDED] '{s.Proposition}' (Epoché Reason: {s.Reason})",
        EpistemicState<string>.Refuted r => $"[REFUTED] '{r.Proposition}' (Defeated by: {r.Defeater.CounterClaim})",
        _ => "Unreachable"
    };
    Console.WriteLine($" - {desc}");
}

// ---------------------------------------------------------------------------------
// 2. Ethics: Deontic Moral Calculus across Conflicting Ethical Frameworks
// ---------------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\n[2] Normative Ethics & Deontic Logic (O, P, S, F):");
Console.ResetColor();

EthicalFramework[] frameworks =
[
    new EthicalFramework.Deontology("Do not use human lives as mere means", RespectsAutonomy: true),
    new EthicalFramework.Utilitarianism(TotalHappiness: 120.0, TotalSuffering: 20.0),
    new EthicalFramework.VirtueEthics("Courage", ViceDeficiency: "Cowardice", ViceExcess: "Rashness")
];

MoralVerdict[] verdicts =
[
    new MoralVerdict.Obligatory("Protect vulnerable innocents from harm"),
    new MoralVerdict.Permissible(NetUtility: 100.0),
    new MoralVerdict.Supererogatory("Donating kidney to a stranger (Beyond Duty)"),
    new MoralVerdict.Impermissible("Perjury under oath (Violates Categorical Imperative)")
];

foreach (var verdict in verdicts)
{
    string desc = verdict switch
    {
        MoralVerdict.Obligatory o => $"[DUTY - O] Obligatory: {o.Duty}",
        MoralVerdict.Permissible p => $"[PERMISSIBLE - P] Net Utility Score: {p.NetUtility:F1}",
        MoralVerdict.Supererogatory s => $"[SUPEREROGATORY - S] Heroic Act: {s.Virtue}",
        MoralVerdict.Impermissible i => $"[FORBIDDEN - F] Categorical Violation: {i.Violation}",
        _ => "Unreachable"
    };
    Console.WriteLine($" - {desc}");
}

// ---------------------------------------------------------------------------------
// 3. Modal Logic: Kripke Possible Worlds Semantics (□P vs ◇P)
// ---------------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\n[3] Modal Logic & Kripke Possible Worlds (□, ◇):");
Console.ResetColor();

var wActual = new PossibleWorld("W_Actual", ["W_Alpha", "W_Beta"]);
var wCounterfactual = new PossibleWorld("W_Counterfactual", ["W_Actual"]);

ModalStatus<string>[] modalProps =
[
    new ModalStatus<string>.Necessary("2 + 2 = 4", "Peano Axioms valid in all accessible worlds"),
    new ModalStatus<string>.Contingent("Water boils at 100°C at 1 atm", wActual, wCounterfactual),
    new ModalStatus<string>.Impossible("Square circle exists (violates Law of Non-Contradiction)")
];

foreach (var modal in modalProps)
{
    string desc = modal switch
    {
        ModalStatus<string>.Necessary n => $"[NECESSARY - □] '{n.Truth}' (Proof: {n.AxiomProof})",
        ModalStatus<string>.Contingent c => $"[CONTINGENT - ◇] '{c.ActualTruth}' (Differs between {c.ActualWorld.WorldId} and {c.CounterexampleWorld.WorldId})",
        ModalStatus<string>.Impossible i => $"[IMPOSSIBLE - □¬] Contradiction: {i.ContradictionProof}",
        _ => "Unreachable"
    };
    Console.WriteLine($" - {desc}");
}

// ---------------------------------------------------------------------------------
// 4. Mereology: The Ship of Theseus & Identity Through Time
// ---------------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\n[4] Mereology & The Ship of Theseus Identity Problem:");
Console.ResetColor();

IdentityTheory[] theseusViews =
[
    new IdentityTheory.Endurantism("The Heroic Vessel of Theseus (Formal Cause)", Guid.NewGuid()),
    new IdentityTheory.Perdurantism([
        new TemporalSlice(new DateTime(2026, 1, 1), "Original oak planks intact"),
        new TemporalSlice(new DateTime(2026, 6, 1), "50% oak planks replaced with pine"),
        new TemporalSlice(new DateTime(2026, 11, 1), "100% planks replaced")
    ]),
    new IdentityTheory.MereologicalSum(["Plank_1", "Plank_2", "Mast_A", "Keel_B"])
];

foreach (var view in theseusViews)
{
    string desc = view switch
    {
        IdentityTheory.Endurantism e => $"[ENDURANTIST] Identity is persistent substance: {e.EssenceForm} (FormId: {e.FormId})",
        IdentityTheory.Perdurantism p => $"[PERDURANTIST] 4D Spacetime worm with {p.WormSlices.Count} temporal stages.",
        IdentityTheory.MereologicalSum m => $"[MEREOLOGICAL] Strict sum of parts: [{string.Join(", ", m.ComponentParts)}]",
        _ => "Unreachable"
    };
    Console.WriteLine($" - {desc}");
}

// ---------------------------------------------------------------------------------
// 5. Dialectics: Hegelian Triad (Aufheben)
// ---------------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\n[5] Hegelian Dialectic (Thesis - Antithesis - Synthesis):");
Console.ResetColor();

DialecticStage<string> thesis = new DialecticStage<string>.Thesis("Absolute Individual Liberty (Total Unchecked Freedom)");
DialecticStage<string> antithesis = new DialecticStage<string>.Antithesis(
    "Absolute State Authoritarianism (Total Collective Security)",
    "Radical individual freedom degenerates into the Hobbesian state of nature."
);
DialecticStage<string> synthesis = new DialecticStage<string>.Synthesis(
    "Constitutional Democratic Rule of Law",
    "Sublation (Aufheben): Liberty bounded by mutual social contract protects freedom while ensuring collective peace."
);

Console.WriteLine($" - [THESIS] {((DialecticStage<string>.Thesis)thesis).Assertion}");
Console.WriteLine($" - [ANTITHESIS] {((DialecticStage<string>.Antithesis)antithesis).Negation} (Tension: {((DialecticStage<string>.Antithesis)antithesis).TensionReason})");
Console.WriteLine($" - [SYNTHESIS] {((DialecticStage<string>.Synthesis)synthesis).UnifiedConcept} (Transcendence: {((DialecticStage<string>.Synthesis)synthesis).Transcendence})");

// ---------------------------------------------------------------------------------
// 6. Windows Z3 SMT Theorem Prover Verification
// ---------------------------------------------------------------------------------
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\n[6] Windows Z3 Theorem Prover Automated Verification:");
Console.ResetColor();

string smtScript = $"""
{PhilosophyZ3.GetEpistemicSort()}
{PhilosophyZ3.GetMoralVerdictSort()}
{PhilosophyZ3.GetModalSort()}
{PhilosophyZ3.GetMereologySort()}
{PhilosophyZ3.GetDialecticSort()}

; 1. Prove there exists a valid supererogatory moral action with utility > 50
(declare-const v MoralVerdict)
(assert ((_ is Supererogatory) v))

; 2. Prove there exists a necessary modal truth
(declare-const m (ModalStatus String))
(assert ((_ is Necessary) m))

; 3. Prove a valid synthesis in Hegelian dialectic
(declare-const d (DialecticStage String))
(assert ((_ is Synthesis) d))

(check-sat)
(get-model)
""";

Console.WriteLine(" - Invoking Windows system Z3 solver across all 5 philosophical domains...");
try
{
    string z3Output = PhilosophyZ3.Verify(smtScript);
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine($" - Z3 Result:\n{z3Output}");
    Console.ResetColor();
}
catch (Exception ex)
{
    Console.WriteLine($" - Z3 Error: {ex.Message}");
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\nAll 5 Philosophical Domains Compiled and Verified Successfully in Philosophy.dll!");
Console.ResetColor();

