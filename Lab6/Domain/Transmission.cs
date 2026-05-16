namespace Lab6.Domain;

/// <summary>
/// Представляє підсистему передавання крутного моменту.
/// </summary>
public sealed class Transmission
{
    /// <summary>
    /// Ініціалізує нову трансмісію.
    /// </summary>
    public Transmission(string transmissionType, int gearCount)
    {
        TransmissionType = transmissionType;
        GearCount = gearCount;
        CurrentGear = 1;
    }

    /// <summary>
    /// Повертає тип трансмісії.
    /// </summary>
    public string TransmissionType { get; }

    /// <summary>
    /// Повертає кількість доступних передач.
    /// </summary>
    public int GearCount { get; }

    /// <summary>
    /// Повертає поточну передачу.
    /// </summary>
    public int CurrentGear { get; private set; }

    /// <summary>
    /// Перемикає трансмісію на передачу в допустимих межах.
    /// </summary>
    public string ShiftGear(int requestedGear)
    {
        CurrentGear = Math.Clamp(requestedGear, 1, GearCount);
        return $"Transmission shifted to gear {CurrentGear}.";
    }
}
