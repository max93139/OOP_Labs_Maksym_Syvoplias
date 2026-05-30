namespace Lab6;

/// <summary>
/// Зберігає показання сенсора (Canonical Class Template).
/// </summary>
public sealed class SensorReading
{
    private string _name;
    private double _value;
    private string _unit;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public SensorReading()
    {
        _name = "Unknown";
        _value = 0.0;
        _unit = string.Empty;
    }

    /// <summary>
    /// Конструктор з усіма параметрами.
    /// </summary>
    public SensorReading(string name, double value, string unit)
    {
        _name = name;
        _value = value;
        _unit = unit;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public SensorReading(SensorReading other)
    {
        _name = other.Name;
        _value = other.Value;
        _unit = other.Unit;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public double Value
    {
        get => _value;
        set => _value = value;
    }

    public string Unit
    {
        get => _unit;
        set => _unit = value;
    }

    /// <summary>
    /// Перетворює показання на рядок протоколу.
    /// </summary>
    public string ToProtocolLine()
    {
        return $"{_name}: {_value:F1} {_unit}";
    }
}
