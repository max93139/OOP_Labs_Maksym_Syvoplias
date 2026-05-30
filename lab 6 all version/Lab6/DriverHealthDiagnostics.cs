namespace Lab6;

/// <summary>
/// Аналізує ризики для здоров'я водія на основі показань сенсорів.
/// </summary>
public sealed class DriverHealthDiagnostics
{
    private const double WARNING_SCORE = 65.0;

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

        if (riskScore >= WARNING_SCORE)
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
        double risk = reading.Name switch
        {
            "Pulse" => Math.Max(0.0, reading.Value - 80.0) * 0.8,
            "Blood pressure" => Math.Max(0.0, reading.Value - 120.0) * 0.5,
            "Eye fatigue" => reading.Value * 0.7,
            _ => 0.0
        };

        return risk;
    }
}
