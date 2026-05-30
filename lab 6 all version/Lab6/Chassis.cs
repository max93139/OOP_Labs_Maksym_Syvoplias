namespace Lab6;

/// <summary>
/// Представляє несуче шасі, яке компонує підсистеми руху та прораховує аеродинамічний опір.
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
        CurrentClearanceCentimeters = 15.0;
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
    /// Повертає поточний кліренс шасі в сантиметрах.
    /// </summary>
    public double CurrentClearanceCentimeters { get; private set; }

    /// <summary>
    /// Стабілізує рух через узгодження роботи частин шасі з урахуванням швидкості автомобіля.
    /// </summary>
    public IReadOnlyList<string> StabilizeMovement(string surfaceName, double speedKmh)
    {
        return new List<string>
        {
            wheelAssembly.AdaptToSurface(surfaceName),
            steeringSystem.ChangeDirection(0),
            transmission.ShiftGearBasedOnSpeed(speedKmh)
        };
    }

    /// <summary>
    /// Змінює кліренс для паркування або умов руху, перераховуючи коефіцієнт аеродинамічного опору.
    /// </summary>
    public string ChangeClearance(double clearanceCentimeters)
    {
        CurrentClearanceCentimeters = Math.Clamp(clearanceCentimeters, 10.0, 25.0);
        double aerodynamicDragCoefficient = 0.22 + 0.003 * (CurrentClearanceCentimeters - 12.0);

        string suspensionMode;
        if (CurrentClearanceCentimeters < 14.0)
        {
            suspensionMode = "Спорт (жорстке демпфування, оптимізована аеродинаміка)";
        }
        else
        {
            if (CurrentClearanceCentimeters > 20.0)
            {
                suspensionMode = "Позашляховий (м'яке демпфування, максимальний кліренс)";
            }
            else
            {
                suspensionMode = "Комфорт (збалансоване демпфування)";
            }
        }

        return $"Кліренс шасі змінено на {CurrentClearanceCentimeters:F1} см. Розрахунковий коефіцієнт опору Cd: {aerodynamicDragCoefficient:F3}. Активний режим: {suspensionMode}.";
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
