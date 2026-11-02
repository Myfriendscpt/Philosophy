using System;
using System.Collections.Generic;

namespace Philosophy.Mereology;

/// <summary>
/// A temporal slice or stage of an entity existing at a discrete time point.
/// </summary>
public sealed record TemporalSlice(DateTime Timestamp, string ManifestState);

/// <summary>
/// Theories of identity and persistence through change over time.
/// </summary>
public abstract record IdentityTheory
{
    private IdentityTheory() { }

    /// <summary>
    /// Aristotle/Fine Endurantism: Entity persists wholly across time by virtue of its substantial form/essence.
    /// </summary>
    public sealed record Endurantism(string EssenceForm, Guid FormId) : IdentityTheory;

    /// <summary>
    /// Lewis/Sider Perdurantism: Entity is a 4D spacetime worm composed of temporal slices.
    /// </summary>
    public sealed record Perdurantism(IReadOnlyList<TemporalSlice> WormSlices) : IdentityTheory;

    /// <summary>
    /// Mereological Sum: Identity is strictly defined as the extensional sum of its constituent components.
    /// If components change, the mereological sum changes.
    /// </summary>
    public sealed record MereologicalSum(IReadOnlyList<string> ComponentParts) : IdentityTheory;

    public TResult Match<TResult>(
        Func<Endurantism, TResult> onEndurantism,
        Func<Perdurantism, TResult> onPerdurantism,
        Func<MereologicalSum, TResult> onMereologicalSum)
    {
        return this switch
        {
            Endurantism e => onEndurantism(e),
            Perdurantism p => onPerdurantism(p),
            MereologicalSum m => onMereologicalSum(m),
            _ => throw new InvalidOperationException("Unreachable: Closed Identity Hierarchy")
        };
    }
}

