using System;
using System.Diagnostics;
using System.IO;

namespace Philosophy.Z3;

/// <summary>
/// Compiles the formal philosophical types into Z3 SMT-LIB2 Algebraic Datatype syntax
/// and evaluates theorem consistency and satisfiability directly with the Windows Z3 solver.
/// </summary>
public static class PhilosophyZ3
{
    private static readonly string[] PossibleZ3Paths =
    [
        @"C:\Users\outsh\AppData\Local\Programs\Python\Python313\bin\z3.exe",
        @"C:\Users\outsh\AppData\Local\Programs\Python\Python313\Scripts\z3.cmd"
    ];

    /// <summary>
    /// SMT-LIB2 compilation syntax for EpistemicState<T> (Epistemology).
    /// </summary>
    public static string GetEpistemicSort() => """
        (declare-datatypes (T) (
          (EpistemicState
            (Known (knownProp T) (warrant String))
            (Believed (believedProp T) (credence Real))
            (Suspended (suspendedProp T) (reason String))
            (Refuted (refutedProp T) (defeater String))
          )
        ))
        """;

    /// <summary>
    /// SMT-LIB2 compilation syntax for MoralVerdict (Deontic Ethics).
    /// </summary>
    public static string GetMoralVerdictSort() => """
        (declare-datatypes () (
          (MoralVerdict
            (Obligatory (duty String))
            (Permissible (utility Real))
            (Supererogatory (virtue String))
            (Impermissible (violation String))
          )
        ))
        """;

    /// <summary>
    /// SMT-LIB2 compilation syntax for ModalStatus<T> (Modal Logic).
    /// </summary>
    public static string GetModalSort() => """
        (declare-datatypes (T) (
          (ModalStatus
            (Necessary (necessaryTruth T) (proof String))
            (Contingent (contingentTruth T) (world String))
            (Impossible (contradiction String))
          )
        ))
        """;

    /// <summary>
    /// SMT-LIB2 compilation syntax for IdentityTheory (Mereology).
    /// </summary>
    public static string GetMereologySort() => """
        (declare-datatypes () (
          (IdentityTheory
            (Endurantism (essence String) (formId Int))
            (Perdurantism (wormSliceCount Int))
            (MereologicalSum (partCount Int))
          )
        ))
        """;

    /// <summary>
    /// SMT-LIB2 compilation syntax for DialecticStage<T> (Hegelian Dialectics).
    /// </summary>
    public static string GetDialecticSort() => """
        (declare-datatypes (T) (
          (DialecticStage
            (Thesis (assertion T))
            (Antithesis (negation T) (tension String))
            (Synthesis (unified T) (transcendence String))
          )
        ))
        """;

    /// <summary>
    /// Resolves the installed z3.exe binary on the Windows system.
    /// </summary>
    public static string ResolveZ3Path()
    {
        foreach (var path in PossibleZ3Paths)
        {
            if (File.Exists(path)) return path;
        }
        return "z3";
    }

    /// <summary>
    /// Pipes SMT-LIB2 commands directly into the Windows Z3 solver and returns the theorem prover output.
    /// </summary>
    public static string Verify(string smt2Commands)
    {
        string z3Path = ResolveZ3Path();

        ProcessStartInfo psi = z3Path.EndsWith(".cmd", StringComparison.OrdinalIgnoreCase)
            ? new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c \"\"{z3Path}\" -in\"",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
            : new ProcessStartInfo
            {
                FileName = z3Path,
                Arguments = "-in",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

        using var process = Process.Start(psi)
            ?? throw new InvalidOperationException($"Failed to launch Z3 at '{z3Path}' on Windows.");

        process.StandardInput.Write(smt2Commands);
        process.StandardInput.Close();

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (!string.IsNullOrWhiteSpace(error))
        {
            throw new InvalidOperationException($"Z3 Solver Error: {error}");
        }

        return output.Trim();
    }
}
