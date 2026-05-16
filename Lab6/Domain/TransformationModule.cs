namespace Lab6.Domain;

/// <summary>
/// Перетворює форму автомобіля для спеціальних середовищ.
/// </summary>
public sealed class TransformationModule
{
    /// <summary>
    /// Ініціалізує новий модуль трансформації.
    /// </summary>
    public TransformationModule(TransformationMode mode)
    {
        Mode = mode;
        Status = ComponentState.Stopped;
    }

    /// <summary>
    /// Повертає вибраний режим трансформації.
    /// </summary>
    public TransformationMode Mode { get; private set; }

    /// <summary>
    /// Повертає стан модуля.
    /// </summary>
    public ComponentState Status { get; private set; }

    /// <summary>
    /// Активує вибраний режим.
    /// </summary>
    public string ActivateMode(TransformationMode mode)
    {
        Mode = mode;
        Status = ComponentState.Active;
        return $"Transformation module activated {Mode} mode.";
    }
}
