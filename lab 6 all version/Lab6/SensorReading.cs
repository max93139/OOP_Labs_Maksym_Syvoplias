namespace Lab6;

/// <summary>
/// Зберігає нормалізоване вимірювання одного сенсора як незмінний запис.
/// </summary>
public sealed class SensorReading
{
    /// <summary>
    /// Ініціалізує нове показання сенсора.
    /// </summary>
    public SensorReading(string name, double value, string unit)
    {
        Name = name;
        Value = value;
        Unit = unit;
    }

    /// <summary>
    /// Повертає назву показання.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Повертає числове значення.
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Повертає одиницю вимірювання.
    /// </summary>
    public string Unit { get; }

    /// <summary>
    /// Перетворює показання на рядок протоколу.
    /// </summary>
    public string ToProtocolLine()
    {
        return $"{Name}: {Value:F1} {Unit}";
    }
}
