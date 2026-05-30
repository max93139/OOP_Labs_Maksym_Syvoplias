using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Аналізує ризики для здоров'я водія на основі показань сенсорів.
/// </summary>
public sealed class DriverHealthDiagnostics
{
    private double _warningScore;
    private string _systemName;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public DriverHealthDiagnostics()
    {
        _warningScore = 65.0;
        _systemName = "Active Health Monitoring System";
    }

    /// <summary>
    /// Конструктор з повним набором конфігурацій.
    /// </summary>
    public DriverHealthDiagnostics(double warningScore, string systemName)
    {
        _warningScore = warningScore;
        _systemName = systemName;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public DriverHealthDiagnostics(DriverHealthDiagnostics other)
    {
        _warningScore = other.WarningScore;
        _systemName = other.SystemName;
    }

    /// <summary>
    /// Повертає поріг попередження.
    /// </summary>
    public double WarningScore
    {
        get
        {
            return _warningScore;
        }
        set
        {
            _warningScore = value;
        }
    }

    /// <summary>
    /// Повертає назву системи діагностики.
    /// </summary>
    public string SystemName
    {
        get
        {
            return _systemName;
        }
        set
        {
            _systemName = value;
        }
    }

    /// <summary>
    /// Обчислює компактний показник ризику для здоров'я.
    /// </summary>
    public double CalculateHealthRisk(IReadOnlyList<SensorReading> readings)
    {
        double score = 0.0;

        foreach (SensorReading reading in readings)
        {
            score += CalculateReadingRisk(reading);
        }

        return Math.Min(score, 100.0);
    }

    /// <summary>
    /// Формує медичну рекомендацію на основі показника ризику.
    /// </summary>
    public string BuildRecommendation(double riskScore)
    {
        string recommendation;

        if (riskScore >= _warningScore)
        {
            recommendation = "Медичний моніторинг рекомендує автопілот і спокійний маршрут.";
        }
        else
        {
            recommendation = "Медичний моніторинг дозволяє звичайний рух.";
        }

        return recommendation;
    }

    private double CalculateReadingRisk(SensorReading reading)
    {
        double risk;
        if (reading.Name == "Pulse")
        {
            risk = Math.Max(0.0, reading.Value - 80.0) * 0.8;
        }
        else if (reading.Name == "Blood pressure")
        {
            risk = Math.Max(0.0, reading.Value - 120.0) * 0.5;
        }
        else if (reading.Name == "Eye fatigue")
        {
            risk = reading.Value * 0.7;
        }
        else
        {
            risk = 0.0;
        }

        return risk;
    }
}
