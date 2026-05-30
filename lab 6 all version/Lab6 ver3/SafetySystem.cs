using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Захищає пасажирів через узгодження захисних реакцій та оцінку сили можливого зіткнення.
/// </summary>
public sealed class SafetySystem
{
    private string _systemLevel;
    private string _activeThreat;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public SafetySystem()
    {
        _systemLevel = "Level 5 Active Autonomous Safety";
        _activeThreat = "None";
    }

    /// <summary>
    /// Конструктор з повним набором конфігурацій.
    /// </summary>
    public SafetySystem(string systemLevel, string activeThreat)
    {
        _systemLevel = systemLevel;
        _activeThreat = activeThreat;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public SafetySystem(SafetySystem other)
    {
        _systemLevel = other.SystemLevel;
        _activeThreat = other.ActiveThreat;
    }

    /// <summary>
    /// Повертає або встановлює рівень безпеки системи.
    /// </summary>
    public string SystemLevel
    {
        get
        {
            return _systemLevel;
        }
        set
        {
            _systemLevel = value;
        }
    }

    /// <summary>
    /// Повертає або встановлює поточну активну загрозу.
    /// </summary>
    public string ActiveThreat
    {
        get
        {
            return _activeThreat;
        }
        set
        {
            _activeThreat = value;
        }
    }

    /// <summary>
    /// Оцінює потенційне перевантаження при зіткненні та активує відповідні ступені захисту.
    /// </summary>
    public IReadOnlyList<string> ActivateProtection(string threatName)
    {
        _activeThreat = threatName;
        ThreatAssessment assessment = EvaluateThreat(threatName);

        List<string> responseLines = new List<string>
        {
            $"Виявлено загрозу: {threatName}.",
            $"Серйозність загрози: {assessment.Score:F1}/10.0 ({assessment.Analysis}).",
            $"Потенційне перевантаження при зіткненні: {assessment.GForce:F1}G."
        };

        responseLines.AddRange(DeployCountermeasures(assessment.GForce));

        return responseLines;
    }

    private ThreatAssessment EvaluateThreat(string threatName)
    {
        double estimatedThreatScore;
        double potentialGForce;
        string threatAnalysis;

        string normalizedThreat = threatName.ToLowerInvariant();
        if (normalizedThreat.Contains("wet", StringComparison.Ordinal) && normalizedThreat.Contains("tired", StringComparison.Ordinal) ||
            normalizedThreat.Contains("мокр", StringComparison.Ordinal) && normalizedThreat.Contains("втомл", StringComparison.Ordinal))
        {
            estimatedThreatScore = 7.8;
            potentialGForce = 12.4;
            threatAnalysis = "Високий ризик: знижена пильність водія та слизьке покриття.";
        }
        else if (normalizedThreat.Contains("collision", StringComparison.Ordinal) || normalizedThreat.Contains("impact", StringComparison.Ordinal) ||
                 normalizedThreat.Contains("зіткн", StringComparison.Ordinal) || normalizedThreat.Contains("удар", StringComparison.Ordinal))
        {
            estimatedThreatScore = 9.5;
            potentialGForce = 42.0;
            threatAnalysis = "КРИТИЧНО: Виявлено неминуче зіткнення.";
        }
        else
        {
            estimatedThreatScore = 2.4;
            potentialGForce = 1.2;
            threatAnalysis = "Низька загроза для руху.";
        }

        return new ThreatAssessment(estimatedThreatScore, potentialGForce, threatAnalysis);
    }

    private List<string> DeployCountermeasures(double gForce)
    {
        List<string> countermeasures = new List<string>();

        if (gForce > 10.0)
        {
            countermeasures.Add("Подушки безпеки повністю приведені в готовність, системи передпуску активні.");
            countermeasures.Add("Переднатягувачі ременів безпеки затягнуті з силою 250N.");
            countermeasures.Add("Гальмівна магістраль високого тиску попередньо закачана.");
            countermeasures.Add("Амортизатори підвіски миттєво стали жорсткими для максимального контролю.");
        }
        else if (gForce > 3.0)
        {
            countermeasures.Add("Подушки безпеки в режимі очікування.");
            countermeasures.Add("Переднатягувачі ременів безпеки затягнуті з силою 100N.");
        }
        else
        {
            countermeasures.Add("Пасажирський салон заблоковано.");
        }

        return countermeasures;
    }
}
