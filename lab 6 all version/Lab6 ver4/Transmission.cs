namespace Lab6;

/// <summary>
/// Представляє підсистему передавання крутного моменту з розрахунком RPM.
/// </summary>
public sealed class Transmission
{
    private double engineRpm;

    /// <summary>
    /// Ініціалізує нову трансмісію.
    /// </summary>
    public Transmission(string transmissionType, int gearCount)
    {
        TransmissionType = transmissionType;
        GearCount = gearCount;
        CurrentGear = 1;
        engineRpm = 1000.0;
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
    /// Автоматично перемикає передачу на основі швидкості електрокара (2 передачі, 2-га вмикається після 80 км/год).
    /// </summary>
    public string ShiftGearBasedOnSpeed(double speedKmh)
    {
        int targetGear;
        if (speedKmh > 80.0)
        {
            targetGear = 2;
        }
        else
        {
            targetGear = 1;
        }

        CurrentGear = Math.Clamp(targetGear, 1, GearCount);

        double vehicleSpeedMeterPerSecond = speedKmh / 3.6; 
        double gearRatio = 4.5 / CurrentGear;
        double finalDriveRatio = 3.2;
        double wheelRadiusMeter = 0.33;

        engineRpm = (vehicleSpeedMeterPerSecond / (2.0 * Math.PI * wheelRadiusMeter)) * 60.0 * gearRatio * finalDriveRatio;
        double torqueMultiplier = gearRatio * finalDriveRatio * 0.95;

        string engineStatus;
        if (engineRpm > 5000.0)
        {
            engineStatus = "Попередження про надмірні оберти! Високе навантаження на двигун.";
        }
        else
        {
            if (engineRpm < 1200.0)
            {
                engineStatus = "Попередження про низькі оберти! Рекомендовано знизити передачу.";
            }
            else
            {
                engineStatus = "Оптимальний робочий діапазон.";
            }
        }

        return $"Трансмісія (електрокар) перемкнута на передачу {CurrentGear} (швидкість: {speedKmh:F1} км/год). Мультиплікатор моменту: {torqueMultiplier:F1}x, очікувані оберти RPM: {engineRpm:F0}. {engineStatus}";
    }

    /// <summary>
    /// Перемикає трансмісію на передачу в допустимих межах за стандартною швидкістю.
    /// </summary>
    public string ShiftGear(int requestedGear)
    {
        return ShiftGearBasedOnSpeed(requestedGear == 2 ? 90.0 : 50.0);
    }
}
