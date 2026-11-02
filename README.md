# Philosophy.dll — Unified Formal Philosophy & Automated Reasoning

A comprehensive .NET class library bringing **Formal Epistemology**, **Normative Ethics**, **Modal Logic**, **Mereology**, and **Hegelian Dialectics** into type-safe C# discriminated unions with automated **Z3 SMT theorem verification**.

---

## 1. System DLL Locations

The multi-targeted Release DLLs and NuGet package have been installed on your Windows system:

| Target Framework | DLL Path |
| :--- | :--- |
| **.NET 8.0 (LTS)** | dotnet\lib\Philosophy\net8.0\Philosophy.dll` |
| **.NET 10.0** | .dotnet\lib\Philosophy\net10.0\Philosophy.dll` |
| **.NET 11.0** | .dotnet\lib\Philosophy\net11.0\Philosophy.dll` |
| **NuGet Package** | .dotnet\packages\Philosophy.1.0.0.nupkg` |

---

## 2. Using Philosophy.dll in Any .NET Project

### Direct Reference in `.csproj`:
```xml
<ItemGroup>
  <Reference Include="Philosophy">
    <HintPath>\.dotnet\lib\Philosophy\net8.0\Philosophy.dll</HintPath>
  </Reference>
</ItemGroup>
```

### Local NuGet Package Installation:
```powershell
dotnet add package Philosophy --source .dotnet\packages
```

---

## 3. The 5 Unified Philosophical Domains

### [1] `Philosophy.Epistemics` (Knowledge & Belief)
Grounded in Plato's Justified True Belief (JTB) and defeasible epistemology:
```csharp
using Philosophy.Epistemics;

EpistemicState<string> belief = new EpistemicState<string>.Known(
    "Cogito, ergo sum",
    new Warrant("Cartesian radical doubt proof", IsEmpirical: false, IsAxiomatic: true)
);

string status = belief switch
{
    EpistemicState<string>.Known k => $"Known: {k.Proposition}",
    EpistemicState<string>.Believed b => $"Credence: {b.Credence:P0}",
    EpistemicState<string>.Suspended s => $"Epoché: {s.Reason}",
    EpistemicState<string>.Refuted r => $"Defeated by: {r.Defeater.CounterClaim}",
    _ => "Unreachable"
};
```

### [2] `Philosophy.Ethics` (Deontic Moral Logic)
Computes verdicts across Deontology (Kant), Utilitarianism (Mill), and Virtue Ethics (Aristotle):
```csharp
using Philosophy.Ethics;

MoralVerdict verdict = new MoralVerdict.Obligatory("Protect vulnerable innocents");
// Variants: Obligatory (O), Permissible (P), Supererogatory (S), Impermissible (F)
```

### [3] `Philosophy.ModalLogic` (Kripke Possible Worlds)
Evaluates truth modalities across possible world semantics ($\Box P$ vs $\Diamond P$):
```csharp
using Philosophy.ModalLogic;

ModalStatus<string> truth = new ModalStatus<string>.Necessary(
    "2 + 2 = 4", 
    "Peano Axioms valid in all accessible worlds"
);
// Variants: Necessary (□), Contingent (◇), Impossible (□¬)
```

### [4] `Philosophy.Mereology` (Parts & Identity through Time)
Formally models parthood and the classical **Ship of Theseus** identity problem:
```csharp
using Philosophy.Mereology;

IdentityTheory view = new IdentityTheory.Endurantism("The Vessel of Theseus", Guid.NewGuid());
// Variants: Endurantism (Substantial Form), Perdurantism (4D Spacetime Worm), MereologicalSum (Sum of Parts)
```

### [5] `Philosophy.Dialectics` (Hegelian Aufheben)
Encodes thesis, antithesis, and sublation (synthesis):
```csharp
using Philosophy.Dialectics;

DialecticStage<string> dialectic = new DialecticStage<string>.Synthesis(
    "Constitutional Democratic Rule of Law",
    "Sublation (Aufheben): Liberty bounded by social contract protects freedom while ensuring collective peace."
);
// Variants: Thesis, Antithesis, Synthesis
```

---

## 4. Automated Z3 SMT Theorem Verification

The library provides [`PhilosophyZ3`] which compiles all philosophical sorts into Z3 SMT-LIB2 syntax and verifies them against the installed Windows Z3 solver:

```csharp
using Philosophy.Z3;

string smtScript = $"""
{PhilosophyZ3.GetMoralVerdictSort()}
{PhilosophyZ3.GetDialecticSort()}

(declare-const v MoralVerdict)
(assert ((_ is Supererogatory) v))

(check-sat)
(get-model)
""";

string result = PhilosophyZ3.Verify(smtScript);
Console.WriteLine(result); // Outputs: sat
```

