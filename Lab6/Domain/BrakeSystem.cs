namespace Lab6.Domain;

/// <summary>
/// Представляє гальмівну підсистему.
/// </summary>
public sealed class BrakeSystem
{
    /// <summary>
    /// Ініціалізує нову гальмівну систему.
    /// </summary>
    public BrakeSystem(string brakeType, double efficiencyPercent)
    {
        BrakeType = brakeType;
        EfficiencyPercent = efficiencyPercent;
    }

    /// <summary>
    /// Повертає тип гальм.
    /// </summary>
    public string BrakeType { get; }

    /// <summary>
    /// Повертає ефективність гальм.
    /// </summary>
    public double EfficiencyPercent { get; }

    /// <summary>
    /// Активує звичайне гальмування.
    /// </summary>
    public string ActivateBraking()
    {
        return $"{BrakeType} braking activated with {EfficiencyPercent:F1}% efficiency.";
    }

    /// <summary>
    /// Активує екстрене гальмування, коли час реакції є критичним.
    /// </summary>
    public string ActivateEmergencyBraking()
    {
        return "Emergency braking activated.";
    }
}
