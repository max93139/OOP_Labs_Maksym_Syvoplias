namespace Lab6.Domain;

/// <summary>
/// Представляє контактну підсистему руху шасі.
/// </summary>
public sealed class WheelAssembly
{
    /// <summary>
    /// Ініціалізує нову ходову частину.
    /// </summary>
    public WheelAssembly(string driveType)
    {
        DriveType = driveType;
        State = ComponentState.Active;
    }

    /// <summary>
    /// Повертає тип приводу.
    /// </summary>
    public string DriveType { get; }

    /// <summary>
    /// Повертає поточний стан ходової частини.
    /// </summary>
    public ComponentState State { get; private set; }

    /// <summary>
    /// Адаптує поведінку ходової частини до якості покриття.
    /// </summary>
    public string AdaptToSurface(string surfaceName)
    {
        State = ComponentState.Active;
        return $"{DriveType} drive adapted to {surfaceName}.";
    }
}
