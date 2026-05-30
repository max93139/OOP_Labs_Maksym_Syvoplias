using System;

namespace Lab6;

/// <summary>
/// Оновлює поведінку керування на основі досвіду виконаного сценарію та розраховує похибку нейромережі.
/// </summary>
public sealed class SelfLearningModule
{
    private double _learningRate;
    private string _policyName;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public SelfLearningModule()
    {
        _learningRate = 0.01;
        _policyName = "Proximal Policy Optimization";
    }

    /// <summary>
    /// Конструктор з повним набором конфігурацій.
    /// </summary>
    public SelfLearningModule(double learningRate, string policyName)
    {
        _learningRate = learningRate;
        _policyName = policyName;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public SelfLearningModule(SelfLearningModule other)
    {
        _learningRate = other.LearningRate;
        _policyName = other.PolicyName;
    }

    /// <summary>
    /// Повертає швидкість навчання.
    /// </summary>
    public double LearningRate
    {
        get
        {
            return _learningRate;
        }
        set
        {
            _learningRate = value;
        }
    }

    /// <summary>
    /// Повертає назву стратегії навчання.
    /// </summary>
    public string PolicyName
    {
        get
        {
            return _policyName;
        }
        set
        {
            _policyName = value;
        }
    }

    /// <summary>
    /// Обчислює нову кількість епізодів навчання.
    /// </summary>
    public int UpdateModel(int currentEpisodeCount, int newEpisodeCount)
    {
        return currentEpisodeCount + newEpisodeCount;
    }

    /// <summary>
    /// Формує повідомлення про оновлення моделі з розрахунком точності та похибки навчання нейромережі.
    /// </summary>
    public string BuildUpdateMessage(int episodeCount)
    {
        double policyTrainingLoss = 0.25 * Math.Exp(-episodeCount / 50.0);
        double policyDecisionAccuracy = 1.0 - policyTrainingLoss;

        string operationalEfficiency;
        if (policyDecisionAccuracy > 0.95)
        {
            operationalEfficiency = "Автономна ефективність: ВИНЯТКОВА. Мінімальне втручання систем безпеки.";
        }
        else if (policyDecisionAccuracy > 0.85)
        {
            operationalEfficiency = "Автономна ефективність: ВИСОКА. Стандартні буфери безпеки активні.";
        }
        else
        {
            operationalEfficiency = "Автономна ефективність: СТАНДАРТНА. Застосовано консервативні параметри.";
        }

        return $"Самонавчальна модель зберегла {episodeCount} епізодів водіння. Помилка стратегії: {policyTrainingLoss:F5}, точність рішень: {policyDecisionAccuracy:P2}. {operationalEfficiency}";
    }
}
