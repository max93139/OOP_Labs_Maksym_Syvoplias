namespace Lab6.Domain;

/// <summary>
/// Оновлює поведінку керування на основі досвіду виконаного сценарію.
/// </summary>
public sealed class SelfLearningModule
{
    /// <summary>
    /// Обчислює нову кількість епізодів навчання.
    /// </summary>
    public int UpdateModel(int currentEpisodeCount, int newEpisodeCount)
    {
        return currentEpisodeCount + newEpisodeCount;
    }

    /// <summary>
    /// Формує повідомлення про оновлення моделі.
    /// </summary>
    public string BuildUpdateMessage(int episodeCount)
    {
        return $"Self-learning model saved {episodeCount} driving episodes.";
    }
}
