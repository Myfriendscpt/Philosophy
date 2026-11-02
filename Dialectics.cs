using System;

namespace Philosophy.Dialectics;

/// <summary>
/// Classical logical fallacies in dialectical discourse.
/// </summary>
public enum FallacyKind
{
    None = 0,
    AdHominem,
    StrawMan,
    BeggingTheQuestion,
    FalseDilemma,
    AffirmingTheConsequent
}

/// <summary>
/// Closed discriminated union representing Hegelian / Marxist Dialectical progression (Aufheben).
/// </summary>
public abstract record DialecticStage<T>
{
    private DialecticStage() { }

    /// <summary>
    /// Initial proposition or status quo assertion.
    /// </summary>
    public sealed record Thesis(T Assertion) : DialecticStage<T>;

    /// <summary>
    /// Negation and contradiction emerging from internal tensions in the thesis.
    /// </summary>
    public sealed record Antithesis(T Negation, string TensionReason) : DialecticStage<T>;

    /// <summary>
    /// Sublation (Aufheben): Preserves the partial truths of thesis and antithesis while transcending their contradiction.
    /// </summary>
    public sealed record Synthesis(T UnifiedConcept, string Transcendence) : DialecticStage<T>;

    public TResult Match<TResult>(
        Func<Thesis, TResult> onThesis,
        Func<Antithesis, TResult> onAntithesis,
        Func<Synthesis, TResult> onSynthesis)
    {
        return this switch
        {
            Thesis t => onThesis(t),
            Antithesis a => onAntithesis(a),
            Synthesis s => onSynthesis(s),
            _ => throw new InvalidOperationException("Unreachable: Closed Dialectical Hierarchy")
        };
    }
}

