using System;

namespace Philosophy.Ethics;

/// <summary>
/// Deontic moral verdict for an action: Obligatory (O), Permissible (P), Supererogatory (S), Impermissible (F).
/// </summary>
public abstract record MoralVerdict
{
    private MoralVerdict() { }

    /// <summary>
    /// Mandatory moral duty (Categorical Imperative).
    /// </summary>
    public sealed record Obligatory(string Duty) : MoralVerdict;

    /// <summary>
    /// Morally permissible action with expected net utility.
    /// </summary>
    public sealed record Permissible(double NetUtility) : MoralVerdict;

    /// <summary>
    /// Heroic or saintly act going beyond duty (Supererogation).
    /// </summary>
    public sealed record Supererogatory(string Virtue) : MoralVerdict;

    /// <summary>
    /// Morally forbidden action violating categorical or utility bounds.
    /// </summary>
    public sealed record Impermissible(string Violation) : MoralVerdict;

    public TResult Match<TResult>(
        Func<Obligatory, TResult> onObligatory,
        Func<Permissible, TResult> onPermissible,
        Func<Supererogatory, TResult> onSupererogatory,
        Func<Impermissible, TResult> onImpermissible)
    {
        return this switch
        {
            Obligatory o => onObligatory(o),
            Permissible p => onPermissible(p),
            Supererogatory s => onSupererogatory(s),
            Impermissible i => onImpermissible(i),
            _ => throw new InvalidOperationException("Unreachable: Closed Moral Hierarchy")
        };
    }
}

/// <summary>
/// Classical normative ethical frameworks.
/// </summary>
public abstract record EthicalFramework
{
    private EthicalFramework() { }

    public sealed record Deontology(string UniversalMaxim, bool RespectsAutonomy) : EthicalFramework;
    public sealed record Utilitarianism(double TotalHappiness, double TotalSuffering) : EthicalFramework
    {
        public double NetUtility => TotalHappiness - TotalSuffering;
    }
    public sealed record VirtueEthics(string CardinalVirtue, string ViceDeficiency, string ViceExcess) : EthicalFramework;
}

