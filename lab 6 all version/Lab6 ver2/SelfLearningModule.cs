namespace Lab6;

/// <summary>
/// Оновлює поведінку керування на основі досвіду виконаного сценарію та розраховує похибку нейромережі.
/// </summary>
public sealed class SelfLearningModule : SmartDevice
{
    /// <summary>
    /// Ініціалізує новий модуль самонавчання ШІ.
    /// </summary>
    public SelfLearningModule() : base("Модуль самонавчання ШІ", 0.50)
    {
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
        else
        {
            if (policyDecisionAccuracy > 0.85)
            {
                operationalEfficiency = "Автономна ефективність: ВИСОКА. Стандартні буфери безпеки активні.";
            }
            else
            {
                operationalEfficiency = "Автономна ефективність: СТАНДАРТНА. Застосовано консервативні параметри.";
            }
        }

        return $"Самонавчальна модель зберегла {episodeCount} епізодів водіння. Помилка стратегії: {policyTrainingLoss:F5}, точність рішень: {policyDecisionAccuracy:P2}. {operationalEfficiency}";
    }

    /// <summary>
    /// Повертає статус модуля самонавчання.
    /// </summary>
    public override string GetStatus()
    {
        return $"Модуль '{DeviceName}' оптимізує нейромережу. Енергоспоживання: {PowerConsumption} кВт.";
    }
}
