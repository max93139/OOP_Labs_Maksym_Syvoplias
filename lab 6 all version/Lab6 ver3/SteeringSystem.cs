namespace Lab6;

/// <summary>
/// Представляє керування напрямком руху з розрахунком відцентрового прискорення.
/// </summary>
public sealed class SteeringSystem
{
    private double lateralAccelerationG;

    /// <summary>
    /// Ініціалізує нову систему кермування з фізичною чутливістю.
    /// </summary>
    public SteeringSystem(string steeringType, double sensitivity)
    {
        SteeringType = steeringType;
        Sensitivity = sensitivity;
        DirectionDegrees = 0;
        lateralAccelerationG = 0.0;
    }

    /// <summary>
    /// Повертає тип кермування.
    /// </summary>
    public string SteeringType { get; }

    /// <summary>
    /// Повертає чутливість кермування.
    /// </summary>
    public double Sensitivity { get; }

    /// <summary>
    /// Повертає поточний напрямок.
    /// </summary>
    public int DirectionDegrees { get; private set; }

    /// <summary>
    /// Змінює напрямок руху та оцінює бічні перевантаження для стабілізації траєкторії.
    /// </summary>
    public string ChangeDirection(int directionDegrees)
    {
        DirectionDegrees = directionDegrees;

        if (directionDegrees != 0)
        {
            double speedMeterPerSecond = 16.67;
            double radiusMeter = 50.0 / Math.Max(0.01, Math.Abs(Math.Sin(directionDegrees * Math.PI / 180.0)));
            double gravityConstant = 9.81;
            lateralAccelerationG = ((speedMeterPerSecond * speedMeterPerSecond) / (radiusMeter * gravityConstant)) * Sensitivity;
        }
        else
        {
            lateralAccelerationG = 0.0;
        }

        string stabilityWarning;
        if (lateralAccelerationG > 0.8)
        {
            stabilityWarning = "УВАГА: Бічна сила G перевищує безпечну межу! Активовано втручання ESP.";
        }
        else
        {
            stabilityWarning = "Автомобіль стабільний. Нормальне бічне прискорення.";
        }

        return $"Напрямок змінено на {DirectionDegrees} градусів. Бічне прискорення: {lateralAccelerationG:F2}g. {stabilityWarning}";
    }

    /// <summary>
    /// Вмикає керування напрямком через автопілот із калібруванням та поправкою траєкторії.
    /// </summary>
    public string ActivateAutopilot()
    {
        double laneOffsetCentimeters = 1.25;
        double pathPlannedAngleDegrees = 0.45;
        return $"Кермування автопілотом активовано. Планований кут траєкторії: {pathPlannedAngleDegrees:F2}° (відхилення: {laneOffsetCentimeters:F1} см).";
    }
}
