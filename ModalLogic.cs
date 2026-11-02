using System;
using System.Collections.Generic;

namespace Philosophy.ModalLogic;

/// <summary>
/// A Saul Kripke Possible World in modal semantics.
/// </summary>
public sealed record PossibleWorld(string WorldId, IReadOnlyList<string> AccessibleWorlds);

/// <summary>
/// Modal truth status of a proposition across possible worlds.
/// </summary>
public abstract record ModalStatus<T>
{
    private ModalStatus() { }

    /// <summary>
    /// True across all accessible possible worlds (Necessity: □P).
    /// </summary>
    public sealed record Necessary(T Truth, string AxiomProof) : ModalStatus<T>;

    /// <summary>
    /// True in the actual world, but counterfactually false in at least one accessible world (Contingency: P ∧ ◇¬P).
    /// </summary>
    public sealed record Contingent(T ActualTruth, PossibleWorld ActualWorld, PossibleWorld CounterexampleWorld) : ModalStatus<T>;

    /// <summary>
    /// False in all possible worlds due to formal contradiction (Impossibility: □¬P).
    /// </summary>
    public sealed record Impossible(string ContradictionProof) : ModalStatus<T>;

    public TResult Match<TResult>(
        Func<Necessary, TResult> onNecessary,
        Func<Contingent, TResult> onContingent,
        Func<Impossible, TResult> onImpossible)
    {
        return this switch
        {
            Necessary n => onNecessary(n),
            Contingent c => onContingent(c),
            Impossible i => onImpossible(i),
            _ => throw new InvalidOperationException("Unreachable: Closed Modal Hierarchy")
        };
    }
}

