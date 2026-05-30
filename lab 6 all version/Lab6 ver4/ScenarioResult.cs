using System;

namespace Lab6;

/// <summary>
/// Зберігає іменований результат розрахунку сценарію.
/// </summary>
public sealed class ScenarioResult
{
    private string _name;
    private double _value;
    private string _message;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public ScenarioResult()
    {
        _name = "Default Scenario Result";
        _value = 0.0;
        _message = "No result logged.";
    }

    /// <summary>
    /// Ініціалізує новий результат сценарію.
    /// </summary>
    public ScenarioResult(string name, double value, string message)
    {
        _name = name;
        _value = value;
        _message = message;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public ScenarioResult(ScenarioResult other)
    {
        _name = other.Name;
        _value = other.Value;
        _message = other.Message;
    }

    /// <summary>
    /// Повертає назву результату.
    /// </summary>
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    /// <summary>
    /// Повертає розраховане значення.
    /// </summary>
    public double Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }

    /// <summary>
    /// Повертає зрозуміле для людини повідомлення результату.
    /// </summary>
    public string Message
    {
        get
        {
            return _message;
        }
        set
        {
            _message = value;
        }
    }

    /// <summary>
    /// Перетворює результат на рядок протоколу.
    /// </summary>
    public string ToProtocolLine()
    {
        return $"{_name}: {_value:F1}. {_message}";
    }
}
