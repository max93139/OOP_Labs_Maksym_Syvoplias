using System;

namespace Lab6;

/// <summary>
/// Представляє підсистему передавання крутного моменту з розрахунком RPM.
/// </summary>
public sealed class Transmission
{
    private const double WHEEL_RADIUS_METERS = 0.33;
    private const double CONVERSION_FACTOR_KPH_TO_MPS = 3.6;
    private const double MINUTES_IN_SECONDS = 60.0;
    private const double TRANSMISSION_EFFICIENCY = 0.95;
    private const double MAX_RPM_LIMIT = 5000.0;
    private const double MIN_RPM_LIMIT = 1200.0;

    private string _transmissionType;
    private int _gearCount;
    private int _currentGear;
    private double _engineRpm;

    /// <summary>
    /// Конструктор за замовчуванням.
    /// </summary>
    public Transmission()
    {
        _transmissionType = "електрична двоступенева";
        _gearCount = 2;
        _currentGear = 1;
        _engineRpm = 1000.0;
    }

    /// <summary>
    /// Конструктор з повними параметрами.
    /// </summary>
    public Transmission(string transmissionType, int gearCount)
    {
        _transmissionType = transmissionType;
        _gearCount = gearCount;
        _currentGear = 1;
        _engineRpm = 1000.0;
    }

    /// <summary>
    /// Конструктор копіювання.
    /// </summary>
    public Transmission(Transmission other)
    {
        _transmissionType = other.TransmissionType;
        _gearCount = other.GearCount;
        _currentGear = other.CurrentGear;
        _engineRpm = other.EngineRpm;
    }

    public string TransmissionType
    {
        get => _transmissionType;
        set => _transmissionType = value;
    }

    public int GearCount
    {
        get => _gearCount;
        set => _gearCount = value;
    }

    public int CurrentGear
    {
        get => _currentGear;
        set => _currentGear = value;
    }

    public double EngineRpm
    {
        get => _engineRpm;
        set => _engineRpm = value;
    }

    /// <summary>
    /// Автоматично перемикає передачу на основі швидкості електрокара (2 передачі, 2-га вмикається після 80 км/год).
    /// </summary>
    public string ShiftGearBasedOnSpeed(double speedKmh)
    {
        int targetGear = speedKmh > 80.0 ? 2 : 1;
        _currentGear = Math.Clamp(targetGear, 1, _gearCount);

        double vehicleSpeedMeterPerSecond = speedKmh / CONVERSION_FACTOR_KPH_TO_MPS; 
        
        // Використовуємо реалістичні передавальні числа для електромобіля!
        double gearRatio = 2.2 / _currentGear;
        double finalDriveRatio = 3.6;

        _engineRpm = (vehicleSpeedMeterPerSecond / (2.0 * Math.PI * WHEEL_RADIUS_METERS)) * MINUTES_IN_SECONDS * gearRatio * finalDriveRatio;
        double torqueMultiplier = gearRatio * finalDriveRatio * TRANSMISSION_EFFICIENCY;

        string engineStatus;
        if (_engineRpm > MAX_RPM_LIMIT)
        {
            engineStatus = "Попередження про надмірні оберти! Високе навантаження на двигун.";
        }
        else if (_engineRpm < MIN_RPM_LIMIT)
        {
            engineStatus = "Попередження про низькі оберти! Рекомендовано знизити передачу.";
        }
        else
        {
            engineStatus = "Оптимальний робочий діапазон.";
        }

        return $"Трансмісія ({_transmissionType}) перемкнута на передачу {_currentGear} (швидкість: {speedKmh:F1} км/год). Мультиплікатор моменту: {torqueMultiplier:F1}x, очікувані оберти RPM: {_engineRpm:F0}. {engineStatus}";
    }

    /// <summary>
    /// Перемикає трансмісію на передачу в допустимих межах за стандартною швидкістю (вирішено проблему мертвого методу).
    /// </summary>
    public string ShiftGear(int requestedGear)
    {
        return ShiftGearBasedOnSpeed(requestedGear == 2 ? 90.0 : 50.0);
    }
}
