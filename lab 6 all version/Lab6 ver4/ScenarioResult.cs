namespace Lab6;

/// <summary>
/// Зберігає іменований результат розрахунку сценарію.
/// </summary>
public sealed class ScenarioResult
{
    /// <summary>
    /// Ініціалізує новий результат сценарію.
    /// </summary>
    public ScenarioResult(string name, double value, string message)
    {
        Name = name;
        Value = value;
        Message = message;
    }

    /// <summary>
    /// Повертає назву результату.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Повертає розраховане значення.
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Повертає зрозуміле для людини повідомлення результату.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Перетворює результат на рядок протоколу.
    /// </summary>
    public string ToProtocolLine()
    {
        return $"{Name}: {Value:F1}. {Message}";
    }
}
