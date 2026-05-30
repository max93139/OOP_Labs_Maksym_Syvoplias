using System;

namespace Lab6;

/// <summary>
/// Прогнозує критичні ситуації на основі об'єднаних значень ризику.
/// </summary>
public sealed class PredictionModule
{
    private double _accuracyWeight;
    private string _modelName;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public PredictionModule()
    {
        _accuracyWeight = 0.95;
        _modelName = "Bayesian Predictive Model";
    }

    /// <summary>
    /// Конструктор з повним набором конфігурацій.
    /// </summary>
    public PredictionModule(double accuracyWeight, string modelName)
    {
        _accuracyWeight = accuracyWeight;
        _modelName = modelName;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public PredictionModule(PredictionModule other)
    {
        _accuracyWeight = other.AccuracyWeight;
        _modelName = other.ModelName;
    }

    /// <summary>
    /// Повертає або встановлює вагу точності.
    /// </summary>
    public double AccuracyWeight
    {
        get
        {
            return _accuracyWeight;
        }
        set
        {
            _accuracyWeight = value;
        }
    }

    /// <summary>
    /// Повертає або встановлює назву моделі.
    /// </summary>
    public string ModelName
    {
        get
        {
            return _modelName;
        }
        set
        {
            _modelName = value;
        }
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
}
