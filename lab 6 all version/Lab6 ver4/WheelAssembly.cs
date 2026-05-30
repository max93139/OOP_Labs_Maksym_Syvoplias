namespace Lab6;

/// <summary>
/// Представляє контактну підсистему руху шасі з адаптацією тиску та коефіцієнта зчеплення.
/// </summary>
public sealed class WheelAssembly
{
    private double surfaceFrictionCoefficient;

    /// <summary>
    /// Ініціалізує нову ходову частину.
    /// </summary>
    public WheelAssembly(string driveType)
    {
        DriveType = driveType;
        State = "Active";
        surfaceFrictionCoefficient = 0.85;
    }

    /// <summary>
    /// Повертає тип приводу.
    /// </summary>
    public string DriveType { get; }

    /// <summary>
    /// Повертає поточний стан ходової частини ("Active", "Stopped" тощо).
    /// </summary>
    public string State { get; private set; }

    /// <summary>
    /// Адаптує тиск у шинах та розраховує максимальну безпечну швидкість повороту за типом покриття.
    /// </summary>
    public string AdaptToSurface(string surfaceName)
    {
        State = "Active";
        string normalizedSurface = surfaceName.ToLowerInvariant();

        if (normalizedSurface.Contains("ice", StringComparison.Ordinal) || normalizedSurface.Contains("лід", StringComparison.Ordinal) || normalizedSurface.Contains("ожелед", StringComparison.Ordinal))
        {
            surfaceFrictionCoefficient = 0.15;
        }
        else
        {
            if (normalizedSurface.Contains("snow", StringComparison.Ordinal) || normalizedSurface.Contains("сніг", StringComparison.Ordinal))
            {
                surfaceFrictionCoefficient = 0.30;
            }
            else
            {
                if (normalizedSurface.Contains("gravel", StringComparison.Ordinal) || normalizedSurface.Contains("грав", StringComparison.Ordinal))
                {
                    surfaceFrictionCoefficient = 0.65;
                }
                else
                {
                    if (normalizedSurface.Contains("wet", StringComparison.Ordinal) || normalizedSurface.Contains("мокр", StringComparison.Ordinal) || normalizedSurface.Contains("волог", StringComparison.Ordinal))
                    {
                        surfaceFrictionCoefficient = 0.55;
                    }
                    else
                    {
                        surfaceFrictionCoefficient = 0.85;
                    }
                }
            }
        }

        double gravityConstant = 9.81;
        double corneringRadiusMeter = 40.0;
        double maximumSafeSpeedKph = Math.Sqrt(surfaceFrictionCoefficient * gravityConstant * corneringRadiusMeter) * 3.6;

        double targetTirePressureBar;
        if (surfaceFrictionCoefficient < 0.40)
        {
            targetTirePressureBar = 1.9;
        }
        else
        {
            if (surfaceFrictionCoefficient < 0.60)
            {
                targetTirePressureBar = 2.1;
            }
            else
            {
                targetTirePressureBar = 2.4;
            }
        }

        return $"Привід {DriveType} адаптовано до покриття \"{surfaceName}\" (зчеплення: {surfaceFrictionCoefficient:F2}). Цільовий тиск у шинах: {targetTirePressureBar:F1} бар. Безпечна швидкість у повороті: {maximumSafeSpeedKph:F1} км/год.";
    }
}
