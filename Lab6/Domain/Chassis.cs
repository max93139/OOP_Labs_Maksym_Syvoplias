namespace Lab6.Domain;

/// <summary>
/// Представляє несуче шасі, яке компонує підсистеми руху.
/// </summary>
public sealed class Chassis
{
    private readonly WheelAssembly wheelAssembly;
    private readonly BrakeSystem brakeSystem;
    private readonly SteeringSystem steeringSystem;
    private readonly Transmission transmission;

    /// <summary>
    /// Ініціалізує нове шасі з необхідних частин.
    /// </summary>
    public Chassis(
        string suspensionType,
        double massKilograms,
        WheelAssembly wheelAssembly,
        BrakeSystem brakeSystem,
        SteeringSystem steeringSystem,
        Transmission transmission)
    {
        SuspensionType = suspensionType;
        MassKilograms = massKilograms;
        this.wheelAssembly = wheelAssembly;
        this.brakeSystem = brakeSystem;
        this.steeringSystem = steeringSystem;
        this.transmission = transmission;
    }

    /// <summary>
    /// Повертає тип підвіски.
    /// </summary>
    public string SuspensionType { get; }

    /// <summary>
    /// Повертає масу шасі.
    /// </summary>
    public double MassKilograms { get; }

    /// <summary>
    /// Стабілізує рух через узгодження роботи частин шасі.
    /// </summary>
    public IReadOnlyList<string> StabilizeMovement(string surfaceName)
    {
        return new List<string>
        {
            wheelAssembly.AdaptToSurface(surfaceName),
            steeringSystem.ChangeDirection(0),
            transmission.ShiftGear(2)
        };
    }

    /// <summary>
    /// Змінює кліренс для паркування або посадки пасажирів.
    /// </summary>
    public string ChangeClearance(double clearanceCentimeters)
    {
        return $"Chassis clearance changed to {clearanceCentimeters:F1} cm.";
    }

    /// <summary>
    /// Передає екстрене гальмування гальмівній підсистемі.
    /// </summary>
    public string ActivateEmergencyBraking()
    {
        return brakeSystem.ActivateEmergencyBraking();
    }

    /// <summary>
    /// Передає керування автопілотом системі кермування.
    /// </summary>
    public string ActivateAutopilot()
    {
        return steeringSystem.ActivateAutopilot();
    }
}
