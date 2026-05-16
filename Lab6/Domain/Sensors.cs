namespace Lab6.Domain;

/// <summary>
/// Описує сенсор, з якого можна зчитати дані.
/// </summary>
public interface ISensor
{
    /// <summary>
    /// Повертає назву сенсора.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Зчитує поточне значення сенсора.
    /// </summary>
    SensorReading Read();
}

/// <summary>
/// Зберігає нормалізоване вимірювання сенсора.
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

/// <summary>
/// Надає фіксовані демонстраційні вимірювання для стабільного лабораторного виведення.
/// </summary>
public sealed class FixedSensor : ISensor
{
    private readonly double value;
    private readonly string unit;

    /// <summary>
    /// Ініціалізує новий фіксований сенсор.
    /// </summary>
    public FixedSensor(string name, double value, string unit)
    {
        Name = name;
        this.value = value;
        this.unit = unit;
    }

    /// <summary>
    /// Повертає назву сенсора.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Зчитує налаштоване значення.
    /// </summary>
    public SensorReading Read()
    {
        return new SensorReading(Name, value, unit);
    }
}
