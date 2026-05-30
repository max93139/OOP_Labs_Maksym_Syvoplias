namespace Lab6;

/// <summary>
/// Виняток, що виникає при перевантаженні системи управління надмірною кількістю одночасних запитів.
/// </summary>
public sealed class TooManyCommandsException : SmartCarException
{
    /// <summary>
    /// Ініціалізує новий виняток надмірної кількості команд.
    /// </summary>
    public TooManyCommandsException(string message) : base(message)
    {
    }
}
