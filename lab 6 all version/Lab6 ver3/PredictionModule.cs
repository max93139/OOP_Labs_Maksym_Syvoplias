namespace Lab6;

/// <summary>
/// Прогнозує критичні ситуації на основі об'єднаних значень ризику.
/// </summary>
public sealed class PredictionModule : SmartDevice
{
    /// <summary>
    /// Ініціалізує новий модуль прогнозування дорожніх ризиків.
    /// </summary>
    public PredictionModule() : base("Модуль прогнозування дорожніх ризиків", 0.30)
    {
    }
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

    /// <summary>
    /// Повертає статус модуля прогнозування.
    /// </summary>
    public override string GetStatus()
    {
        return $"Модуль '{DeviceName}' аналізує ймовірність інцидентів. Енергоспоживання: {PowerConsumption} кВт.";
    }
}
