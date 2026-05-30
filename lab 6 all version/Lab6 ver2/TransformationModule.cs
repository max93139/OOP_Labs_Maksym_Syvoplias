using System;

namespace Lab6;

/// <summary>
/// Перетворює форму автомобіля для спеціальних середовищ за допомогою рядкових режимів.
/// </summary>
public sealed class TransformationModule
{
    private string _mode;
    private string _status;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public TransformationModule()
    {
        _mode = "Ground";
        _status = "Stopped";
    }

    /// <summary>
    /// Ініціалізує новий модуль трансформації.
    /// </summary>
    public TransformationModule(string mode)
    {
        _mode = mode;
        _status = "Stopped";
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public TransformationModule(TransformationModule other)
    {
        _mode = other.Mode;
        _status = other.Status;
    }

    /// <summary>
    /// Повертає вибраний режим трансформації ("Ground", "Water", "Air").
    /// </summary>
    public string Mode
    {
        get
        {
            return _mode;
        }
        set
        {
            _mode = value;
        }
    }

    /// <summary>
    /// Повертає стан модуля ("Stopped", "Active" тощо).
    /// </summary>
    public string Status
    {
        get
        {
            return _status;
        }
        set
        {
            _status = value;
        }
    }

    /// <summary>
    /// Активує вибраний режим на основі його назви.
    /// </summary>
    public string ActivateMode(string mode)
    {
        _mode = mode;
        _status = "Active";

        string modeString;
        switch (mode.ToLowerInvariant())
        {
            case "ground":
            {
                modeString = "Наземний";
                break;
            }
            case "water":
            {
                modeString = "Водний";
                break;
            }
            case "air":
            {
                modeString = "Повітряний";
                break;
            }
            default:
            {
                modeString = mode;
                break;
            }
        }

        return $"Модуль трансформації активував {modeString} режим.";
    }
}
