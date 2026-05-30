using System;
using System.Collections.Generic;

namespace Lab6;

/// <summary>
/// Представляє несуче шасі, яке компонує підсистеми руху та прораховує аеродинамічний опір (True Composition).
/// </summary>
public sealed class Chassis
{
    private string _suspensionType;
    private double _massKilograms;
    private double _currentClearanceCentimeters;

    private readonly WheelAssembly wheelAssembly;
    private readonly BrakeSystem brakeSystem;
    private readonly SteeringSystem steeringSystem;
    private readonly Transmission transmission;

    /// <summary>
    /// Конструктор за замовчуванням (True Composition).
    /// </summary>
    public Chassis()
    {
        _suspensionType = "active air suspension";
        _massKilograms = 1830.0;
        _currentClearanceCentimeters = 15.0;

        wheelAssembly = new WheelAssembly("adaptive all-wheel");
        brakeSystem = new BrakeSystem("electromagnetic", 96.5);
        steeringSystem = new SteeringSystem("electronic", 0.92);
        transmission = new Transmission("електрична двоступенева", 2);
    }

    /// <summary>
    /// Конструктор з конфігураційними параметрами для скомпонованих підсистем (True Composition).
    /// </summary>
    public Chassis(
        string suspensionType,
        double massKilograms,
        string driveType,
        string brakeType,
        double brakeEfficiency,
        string steeringType,
        double steeringSensitivity,
        string transmissionType,
        int gearCount)
    {
        _suspensionType = suspensionType;
        _massKilograms = massKilograms;
        _currentClearanceCentimeters = 15.0;

        wheelAssembly = new WheelAssembly(driveType);
        brakeSystem = new BrakeSystem(brakeType, brakeEfficiency);
        steeringSystem = new SteeringSystem(steeringType, steeringSensitivity);
        transmission = new Transmission(transmissionType, gearCount);
    }

    /// <summary>
    /// Конструктор копіювання (Глибоке копіювання скомпонованих частин).
    /// </summary>
    public Chassis(Chassis other)
    {
        _suspensionType = other.SuspensionType;
        _massKilograms = other.MassKilograms;
        _currentClearanceCentimeters = other.CurrentClearanceCentimeters;

        wheelAssembly = new WheelAssembly(other.WheelComponent);
        brakeSystem = new BrakeSystem(other.BrakeComponent);
        steeringSystem = new SteeringSystem(other.SteeringComponent);
        transmission = new Transmission(other.TransmissionComponent);
    }

    public string SuspensionType
    {
        get => _suspensionType;
        set => _suspensionType = value;
    }

    public double MassKilograms
    {
        get => _massKilograms;
        set => _massKilograms = value;
    }

    public double CurrentClearanceCentimeters
    {
        get => _currentClearanceCentimeters;
        set => _currentClearanceCentimeters = value;
    }

    public WheelAssembly WheelComponent => wheelAssembly;
    public BrakeSystem BrakeComponent => brakeSystem;
    public SteeringSystem SteeringComponent => steeringSystem;
    public Transmission TransmissionComponent => transmission;

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
        _currentClearanceCentimeters = Math.Clamp(clearanceCentimeters, 10.0, 25.0);
        double aerodynamicDragCoefficient = 0.22 + 0.003 * (_currentClearanceCentimeters - 12.0);

        string suspensionMode;
        if (_currentClearanceCentimeters < 14.0)
        {
            suspensionMode = "Спорт (жорстке демпфування, оптимізована аеродинаміка)";
        }
        else if (_currentClearanceCentimeters > 20.0)
        {
            suspensionMode = "Позашляховий (м'яке демпфування, максимальний кліренс)";
        }
        else
        {
            suspensionMode = "Комфорт (збалансоване демпфування)";
        }

        return $"Кліренс шасі змінено на {_currentClearanceCentimeters:F1} см. Розрахунковий коефіцієнт опору Cd: {aerodynamicDragCoefficient:F3}. Активний режим: {suspensionMode}.";
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

    /// <summary>
    /// Перемикає трансмісію на передачу в допустимих межах за стандартною швидкістю.
    /// </summary>
    public string ShiftGear(int requestedGear)
    {
        return transmission.ShiftGear(requestedGear);
    }
}
