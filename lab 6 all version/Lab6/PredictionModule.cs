namespace Lab6;

/// <summary>
/// Прогнозує критичні ситуації на основі об'єднаних значень ризику.
/// </summary>
public sealed class PredictionModule
{
    /// <summary>
    /// Обчислює ймовірність аварії.
    /// </summary>
    public double CalculateAccidentProbability(double driverRisk, double environmentRisk)
    {
        double weightedRisk = driverRisk * 0.6 + environmentRisk * 0.4;
        return Math.Min(weightedRisk, 100.0);
    }

    /// <summary>
    /// Формує зрозуміле повідомлення прогнозу.
    /// </summary>
    public string BuildForecast(double accidentProbability)
    {
        string forecast;

        if (accidentProbability >= 50.0)
        {
            forecast = "Можливий критичний стан. Вибрано оборонний маршрут.";
        }
        else
        {
            forecast = "Критичний стан малоймовірний. Стандартний маршрут безпечний.";
        }

        return forecast;
    }
}
