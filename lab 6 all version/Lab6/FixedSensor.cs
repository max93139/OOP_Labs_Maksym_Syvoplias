namespace Lab6;

/// <summary>
/// Надає фіксовані демонстраційні вимірювання для стабільного лабораторного виведення.
/// Реалізує ISensor без реального апаратного забезпечення, щоб не залежати від обладнання під час тестування.
/// </summary>
public sealed class FixedSensor : ISensor
{
    private readonly double sensorValue;
    private readonly string sensorUnit;

    /// <summary>
    /// Ініціалізує новий фіксований сенсор із заданим значенням.
    /// </summary>
    public FixedSensor(string name, double value, string unit)
    {
        Name = name;
        sensorValue = value;
        sensorUnit = unit;
    }

    /// <summary>
    /// Повертає назву сенсора.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Зчитує налаштоване фіксоване значення без звернення до апаратури.
    /// </summary>
    public SensorReading Read()
    {
        return new SensorReading(Name, sensorValue, sensorUnit);
    }
}
