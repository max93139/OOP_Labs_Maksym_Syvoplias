using System;

namespace Lab6;

/// <summary>
/// Представляє гальмівну підсистему з фізичним розрахунком теплового навантаження.
/// </summary>
public sealed class BrakeSystem
{
    private const double DEFAULT_SPEED_MPS = 27.78;
    private const double EMERGENCY_SPEED_MPS = 36.11;
    private const double VEHICLE_MASS_KG = 1830.0;
    private const double FRONT_AXLE_SHARE = 0.70;
    private const double DISC_MASS_KG = 8.0;
    private const double HEAT_CAPACITY = 500.0;
    private const double TEMP_LIMIT = 350.0;

    private double _brakeTemperatureCelsius;
    private double _wearPercent;
    private string _brakeType;
    private double _efficiencyPercent;

    /// <summary>
    /// Конструктор за замовчуванням (Canonical Class Template).
    /// </summary>
    public BrakeSystem()
    {
        _brakeType = "electromagnetic";
        _efficiencyPercent = 96.5;
        _brakeTemperatureCelsius = 25.0;
        _wearPercent = 12.5;
    }

    /// <summary>
    /// Конструктор з повними параметрами.
    /// </summary>
    public BrakeSystem(string brakeType, double efficiencyPercent)
    {
        _brakeType = brakeType;
        _efficiencyPercent = efficiencyPercent;
        _brakeTemperatureCelsius = 25.0;
        _wearPercent = 12.5;
    }

    /// <summary>
    /// Конструктор копіювання (Canonical Class Template).
    /// </summary>
    public BrakeSystem(BrakeSystem other)
    {
        _brakeType = other.BrakeType;
        _efficiencyPercent = other.EfficiencyPercent;
        _brakeTemperatureCelsius = other.BrakeTemperatureCelsius;
        _wearPercent = other.WearPercent;
    }

    public string BrakeType
    {
        get
        {
            return _brakeType;
        }
        set
        {
            _brakeType = value;
        }
    }

    public double EfficiencyPercent
    {
        get
        {
            return _efficiencyPercent;
        }
        set
        {
            _efficiencyPercent = value;
        }
    }

    public double BrakeTemperatureCelsius
    {
        get
        {
            return _brakeTemperatureCelsius;
        }
        set
        {
            _brakeTemperatureCelsius = value;
        }
    }

    public double WearPercent
    {
        get
        {
            return _wearPercent;
        }
        set
        {
            _wearPercent = value;
        }
    }

    /// <summary>
    ///  Активує звичайне гальмування та розраховує приріст температури гальмівних дисків.
    /// </summary>
    public string ActivateBraking()
    {
        double kineticEnergyJoules = 0.5 * VEHICLE_MASS_KG * DEFAULT_SPEED_MPS * DEFAULT_SPEED_MPS;
        double energyPerDiscJoules = (kineticEnergyJoules * FRONT_AXLE_SHARE) / 2.0;

        double temperatureIncrease = energyPerDiscJoules / (DISC_MASS_KG * HEAT_CAPACITY);
        _brakeTemperatureCelsius += temperatureIncrease;

        double fadingFactor = _brakeTemperatureCelsius > TEMP_LIMIT ? Math.Max(0.4, 1.0 - (_brakeTemperatureCelsius - TEMP_LIMIT) * 0.002) : 1.0;
        double wearIncrease = 0.05 * (_brakeTemperatureCelsius / 100.0);
        _wearPercent = Math.Min(100.0, _wearPercent + wearIncrease);
        double activeEfficiency = _efficiencyPercent * fadingFactor;

        return $"Гальмування {_brakeType} активовано. Теплове навантаження: +{temperatureIncrease:F1}°C (поточна: {_brakeTemperatureCelsius:F1}°C). Ефективність: {activeEfficiency:F1}% (знос: {_wearPercent:F2}%).";
    }

    /// <summary>
    ///  Активує екстрене гальмування з ABS.
    /// </summary>
    public string ActivateEmergencyBraking()
    {
        double kineticEnergyJoules = 0.5 * VEHICLE_MASS_KG * EMERGENCY_SPEED_MPS * EMERGENCY_SPEED_MPS;
        double energyPerDiscJoules = (kineticEnergyJoules * FRONT_AXLE_SHARE) / 2.0;

        double temperatureIncrease = energyPerDiscJoules / (DISC_MASS_KG * HEAT_CAPACITY);
        _brakeTemperatureCelsius += temperatureIncrease;

        double fadingFactor = _brakeTemperatureCelsius > TEMP_LIMIT ? Math.Max(0.3, 1.0 - (_brakeTemperatureCelsius - TEMP_LIMIT) * 0.003) : 1.0;
        double wearIncrease = 0.15 * (_brakeTemperatureCelsius / 100.0);
        _wearPercent = Math.Min(100.0, _wearPercent + wearIncrease);
        double activeEfficiency = _efficiencyPercent * fadingFactor;

        string safetyStatus = activeEfficiency < 50.0 ? "ВИЯВЛЕНО КРИТИЧНЕ ЗНИЖЕННЯ ЕФЕКТИВНОСТІ! Шлях гальмування збільшено." : "ABS активна (18 імпульсів). Автомобіль стабільний.";

        return $"Екстрене гальмування активовано! Теплове навантаження: +{temperatureIncrease:F1}°C (поточна: {_brakeTemperatureCelsius:F1}°C). {safetyStatus} (знос: {_wearPercent:F2}%).";
    }
}
