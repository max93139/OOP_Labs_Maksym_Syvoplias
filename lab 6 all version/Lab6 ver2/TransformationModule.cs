namespace Lab6;

/// <summary>
/// Перетворює форму автомобіля для спеціальних середовищ за допомогою рядкових режимів.
/// </summary>
public sealed class TransformationModule
{
    /// <summary>
    /// Ініціалізує новий модуль трансформації.
    /// </summary>
    public TransformationModule(string mode)
    {
        Mode = mode;
        Status = "Stopped";
    }

    /// <summary>
    /// Повертає вибраний режим трансформації ("Ground", "Water", "Air").
    /// </summary>
    public string Mode { get; private set; }

    /// <summary>
    /// Повертає стан модуля ("Stopped", "Active" тощо).
    /// </summary>
    public string Status { get; private set; }

    /// <summary>
    /// Активує вибраний режим на основі його назви.
    /// </summary>
    public string ActivateMode(string mode)
    {
        Mode = mode;
        Status = "Active";

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
