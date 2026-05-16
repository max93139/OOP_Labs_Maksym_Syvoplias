namespace Lab6.Domain;

/// <summary>
/// Представляє керування напрямком руху.
/// </summary>
public sealed class SteeringSystem
{
    /// <summary>
    /// Ініціалізує нову систему кермування.
    /// </summary>
    public SteeringSystem(string steeringType, double sensitivity)
    {
        SteeringType = steeringType;
        Sensitivity = sensitivity;
        DirectionDegrees = 0;
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
    /// Змінює напрямок руху.
    /// </summary>
    public string ChangeDirection(int directionDegrees)
    {
        DirectionDegrees = directionDegrees;
        return $"Direction changed to {DirectionDegrees} degrees.";
    }

    /// <summary>
    /// Вмикає керування напрямком через автопілот.
    /// </summary>
    public string ActivateAutopilot()
    {
        return "Autopilot controls steering direction.";
    }
}
