namespace Lab6;

/// <summary>
/// Надає фіксовані демонстраційні вимірювання для стабільного лабораторного виведення.
/// </summary>
public sealed class FixedSensor : ISensor
{
    private string _name;
    private double _sensorValue;
    private string _sensorUnit;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public FixedSensor()
    {
        _name = "Fixed Sensor";
        _sensorValue = 0.0;
        _sensorUnit = string.Empty;
    }

    /// <summary>
    /// Конструктор з усіма параметрами.
    /// </summary>
    public FixedSensor(string name, double value, string unit)
    {
        _name = name;
        _sensorValue = value;
        _sensorUnit = unit;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public FixedSensor(FixedSensor other)
    {
        _name = other.Name;
        _sensorValue = other.SensorValue;
        _sensorUnit = other.SensorUnit;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public double SensorValue
    {
        get => _sensorValue;
        set => _sensorValue = value;
    }

    public string SensorUnit
    {
        get => _sensorUnit;
        set => _sensorUnit = value;
    }

    /// <summary>
    /// Зчитує налаштоване фіксоване значення без звернення до апаратури.
    /// </summary>
    public SensorReading Read()
    {
        return new SensorReading(_name, _sensorValue, _sensorUnit);
    }
}
