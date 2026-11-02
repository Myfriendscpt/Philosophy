using System;

namespace Philosophy.Epistemics;

/// <summary>
/// Justification warrant for high-confidence knowledge.
/// </summary>
public sealed record Warrant(string Description, bool IsEmpirical, bool IsAxiomatic);

/// <summary>
/// A counter-argument or defeating evidence that overturns prior belief.
/// </summary>
public sealed record Defeater(string CounterClaim, string DefeatingEvidence);

/// <summary>
/// Closed discriminated union representing an agent's cognitive epistemic state towards a proposition.
/// Grounded in Plato's Justified True Belief (JTB) and defeasible epistemology.
/// </summary>
public abstract record EpistemicState<T>
{
    private EpistemicState() { }

    /// <summary>
    /// High-confidence knowledge backed by infallible or defeasible justification.
    /// </summary>
    public sealed record Known(T Proposition, Warrant Evidence) : EpistemicState<T>;

    /// <summary>
    /// Belief with subjective Bayesian credence (0.0 to 1.0).
    /// </summary>
    public sealed record Believed(T Proposition, double Credence) : EpistemicState<T>;

    /// <summary>
    /// Pyrrhonian Epoché: Conscious suspension of judgment due to balanced reasons.
    /// </summary>
    public sealed record Suspended(T Proposition, string Reason) : EpistemicState<T>;

    /// <summary>
    /// Active refutation by counter-example or formal contradiction.
    /// </summary>
    public sealed record Refuted(T Proposition, Defeater Defeater) : EpistemicState<T>;

    public TResult Match<TResult>(
        Func<Known, TResult> onKnown,
        Func<Believed, TResult> onBelieved,
        Func<Suspended, TResult> onSuspended,
        Func<Refuted, TResult> onRefuted)
    {
        return this switch
        {
            Known k => onKnown(k),
            Believed b => onBelieved(b),
            Suspended s => onSuspended(s),
            Refuted r => onRefuted(r),
            _ => throw new InvalidOperationException("Unreachable: Closed Epistemic Hierarchy")
        };
    }
}

